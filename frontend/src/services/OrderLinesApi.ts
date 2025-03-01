import api from "./api";

const SalesPersonApi = {
    getAllSalesPersons() {
        return api.get("/api/Salesperson/getAllSalesPersons");
    },
};

export default SalesPersonApi;
