<img src="https://user-images.githubusercontent.com/44977760/263112420-72722660-3c01-4cc7-8ac0-0a35f2959569.png" alt="ShadowBuddyService" width="10%">

# ShadowBuddyService

Welcome to the ShadowBuddyService README! This microservice is designed to handle budgeting and financial management tasks
as part of a larger system. It provides gRPC API endpoints that allow users to perform various budget operations. This
microservice utilizes CQRS architecture to separate command and query responsibilities, improving performance and
scalability.

## Introduction

ShadowBuddyService is responsible for managing budget-related operations within your application. It provides gRPC
handlers that allow users to create, update, delete, and retrieve budget information. This microservice ensures
separation of concerns and can be easily integrated with other microservices.

## Features

- In progress

## Getting Started

### Prerequisites

- .NET Core SDK (version 7.0)
- SQLite

### Installation

1. Clone this repository.
2. Navigate to the project directory.
3. Build the project: ```dotnet build```
4. Run the project: ```dotnet run```
5. The microservice will now be accessible at http://localhost:5000.

## Usage

To use the ShadowBuddyService, you can utilize the generated gRPC client libraries in your programming language. These
client libraries provide a convenient way to interact with the microservice.

## CQRS

This microservice implements CQRS to separate the command and query responsibilities. Command operations (e.g., create,
update, delete) are handled by the Command side, while Query operations (e.g., retrieval) are handled by the Query side.
This separation improves scalability and allows optimizing each side independently.

## gRPC

This microservice uses gRPC for communication between clients and the server. The .proto file defines the service
methods and data structures.

## Configuration

You can configure the microservice by modifying the appsettings.json file. Configuration options include database
connection strings, external service URLs, and more.

## Testing

Unit tests and integration tests are located in the tests directory. You can run tests using ```dotnet test```.

