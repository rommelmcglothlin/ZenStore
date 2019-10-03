Zen Store
=========
![zen-store](img.jpg)
Breathe in, breathe out. Welcome to the Zen Store where we sell Zen and Zen accessories! Namaste.

### Goals
In this App You have been tasked with creating the server for an up and coming online retail store. This application is in its early stages but the store owners know they will want to have some basic features such as products, product reviews, and customer orders. The goal, is to create a server complete with a MySQL database to have these features built out. As of now the store owners are not worried about Users or any type of Auth System.

### The Setup
This client side of this app is currently a simple testing tool that will test the appropriate configuration of the server and its `api endpoints`. You will be able to access these tests by starting up your server and navigating to `https://localhost:5001`

Utilizing the test client you will know if you have been able to complete the required tasks of this checkpoint.

Basic Interfaces have been provided and the first test is currently passing as a proof of concept however you will be responsible for setting up an actual database and ensuring all tests pass with persistent data.

### Business Rules 
For now the business rules are fairly straight forward and only pertain to Customer Orders. As such the following rules must be adhered to:

- Orders may only be placed with valid products
- Orders can only be shipped once
- Orders can only be canceled once
- If an order has been shipped it can't be canceled
- If an order has been canceled it can't be shipped
- If an order is shipped or canceled it can no longer be edited


### Coding Standards
Your boss is a stickler for quality code and is expecting the server to be written following the MVC Standards practiced in Dotnet. Keeping this in mind you will need to build out the application structure.

All of the business rules for this server need to be validated inside of the appropriate service and failure to pass a validation should throw Exceptions. 

The client has been setup to handle different status codes and may fail to pass some tests if your server replies with `200 Ok` instead of the intended `400 BadRequest`

> All Business rules must be handled solely in the service layer!

## Requirements

### Functionality
- Products support basic CRUD
- Product Reviews support basic CRUD
- Reviews can be retrieved by productId
- Orders can only be created if all products are valid
- Order total must be calculated from the Product Prices
- Dates are added to the interfaces and models and only set by the Server when the appropriate requests are issued
- Proper File and Folder structuring is followed
- RESTful API Conventions are followed as described above
- All business rules described above are implemented
- All tests pass
