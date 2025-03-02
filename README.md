# Charity sale POS(Point Of Sale)

# DataBase Management System API

- Frontend: Built with React(TypeScript) and Vite for a fast and optimized development experience.
- Backend: Developed with C#
- Backend Framework: .NET 8 and Entity Framework
- Database:
    - Primary: PostgreSQL 17.3
- API Documentation & Design Tools: Swagger

## Project Architecture

- For this project was used 3-Tier Architecture.
- For every entity was created own Controller, Service, Repository.

## Entity Descriptions

- Payment has an ID, OrderID (foreign key), CashPaid, ChangeReturned and PaymentDate.
- Product has an ID, Title, Cost, Current Quantity, StartingQuantity and ProductType.
- Orders has an ID, SalesPersonID(foreign key), OrderDate, TotalAmount and Status
- OrderLines has an ID, OrderID(foreign key), ProductID(foreign key) and Quantity.
- SalesPerson has an ID and Name.

## API

### Requests

| Method   | Url                                                    | Passing Properties                                                                                                    | Description                         | Controllers |
|----------|--------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------|-------------------------------------|-------------|
| **POST** | _`/api/Inventory/addProduct`_                          | JSON {"id": 0 , "title": "string", "cost": 0, "productType": "string", "startingQuantity": 0, "currentQuantity": 0 }  | Add new product                     | Inventory   |
| **GET**  | _`/api/Invenotry/getAllProducts`_                      | -                                                                                                                     | Get list of all products            | Inventory   |
| **GET**  | _`/api/Inventory/getProductById/{id}`_                 | JSON {"id": "number" }                                                                                                | Get product by ID                   | Inventory   |
| **PUT**  | _`/api/Invenotry/updateProdctyById{id}`_               | JSON {"id": 0 , "title": "string", "cost": 0, "productType": "string", "startingQuantity": 0,  "currentQuantity": 0 } | Update an existing Product          | Inventory   |
| **PUT**  | _`/api/Inventory/decreaseProductCurrentQuantity/{id}`_ | JSON {"id": "number" }                                                                                                | Decrease CurrentQuantity of Product | Inventory   |
| **PUT**  | _`/api/Inventory/increaseProductCurrentQuantity/{id}`_ | JSON {"id": "number" }                                                                                                | Increase CurrentQuantity of Product | Inventory   |
| **POST** | _`/api/Order/createOrder`_                             | JSON { there's pretty complex one so u can check it in Swagger ;) }                                                   | Creating a new order                | Order       |
| **GET**  | _`/api/Order/getOrderById/{id}`_                       | JSON {"id": "number" }                                                                                                | Get order by ID                     | Order       |
| **GET**  | _`/api/Order/getAllOrders`_                            | -                                                                                                                     | Return a list of orders             | Order       |
| **PUT**  | _`/api/Order/checkoutOrder/{orderId}`_                 | JSON {"orderId" + cashPaid: 0}                                                                                        | Checkout the order                  | Order       |
| **PUT**  | _`/api/Order/resetOrder/{orderId}`_                    | JSON {"orderId": "number" }                                                                                           | Reset the order                     | Order       |
| **PUT**  | _`/api/Order/updateOrder/{orderId}`_                   | JSON { there's pretty complex one so u can check it in Swagger ;) }                                                   | Update an existing order            | Order       |
| **POST** | _`/api/Payment/processPayment`_                        | JSON { "id": 0, "orderId": 0, "cashPaid": 0, "changeReturned": 0, "paymentDate": "Date"                               | Process the payment                 | Payment     |
| **GET**  | _`/api/Payment/getAllPayments`_                        | -                                                                                                                     | Get all payments                    | Payment     |
| **GET**  | _`/api/Payment/getPaymentById/{id}`_                   | JSON {"id": "number" }                                                                                                | Get payment by ID                   | Payment     |
| **POST** | _`/api/Salesperson/createSalesPerson`_                 | JSON {"id": 0, "name": "string"  }                                                                                    | Add new salesperson                 | Salesperson |
| **GET**  | _`/api/Salesperson/getAllSalesPersons`_                | -                                                                                                                     | Return a list of salespersons       | Salesperson |
| **GET**  | _`/api/Salesperson/getSalesPersonById/{id}`_           | JSON {"id": "number" }                                                                                                | Get salesperson by ID               | Salesperson |

<br><br>

# Installation(Set up the project)

To set up my project correctly you need:
- [.NET 8 SDK](https://dotnet.microsoft.com/ru-ru/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Itellij IDEA](https://www.jetbrains.com/idea/download/?section=windows)
