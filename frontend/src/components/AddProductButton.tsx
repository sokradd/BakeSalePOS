import React, { useState } from 'react';
import {
    Button,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    TextField
} from '@mui/material';
import { Product } from '/BakeSalePOS/frontend/src/Interfaces';
import ProductApi from '/BakeSalePOS/frontend/src/services/ProductsApi';

const AddProductButton: React.FC = () => {
    const [open, setOpen] = useState(false);
    const [title, setTitle] = useState('');
    const [cost, setCost] = useState<number>(0);
    const [productType, setProductType] = useState('');
    const [startingQuantity, setStartingQuantity] = useState<number>(0);
    const [currentQuantity, setCurrentQuantity] = useState<number>(0);

    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const handleSubmit = async () => {
        const newProduct: Product = {
            id: 0,
            title,
            cost,
            productType,
            startingQuantity,
            currentQuantity,
        };

        try {
            const response = await ProductApi.addProduct(newProduct);
            console.log('Product created:', response.data);
        } catch (error) {
            console.error("Error creating product:", error);
        }

        setTitle('');
        setCost(0);
        setProductType('');
        setStartingQuantity(0);
        setCurrentQuantity(0);
        handleClose();
    };

    return (
        <>
            <Button variant="contained" onClick={handleOpen}>
                Add Product
            </Button>
            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>Add New Product</DialogTitle>
                <DialogContent>
                    <TextField
                        autoFocus
                        margin="dense"
                        label="Title"
                        fullWidth
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                    />
                    <TextField
                        margin="dense"
                        label="Cost"
                        type="number"
                        fullWidth
                        value={cost}
                        onChange={(e) => setCost(parseFloat(e.target.value))}
                    />
                    <TextField
                        margin="dense"
                        label="Product Type"
                        fullWidth
                        value={productType}
                        onChange={(e) => setProductType(e.target.value)}
                    />
                    <TextField
                        margin="dense"
                        label="Starting Quantity"
                        type="number"
                        fullWidth
                        value={startingQuantity}
                        onChange={(e) => setStartingQuantity(parseInt(e.target.value))}
                    />
                    <TextField
                        margin="dense"
                        label="Current Quantity"
                        type="number"
                        fullWidth
                        value={currentQuantity}
                        onChange={(e) => setCurrentQuantity(parseInt(e.target.value))}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose}>Cancel</Button>
                    <Button onClick={handleSubmit} variant="contained">Add Product</Button>
                </DialogActions>
            </Dialog>
        </>
    );
};

export default AddProductButton;
