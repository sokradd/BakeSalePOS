import api from "./api";

interface Payment {
    id: number;
    orderId: number;
    cashPaid: number;
    changeReturned: number;
    paymentDate:  string;
}

const PaymentsApi = {
    processPayment(payment: Payment) {
        return api.post<Payment>("/api/Payment/processPayment", payment)
            .catch(error =>{
               console.error("Error processing payment:", error);
               throw error;
            });
    },
    getAllPayments(){
        return api.get<Payment[]>(`/api/Payment/getAllPayments`)
            .catch(error => {
                console.error("Error fetching payments:", error);
                throw error;
            })
    },
    getPaymentById(id:number) {
        return api.get<Payment>(`/api/Payment/getPaymentById/${id}`)
            .catch(error => {
                console.error("Error fetching payment by ID:", error);
                throw error;
            })
    }
};

export default PaymentsApi;
