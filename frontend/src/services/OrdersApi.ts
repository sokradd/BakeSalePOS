import api from "./api";

interface Order {
    Id?: number;
    SalespersonId?: number;
    OrderDate: number;
    TotalAmount: number;
    Status: string;
}

const OrdersApi = {
    createOrder(order: Order) {
        return api.post<Order>("/api/Order/createOrder", order, {
            headers: {"Content-Type": "application/json"}
        })
            .catch(error => {
                console.error("Error adding order:", error);
                throw error;
            });
    },
    getAllOrders() {
        return api.get<Order[]>("/api/Order/getAllOrders")
            .catch(error => {
                console.error("Error fetching order:", error);
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
    getAllOrdersBySalesPerson(salespersonId:number){
        return api.get<Order>(`/api/Order/getAllOrdersBySalesPerson/${salespersonId}`)
            .catch(error =>{
                console.error("Error fetching order by SalesPerson ID:", error)
                throw error;
            })
    },
    checkoutOrder(orderId: number) {
        return api.put<Order>(`/api/Order/checkoutOrder/${orderId}`)
            .catch(error => {
                console.error("Error checkout order by ID:", error);
                throw error;
            });
    },
    resetOrder(orderId: number) {
        return api.put<Order>(`/api/Order/resetOrder/${orderId}`)
            .catch(error => {
                console.error("Error reset order by ID:", error);
                throw error;
            });
    }
};

export default OrdersApi;
