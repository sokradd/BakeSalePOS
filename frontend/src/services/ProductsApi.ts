import api from "./api";

interface Product {
    id: number;
    title: string;
    cost: number;
    currentQuantity: number;
    startingQuantity: number;
    productType: string;

}

const ProductApi = {
    addProduct(product: Product) {
        return api.post<Product>("/api/Inventory/addProduct", product, {
            headers: { "Content-Type": "application/json" }
        })
            .catch(error => {
                console.error("Error adding product:", error);
                throw error;
            });
    },
    getAllProducts() {
        return api.get<Product[]>("/api/Inventory/getAllProducts")
            .catch(error => {
                console.error("Error fetching products:", error);
                throw error;
            });
    },
    getProductById(id: number) {
        return api.get<Product>(`/api/Inventory/getProductById/${id}`)
            .catch(error => {
                console.error("Error fetching product by ID:", error);
                throw error;
            });
    },
    updateProductsById(id:number) {
        return api.put<Product>(`/api/Inventory/updateProductsById/${id}`)
            .catch(error => {
                console.error("Error updating product by ID:", error);
                throw error;
            });
    },
    decreaseProductCurrentQuantity(id:number){
        return api.put<Product>(`/api/Inventory/decreaseProductCurrentQuantity/${id}`)
            .catch(error => {
                console.error("Error decreasing product by ID:", error);
                throw error;
            });
        },
    inreaseProductCurrentQuantity(id:number){
        return api.put<Product>(`/api/Inventory/increaseProductCurrentQuantity/${id}`)
            .catch(error => {
                console.error("Error increasing product by ID:", error);
                throw error;
            });
    }
};

export default ProductApi;