## TechnoMarket.eShop


### BilgeAdam .Net Full Stack Course Graduation Project
**Electronic products sales system made in a microservices architecture using .NET Core**

There is a couple of microservices which implemented e-commerce modules over **Catalog, Basket, Customer** and **Order** microservices with **NoSQL (MongoDB, Redis)** and **Relational databases (PostgreSQL, Sql Server)** with communicating over **RabbitMQ Communication** and using **Ocelot API Gateway**.

![alt text](https://i.ibb.co/cbbd2xT/project-architecture.jpg)

### Whats Including In This Repository

#### Catalog microservice which includes; 
* ASP.NET Core Web API application 
* REST API principles, CRUD operations
* Repository and Unit Of Work pattern Implementation
* **Sql Server database** connection and containerization
* Using **Entity Framework Core ORM**
* Using **FluentValidation** and **AutoMapper**
* Using **Custom Response, Middleware and Exceptions** with Shared Library
* Testing with **xUnitTest** for Services and Controllers
* Logging with **nLog** for Services

#### Customer microservice which includes; 
* ASP.NET Core Web API application 
* REST API principles, CRUD operations
* Repository and Unit Of Work pattern Implementation
* **PostgreSQL database** connection and containerization
* Using **Entity Framework Core ORM**
* Using **FluentValidation** and **AutoMapper**
* Using **Custom Response, Middleware and Exceptions** with Shared Library
* Logging with **nLog** for Services

#### Order microservice which includes; 
* ASP.NET Core Web API application 
* REST API principles, CRUD operations
* **MongoDB database** connection and containerization
* Using **MongoDB Driver**
* Using **FluentValidation** and **AutoMapper**
* Using **Custom Response, Middleware and Exceptions** with Shared Library
* Logging with **nLog** for Services

#### Basket microservice which includes; 
* ASP.NET Core Web API application 
* REST API principles, CRUD operations
* **Redis database** connection and containerization
* Using **FluentValidation**
* Using **Custom Response, Middleware and Exceptions** with Shared Library
* Logging with **nLog** for Services

#### PhotoStock microservice which includes; 
* ASP.NET Core Web API application 
* REST API principles, CRUD operations
* Using **Custom Response, Middleware and Exceptions** with Shared Library

#### API Gateway Ocelot Microservice which includes; 
* Implement **API Gateways with Ocelot**
* Sample microservices/containers to reroute through the API Gateways

#### WebUI ShoppingApp Microservice
* ASP.NET Core Web Application with Bootstrap 5 and Razor template
* Call **Ocelot APIs with HttpClientFactory**

