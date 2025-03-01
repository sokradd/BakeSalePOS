
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import SalesPersonMenu from '../Buttons/SalesPersonMenu.tsx';

export default function ButtonAppBar() {
    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="static" color="transparent">
                <Toolbar>
                    <Typography variant="h6" component="div">
                        BakeSalePOS
                    </Typography>
                    <Box sx={{ ml: 'auto' }}>
                        <SalesPersonMenu />
                    </Box>
                </Toolbar>
            </AppBar>
        </Box>
    );
}
