import React, { useState, useEffect } from 'react';
import {
    Grid, Paper, Typography, Button, List,
    ListItem, ListItemText, TextField, Menu, MenuItem
} from '@mui/material';
import ProductApi from './services/ProductsApi';
import OrdersApi from './services/OrdersApi';
import SalesPersonApi from './services/SalesPersonApi';
import { Product, CartItem, Order, OrderLine } from './Interfaces';
import { AxiosResponse } from "axios";

const POSLayout: React.FC = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [cartItems, setCartItems] = useState<CartItem[]>([]);
    const [salespersons, setSalespersons] = useState<any[]>([]);
    const [selectedSalespersonId, setSelectedSalespersonId] = useState<number | null>(null);
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const [cashPaid, setCashPaid] = useState<number>(0);
    const [currentOrderId, setCurrentOrderId] = useState<number | null>(null);

    const open = Boolean(anchorEl);

    useEffect(() => {
        ProductApi.getAllProducts()
            .then((response: AxiosResponse<Product[]>) => {
                console.log("Products loaded:", response.data);
                setProducts(response.data);
            })
            .catch(error => console.error("Error loading products:", error));
    }, []);

    useEffect(() => {
        SalesPersonApi.getAllSalesPersons()
            .then(res => setSalespersons(res.data))
            .catch(err => console.error("Error loading salespersons:", err));
    }, []);

    const addToCart = async (product: Product) => {
        if (product.currentQuantity <= 0) return;

        const newOrderLine: OrderLine = {
            id: 0,
            orderId: currentOrderId || 0,
            productId: product.id,
            quantity: 1,
        };

        if (!currentOrderId) {
            if (!selectedSalespersonId) {
                alert("Please select a salesperson first.");
                return;
            }
            const newOrder: Order = {
                id: 0,
                orderDate: new Date().toISOString(),
                totalAmount: product.cost,
                salespersonId: selectedSalespersonId,
                status: "Pending",
                orderLines: [newOrderLine],
            };

            try {
                const response = await OrdersApi.createOrder(newOrder);
                const createdOrder = response.data;
                setCurrentOrderId(createdOrder.id);
                setCartItems([{
                    id: product.id,
                    name: product.title,
                    price: product.cost,
                    quantity: 1,
                    productType: product.productType,
                    startingQuantity: product.startingQuantity,
                    currentQuantity: product.currentQuantity,
                }]);
            } catch (error) {
                console.error("Error creating order:", error);
                alert("Failed to create order.");
                return;
            }
        } else {
            let updatedCartItems: CartItem[];
            const existingItem = cartItems.find(item => item.id === product.id);
            if (existingItem) {
                updatedCartItems = cartItems.map(item =>
                    item.id === product.id ? { ...item, quantity: item.quantity + 1 } : item
                );
            } else {
                updatedCartItems = [
                    ...cartItems,
                    {
                        id: product.id,
                        name: product.title,
                        price: product.cost,
                        quantity: 1,
                        productType: product.productType,
                        startingQuantity: product.startingQuantity,
                        currentQuantity: product.currentQuantity,
                    }
                ];
            }
            setCartItems(updatedCartItems);

            const newTotalAmount = updatedCartItems.reduce((total, item) => total + item.price * item.quantity, 0);

            const updatedOrderLines: OrderLine[] = updatedCartItems.map(item => ({
                id: 0,
                orderId: currentOrderId!,
                productId: item.id,
                quantity: item.quantity,
            }));

            const updatedOrder: Order = {
                id: currentOrderId!,
                orderDate: new Date().toISOString(),
                totalAmount: newTotalAmount,
                salespersonId: selectedSalespersonId!,
                status: "Pending",
                orderLines: updatedOrderLines,
            };

            try {
                await OrdersApi.updateOrder(currentOrderId!, updatedOrder);
            } catch (error) {
                console.error("Error updating order:", error);
                alert("Failed to update order.");
                return;
            }
        }


        setProducts(prevProducts =>
            prevProducts.map(p =>
                p.id === product.id ? { ...p, currentQuantity: p.currentQuantity - 1 } : p
            )
        );
    };


    const totalAmount = cartItems.reduce((total, item) => total + item.price * item.quantity, 0);

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

    const checkout = async () => {
        if (!currentOrderId) {
            alert("Order not created.");
            return;
        }
        if (cashPaid < totalAmount) {
            alert("Cash paid is less than total amount!");
            return;
        }
        try {
            const response = await OrdersApi.checkoutOrder(currentOrderId, cashPaid);
            console.log("Checkout response:", response.data);
            alert("Payment successful!");
            setCartItems([]);
            setCashPaid(0);
            setCurrentOrderId(null);
        } catch (error) {
            console.error("Error during checkout:", error);
            alert("Failed to process payment.");
        }
    };

    const resetOrder = async () => {
        if (!currentOrderId) return;
        try {
            await OrdersApi.resetOrder(currentOrderId);
            alert("Order reset successfully!");
            const response = await ProductApi.getAllProducts();
            setProducts(response.data);
            setCartItems([]);
            setCashPaid(0);
            setCurrentOrderId(null);
        } catch (error) {
            console.error("Error resetting order:", error);
            alert("Failed to reset order.");
        }
    };

    return (
        <>
            <Grid container spacing={2} sx={{ height: 'calc(100vh - 64px)', padding: 2, overflow: 'hidden', boxSizing: 'border-box' }}>
                <Grid item xs={8} sx={{ display: 'flex', flexDirection: 'column', gap: 2, overflow: 'hidden', height: '100%' }}>
                    <Paper sx={{ padding: 2 }}>
                        <Button
                            id="salesperson-button"
                            aria-controls={open ? 'salesperson-menu' : undefined}
                            aria-haspopup="true"
                            aria-expanded={open ? 'true' : undefined}
                            color="success"
                            onClick={handleClick}
                            variant="contained"
                        >
                            {selectedSalespersonId ? `SalesPerson #${selectedSalespersonId}` : "Select SalesPerson"}
                        </Button>
                        <Menu
                            id="salesperson-menu"
                            anchorEl={anchorEl}
                            open={open}
                            onClose={handleClose}
                            MenuListProps={{ 'aria-labelledby': 'salesperson-button' }}
                        >
                            {salespersons.map(sp => (
                                <MenuItem key={sp.id} onClick={() => handleSelectSalesperson(sp.id)}>
                                    {sp.name}
                                </MenuItem>
                            ))}
                        </Menu>
                    </Paper>

                    <Paper sx={{ padding: 2, flex: 1, overflow: 'hidden', height: '50%', display: 'flex', flexDirection: 'column' }}>
                        <Typography variant="h6" gutterBottom>
                            Baking
                        </Typography>
                        <Grid container spacing={2} sx={{ overflowY: 'auto', flexGrow: 1 }}>
                            {products.filter(p => p.productType === 'Baking').map(product => (
                                <Grid item xs={4} key={product.id}>
                                    <Paper sx={{ padding: 2, textAlign: 'center' }}>
                                        <Typography variant="h6">{product.title}</Typography>
                                        <Typography variant="body1">{product.cost} eur.</Typography>
                                        <Button
                                            variant="contained"
                                            sx={{ mt: 1 }}
                                            onClick={() => addToCart(product)}
                                            disabled={product.currentQuantity <= 0}
                                        >
                                            {product.currentQuantity > 0 ? "Add to Cart" : "Out of stock"}
                                        </Button>
                                    </Paper>
                                </Grid>
                            ))}
                        </Grid>
                    </Paper>

                    <Paper sx={{ padding: 2, flex: 1, overflow: 'hidden', mt: 2, height: '50%', display: 'flex', flexDirection: 'column' }}>
                        <Typography variant="h6" gutterBottom>
                            Second Hand Items
                        </Typography>
                        <Grid container spacing={2} sx={{ overflowY: 'auto', flexGrow: 1 }}>
                            {products.filter(p => p.productType === 'SecondHand').map(product => (
                                <Grid item xs={4} key={product.id}>
                                    <Paper sx={{ padding: 2, textAlign: 'center' }}>
                                        <Typography variant="h6">{product.title}</Typography>
                                        <Typography variant="body1">{product.cost} eur.</Typography>
                                        <Button
                                            variant="contained"
                                            sx={{ mt: 1 }}
                                            onClick={() => addToCart(product)}
                                            disabled={product.currentQuantity <= 0}
                                        >
                                            {product.currentQuantity > 0 ? "Add to Cart" : "Out of stock"}
                                        </Button>
                                    </Paper>
                                </Grid>
                            ))}
                        </Grid>
                    </Paper>
                </Grid>

                <Grid item xs={4}>
                    <Paper sx={{ padding: 2, height: '100%', display: 'flex', flexDirection: 'column', gap: 2, overflow: 'hidden', boxSizing: 'border-box' }}>
                        <Typography variant="h6" gutterBottom>
                            Cart
                        </Typography>
                        <List sx={{ flexGrow: 1, overflowY: 'auto', maxHeight: 'calc(100vh - 300px)' }}>
                            {cartItems.map(item => (
                                <ListItem key={item.id}>
                                    <ListItemText primary={item.name} secondary={`${item.quantity} x ${item.price} eur.`} />
                                    <Typography variant="body1">{(item.quantity * item.price).toFixed(2)} eur.</Typography>
                                </ListItem>
                            ))}
                        </List>
                        <Typography variant="h6" sx={{ mt: 2 }}>
                            Total: {totalAmount.toFixed(2)} eur.
                        </Typography>
                        <TextField
                            label="Cash Paid"
                            type="number"
                            value={cashPaid}
                            onChange={(e) => setCashPaid(parseFloat(e.target.value))}
                            fullWidth
                            sx={{ mb: 2 }}
                        />
                        <Button variant="contained" fullWidth sx={{ mt: 2 }} onClick={checkout}>
                            Checkout
                        </Button>
                        <Button variant="outlined" fullWidth sx={{ mt: 1 }} onClick={resetOrder} disabled={!currentOrderId}>
                            Reset Order
                        </Button>
                    </Paper>
                </Grid>
            </Grid>
        </>
    );
};

export default POSLayout;
