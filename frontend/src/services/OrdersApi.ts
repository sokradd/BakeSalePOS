import api from "./api";

export interface OrderLine {
    id: number;
    orderId: number;
    productId: number;
    quantity: number;
}

export interface Order {
    id: number;
    orderDate:  string;
    totalAmount: number;
    salespersonId: number;
    status: string;
    orderLines: OrderLine[];
}

const OrdersApi = {
    createOrder(order: Order) {
        return api.post<Order>("/api/Order/createOrder", order, {
            headers: { "Content-Type": "application/json" }
        })
            .catch(error => {
                console.error("Error creating order:", error);
                throw error;
            });
    },

    updateOrder(orderId: number, order: Order) {
        return api.put<Order>(`/api/Order/updateOrder/${orderId}`, order, {
            headers: { "Content-Type": "application/json" }
        })
            .catch(error => {
                console.error("Error updating order:", error);
                throw error;
            });
    },

    checkoutOrder(orderId: number, cashAmount: number) {
        return api.put<Order>(`/api/Order/checkoutOrder/${orderId}`, cashAmount, {
            headers: { "Content-Type": "application/json" }
        })
            .catch(error => {
                console.error("Error during checkout for order ID:", error);
                throw error;
            });
    },

    resetOrder(orderId: number) {
        return api.put<Order>(`/api/Order/resetOrder/${orderId}`)
            .catch(error => {
                console.error("Error resetting order for order ID:", error);
                throw error;
            });
    },

    getAllOrders() {
        return api.get<Order[]>("/api/Order/getAllOrders")
            .catch(error => {
                console.error("Error fetching orders:", error);
                throw error;
            });
    },

    getOrderById(id: number) {
        return api.get<Order>(`/api/Order/getOrderById/${id}`)
            .catch(error => {
                console.error("Error fetching order by ID:", error);
                throw error;
            });
    },

    getAllOrdersBySalesPerson(salespersonId: number) {
        return api.get<Order[]>(`/api/Order/getAllOrdersBySalesPerson/${salespersonId}`)
            .catch(error => {
                console.error("Error fetching orders by Salesperson ID:", error);
                throw error;
            });
    }
};

export default OrdersApi;
