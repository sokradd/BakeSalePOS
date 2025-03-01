import React from 'react';
import { Grid, Paper, Typography, Button, List, ListItem, ListItemText } from '@mui/material';
import Header from "./components/Sections/Header.tsx";

const POSLayout: React.FC = () => {
    // Пример данных товаров
    const baking = [
        { id: 1, name: 'Baking 1', price: 100 },
        { id: 2, name: 'Baking 2', price: 200 },
        { id: 3, name: 'Baking 3', price: 300 },
        { id: 4, name: 'Baking 4', price: 200 },
        { id: 5, name: 'Baking 5', price: 300 },
    ];

    const secondHandItems = [
        { id: 1, name: 'Second Hand 1', price: 100 },
        { id: 2, name: 'Second Hand 2', price: 200 },
        { id: 3, name: 'Second Hand 3', price: 300 },
        { id: 4, name: 'Second Hand 4', price: 200 },
    ];

    // Пример данных корзины
    const cartItems = [
        { id: 1, name: 'Baking 1', price: 100, quantity: 2 },
        { id: 2, name: 'Baking 2', price: 200, quantity: 1 },
        { id: 3, name: 'Second Hand 1', price: 100, quantity: 1 },

    ];

    const totalAmount = cartItems.reduce((total, item) => total + item.price * item.quantity, 0);

    return (
        <>
            <Header />
            <Grid container spacing={2} sx={{ height: 'calc(100vh - 64px)', padding: 2, overflow: 'hidden', boxSizing: 'border-box' }}>
                {/* Левая часть: Список товаров */}
                <Grid item xs={8} sx={{ display: 'flex', flexDirection: 'column', gap: 2, overflow: 'hidden', height: '100%' }}>
                    {/* Секция Baking */}
                    <Paper sx={{ padding: 2, flex: 1, overflow: 'hidden', height: '50%', display: 'flex', flexDirection: 'column' }}>
                        <Typography variant="h6" gutterBottom>
                            Baking
                        </Typography>
                        <Grid container spacing={2} sx={{ overflowY: 'auto', flexGrow: 1 }}>
                            {baking.map((product) => (
                                <Grid item xs={4} key={product.id}>
                                    <Paper sx={{ padding: 2, textAlign: 'center' }}>
                                        <Typography variant="h6">{product.name}</Typography>
                                        <Typography variant="body1">{product.price} eur.</Typography>
                                        <Button variant="contained" sx={{ mt: 1 }}>
                                            Add to cart
                                        </Button>
                                    </Paper>
                                </Grid>
                            ))}
                        </Grid>
                    </Paper>

                    {/* Секция Second Hand Items */}
                    <Paper sx={{ padding: 2, flex: 1, overflow: 'hidden', mt: 2, height: '50%', display: 'flex', flexDirection: 'column' }}>
                        <Typography variant="h6" gutterBottom>
                            Second Hand Items
                        </Typography>
                        <Grid container spacing={2} sx={{ overflowY: 'auto', flexGrow: 1 }}>
                            {secondHandItems.map((product) => (
                                <Grid item xs={4} key={product.id}>
                                    <Paper sx={{ padding: 2, textAlign: 'center' }}>
                                        <Typography variant="h6">{product.name}</Typography>
                                        <Typography variant="body1">{product.price} eur.</Typography>
                                        <Button variant="contained" sx={{ mt: 1 }}>
                                            Add to cart
                                        </Button>
                                    </Paper>
                                </Grid>
                            ))}
                        </Grid>
                    </Paper>
                </Grid>

                {/* Правая часть: Корзина и управление */}
                <Grid item xs={4}>
                    <Paper sx={{
                        padding: 2,
                        height: '100%', // Занимает всю высоту родительского контейнера
                        display: 'flex',
                        flexDirection: 'column',
                        gap: 2,
                        overflow: 'hidden', // Запрещаем выход за пределы контейнера
                        boxSizing: 'border-box'
                    }}>
                        <Typography variant="h6" gutterBottom>
                            Cart
                        </Typography>
                        {/* Контейнер для списка товаров с прокруткой */}
                        <List sx={{
                            flexGrow: 1, // Занимает всё доступное пространство
                            overflowY: 'auto', // Включаем вертикальную прокрутку
                            maxHeight: 'calc(100vh - 300px)', // Ограничиваем высоту
                            '&::-webkit-scrollbar': { // Стилизация скроллбара (опционально)
                                width: '8px',
                            },
                            '&::-webkit-scrollbar-track': {
                                background: '#f1f1f1',
                            },
                            '&::-webkit-scrollbar-thumb': {
                                background: '#888',
                                borderRadius: '4px',
                            },
                            '&::-webkit-scrollbar-thumb:hover': {
                                background: '#555',
                            }
                        }}>
                            {cartItems.map((item) => (
                                <ListItem key={item.id}>
                                    <ListItemText primary={item.name} secondary={`${item.quantity} x ${item.price} eur.`} />
                                    <Typography variant="body1">{item.quantity * item.price} eur.</Typography>
                                </ListItem>
                            ))}
                        </List>
                        <Typography variant="h6" sx={{ mt: 2 }}>
                            Total amount: {totalAmount} eur.
                        </Typography>
                        <Button variant="contained" fullWidth sx={{ mt: 2 }}>
                            Checkout
                        </Button>
                        <Button variant="outlined" fullWidth sx={{ mt: 1 }}>
                            Reset
                        </Button>
                    </Paper>
                </Grid>
            </Grid>
        </>
    );
};

export default POSLayout;