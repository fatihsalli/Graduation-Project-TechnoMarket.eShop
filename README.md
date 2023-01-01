# TechnoMarket.eShop


### BilgeAdam .Net Full Stack Course Graduation Project
**Electronic products sales system made in a microservices architecture using .NET Core**

There is a couple of microservices which implemented e-commerce modules over **Catalog, Basket, Customer, Order** and **PhotoStock** microservices with **NoSQL (MongoDB, Redis)** and **Relational databases (PostgreSQL, Sql Server)** with communicating over **RabbitMQ Communication with MassTransit** and using **Ocelot API Gateway**. You can see the overall picture of implementations on a e-commerce microservices project with using .net tools;

![alt text](https://i.ibb.co/Zh7pLyW/project-architecture.jpg)

## Whats Including In This Repository

#### Catalog microservice which includes; 
* ASP.NET Core Web API application 
* REST API principles, CRUD operations
* Repository and Unit Of Work Pattern Implementation
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

#### IdentityServer microservice which includes (Not completed yet)
* Using IdentityServer4  

#### API Gateway Ocelot Microservice which includes; 
* Implement **API Gateways with Ocelot**
* Sample microservices/containers to reroute through the API Gateways
* The Gateway aggregation pattern in **Shopping.Aggregator**

#### Microservices Cross-Cutting Implementations
* Implementing Centralized Distributed Logging with Elastic Stack (ELK); Elasticsearch, Logstash, Kibana and SeriLog for Microservices **(Not completed yet)**
* Implementing Logging with Nlog for each Microservices

#### WebUI ShoppingApp Microservice
* ASP.NET Core Web Application with Bootstrap 5 and Razor template
* Call **Ocelot APIs with HttpClientFactory**

#### Docker Compose establishment with all microservices on docker
* Containerization of microservices
* Containerization of databases
* Override Environment variables
