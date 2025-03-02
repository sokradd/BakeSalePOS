import React from 'react';
import { Button } from '@mui/material';
import OrdersApi from '../services/OrdersApi';
import PaymentsApi from '../services/PaymentsApi';
import ProductApi from '../services/ProductsApi';
import { Order, Payment, Product } from '../Interfaces';

const FetchDataButton: React.FC = () => {

    const handleFetchAllData = async () => {
        try {

            const paymentsResponse = await PaymentsApi.getAllPayments();
            const payments: Payment[] = paymentsResponse.data;
            console.log("Payments:", payments);

            const ordersResponse = await OrdersApi.getAllOrders();
            const orders: Order[] = ordersResponse.data;
            console.log("Orders:", orders);

            const productsResponse = await ProductApi.getAllProducts();
            const products: Product[] = productsResponse.data;
            console.log("Products:", products);

            alert("Data fetched successfully. Check console for details.");


        } catch (error) {
            console.error("Error fetching data:", error);
            alert("Failed to fetch data.");
        }
    };

    return (
        <Button variant="contained" color="inherit" onClick={handleFetchAllData}>
            Fetch All Data
        </Button>
    );
};

export default FetchDataButton;
