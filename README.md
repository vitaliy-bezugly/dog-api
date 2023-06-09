# Dogs house service
This repository contains a sample REST API built with C# that interacts with a Microsoft SQL database table named "dogs.".

## Clone the repository:

```bash
git clone https://github.com/vitaliy-bezugly/dog-api.git
```

## Local running

 To run the application on your local machine, you have two options:
1. Using .NET Core: Make sure you have the .NET Core SDK installed. Open a terminal or command prompt and navigate to the root folder of the project. Run the following command:

```
dotnet run --project src/WebApi
```

Ensure that you have configured the database connection. You can either update the connection string in the appsettings.Development.json file or set it as an environment variable (<code>DB_CONN</code>).

2. Using <code>Docker</code>: Make sure you have Docker installed on your machine. Open a terminal or command prompt and navigate to the root folder of the project. Run the following command:
```
docker-compose up -d
```

This command will build and run the Docker containers for the application, along with all their dependencies. The API will be accessible at http://localhost:8080 on your host computer.

Note: With Docker, the necessary database migrations will be applied automatically.

## Overview

Domain
This will contain only entities to the domain layer.

Application
This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

Infrastructure
This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

WebApi
This layer is a Web API. This layer depends on both the Application and Infrastructure layers.

## API Endpoints
1. Ping
URL: GET /ping
Description: Returns a message indicating the version of the Dogs House service.
Example: curl -X GET http(s)://localhost:[port]/ping
2. Query Dogs
URL: GET /dogs
Description: Returns a list of dogs, supporting sorting and pagination.
Example: curl -X GET http(s)://localhost:[port]/dogs
Query Parameters:
attribute: (Optional) Specifies the attribute for sorting (e.g., weight).
order: (Optional) Specifies the sort order (asc or desc).
pageNumber: (Optional) Specifies the page number for pagination.
pageSize: (Optional) Specifies the number of dogs per page.
3. Create Dog
URL: POST /dog
Description: Creates a new dog with the provided details.
Example: curl -X POST http(s)://localhost:[port]/dog -d '{"name": "Doggy", "color": "red", "tail_length": 173, "weight": 33}'
Request Body: Provide the details of the dog in JSON format.
Handling Too Many Requests
The application includes logic to handle situations when there are too many incoming requests. It limits the number of requests that can be processed within a certain timeframe. If the limit is exceeded, the API will return an HTTP status code 429 Too Many Requests.

To configure the maximum number of requests allowed per second, modify <code>appsettings.json</code> in the application.

## Error Handling
The API handles various error cases to ensure proper functionality. Some common cases that are handled include:

Dog with the same name already exists in the database.
Invalid tail length (negative number or non-numeric value).
Invalid JSON passed in the request body.
Detailed error messages and appropriate HTTP status codes are returned to the client to facilitate proper error handling on the consumer side.

## Handling Too Many Requests
The application includes logic to handle 10 requests per second (as default)

## Tools and Technologies
The Dogs House API is developed using the following tools and technologies:

- ASP.NET Core 6
- Entity Framework Core
- Code First principle
- MS SQL
- MediatR
- FluentValidation
- XUnit
- FluentAssertion
- Moq
- Microsoft.AspNetCore.Mvc.Testing
