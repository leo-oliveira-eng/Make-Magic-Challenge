# Make-Magic-Challenge ![.NET Core](https://github.com/leo-oliveira-eng/Make-Magic-Challenge/workflows/.NET%20Core/badge.svg) [![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md)

API was developed in .Net Core solving the Make Magic challenge. The specifications for building the API can be found [here](https://github.com/dextra/challenges/blob/master/backend/MAKE-MAGIC-PT.md). The application was developed as a database first implemented in MySql using Entity Framework Core as ORM. The solution was implemented using DDD and TDD techniques.

The API documentation was implemented using swagger. A message package and a connector were also created to allow connection to the API Gateway.

Thinking about microservices structure, the components that might be reused by other possible services were separated into packages in Nuget.


![make magic](https://user-images.githubusercontent.com/38479895/92552595-6764be00-f237-11ea-829b-627966608a07.png)


## Dependencies

As mentioned, the API uses components that are subject to reuse. They have been separated into packages. Below there is a table showing the function of each one. Just click on the name of the packages to be redirected to those repositories.

|                              Package                                             |                                          Desciption                                         |
| -------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------- |
| [BaseEntity](https://github.com/leo-oliveira-eng/BaseEntity)                     | Package that encapsulate basic properties and methods to facilitade create entities.        | 
| [Message.Response.Maybe](https://github.com/leo-oliveira-eng/Message)            | This package encapsulates responses between methods.                                        |
| [Infra.BaseRepository](https://github.com/leo-oliveira-eng/Infra)                | Package that encapsulate the implementation of the repository pattern  with Unit of Work.   |
| [HTTP.Request.Service](https://github.com/leo-oliveira-eng/HTTP-Request-Service) | Package that encapsulates methods to help REST communication between HTTP services.         |
| [API-Gateway](https://github.com/leo-oliveira-eng/API-Gateway)                   | Abstraction layer for routes the requests in a microservice oriented application            |


## Licence 

The project is under [MIT License](LICENSE.md), so it grants you permission to use, copy, and modify a piece of this software free of charge, as is, without restriction or warranty.
