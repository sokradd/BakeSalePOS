export interface Product {
    id: number;
    title: string;
    cost: number;
    currentQuantity: number;
    startingQuantity: number;
    productType: string;
}

export interface Salesperson {
    id: number;
    name: string;
}

export interface Order {
    id: number;
    orderDate: string;
    totalAmount: number;
    salespersonId: number;
    status: string;
    orderLines: OrderLine[];
}

export interface OrderLine {
    id: number;
    orderId: number;
    productId: number;
    quantity: number;
}

export interface Payment {
    id: number;
    orderId: number;
    cashPaid: number;
    changeReturned: number;
    paymentDate: string;
}

export interface CartItem {
    id: number;
    name: string;
    price: number;
    quantity: number;
    productType: string;
    startingQuantity: number;
    currentQuantity: number;
}
