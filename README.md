<img src="https://user-images.githubusercontent.com/44977760/263112420-72722660-3c01-4cc7-8ac0-0a35f2959569.png" alt="ShadowPal" width="10%">

# ShadowPalService

Welcome to the ShadowPalService README! This microservice is designed to handle budgeting and financial management tasks as part of a larger system. It's built using C# and follows microservices architecture principles to ensure scalability, maintainability, and modularity.

##Introduction
ShadowPalService is responsible for managing budget-related operations within your application. It provides gRPC handlers that allow users to create, update, delete, and retrieve budget information. This microservice ensures separation of concerns and can be easily integrated with other microservices.

##Features
 - In progress

##Getting Started
###Prerequisites
 - .NET Core SDK (version 7.0)
 - SQLite
###Installation
 1. Clone this repository.
 2. Navigate to the project directory.
 3. Build the project: ```dotnet build```
 4. Run the project: ```dotnet run```
 5. The microservice will now be accessible at http://localhost:5000.
##Usage
To use the ShadowPalService, you can utilize the generated gRPC client libraries in your programming language. These client libraries provide a convenient way to interact with the microservice.
##gRPC
This microservice uses gRPC for communication between clients and the server. The .proto file defines the service methods and data structures.
##Configuration
You can configure the microservice by modifying the appsettings.json file. Configuration options include database connection strings, external service URLs, and more.
##Testing
Unit tests and integration tests are located in the tests directory. You can run tests using dotnet test.

