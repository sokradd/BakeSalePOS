import api from "./api";

interface Salesperson {
    Id?: number;
    Name: string;
}

const SalesPersonApi = {
    createSalesPerson(salesPerson: Salesperson) {
        return api.post<Salesperson>(`/api/Salesperson/createSalesPerson`, salesPerson)
            .catch(error => {
                console.error("Error adding SalesPerson:",error);
                throw error;
            })
    },
    getAllSalesPersons() {
        return api.get<Salesperson[]>("/api/Salesperson/getAllSalesPersons")
            .catch(error => {
                console.error("Error fetching SalesPersons:", error);
                throw error;
            })
    },
    getSalesPersonById(id:number){
        return api.get<Salesperson>(`/api/Salesperson/getSalesPersonById/${id}`)
            .catch(error => {
                console.error("Error fetching SalesPerson by ID:", error);
                throw error;
            })
    }
};

export default SalesPersonApi;
