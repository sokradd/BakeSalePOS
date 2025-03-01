import * as React from 'react';
import Button from '@mui/material/Button';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import SalesPersonApi from "../../services/SalesPersonApi";

export default function BasicMenu() {
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const [salespersons, setSalespersons] = React.useState<any[]>([]);
    const [selectedSalespersonId, setSelectedSalespersonId] = React.useState<number | null>(null);

    React.useEffect(() => {
        SalesPersonApi.getAllSalesPersons()
            .then(res => {
                setSalespersons(res.data);
            })
            .catch(err => console.error("Error to get salespersons:", err));
    }, []);

    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    const handleSelectSalesperson = (id: number) => {
        setSelectedSalespersonId(id);
        handleClose();
    };

    return (
        <div>
            <Button
                id="basic-button"
                aria-controls={open ? 'basic-menu' : undefined}
                aria-haspopup="true"
                aria-expanded={open ? 'true' : undefined}
                color="success"
                onClick={handleClick}
                variant="contained"
                style={{}}
            >
                {selectedSalespersonId ? `SalesPerson #${selectedSalespersonId}` : "Choose SalesPerson"}
            </Button>
            <Menu
                id="basic-menu"
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
                MenuListProps={{ 'aria-labelledby': 'basic-button' }}
            >
                {salespersons.map(sp => (
                    <MenuItem
                        key={sp.id}
                        onClick={() => handleSelectSalesperson(sp.id)}
                    >
                        {sp.name}
                    </MenuItem>
                ))}
            </Menu>
        </div>
    );
}
