# TechnoMarket.eShop


### BilgeAdam .Net Full Stack Course Graduation Project
**Electronic products sales system based on a simplified microservices architecture and Docker containers using .NET Core 6**

There are five microservices which implemented e-commerce modules over **Catalog, Basket, Customer, Order** and **PhotoStock** microservices with **NoSQL (MongoDB, Redis)** and **Relational databases (PostgreSQL, Sql Server)** with communicating using **RabbitMQ-MassTransit** and **Ocelot API Gateway**. You can see the diagram of the project below;

![alt text](https://i.ibb.co/XVBh0D8/project-architecture.jpg)

## Whats Including In This Repository

#### Catalog microservice
* ASP.NET Core Web API application 
* REST API principles, CRUD operations
* Repository and Unit Of Work Pattern Implementation
* **Sql Server database** connection and containerization
* Using **Entity Framework Core ORM**
* Using **FluentValidation** and **AutoMapper**
* Using **Custom Response, Middleware and Exceptions** with Shared Library
* Testing with **xUnitTest** for Services and Controllers

#### Customer microservice
* ASP.NET Core Web API application 
* REST API principles, CRUD operations
* Repository and Unit Of Work pattern Implementation
* **PostgreSQL database** connection and containerization
* Using **Entity Framework Core ORM**
* Using **FluentValidation** and **AutoMapper**
* Using **Custom Response, Middleware and Exceptions** with Shared Library

#### Order microservice
* ASP.NET Core Web API application 
* REST API principles, CRUD operations
* **MongoDB database** connection and containerization
* Using **MongoDB Driver**
* Using **FluentValidation** and **AutoMapper**
* Using **Custom Response, Middleware and Exceptions** with Shared Library

#### Basket microservice
* ASP.NET Core Web API application 
* REST API principles, CRUD operations
* **Redis database** connection and containerization
* Using **FluentValidation**
* Using **Custom Response, Middleware and Exceptions** with Shared Library

#### PhotoStock microservice
* ASP.NET Core Web API application 
* REST API principles, CRUD operations
* Using **Custom Response, Middleware and Exceptions** with Shared Library

#### IdentityServer microservice (Not completed yet)
* Using IdentityServer4 (JWT)

#### Asynchronous Communication of Microservices
* Using **MassTransit** for **RabbitMQ** Message-Broker system
* Publishing Checkout command from Basket microservices and Subscribing this message from Order microservices
* Publishing ProductUpdate event from Catalog microservices and Subscribing this event from Order microservices (Eventual Consistency)

#### API Gateway Ocelot Microservice 
* Implement **API Gateways with Ocelot**
* The Gateway aggregation pattern in **Shopping.Aggregator**

#### Microservices Cross-Cutting Implementations
* Implementing Centralized Distributed Logging with **Elasticsearch, Logstash, Kibana and SeriLog** for Microservices

#### WebUI ShoppingApp Microservice
* ASP.NET Core Web Application with Html, CSS and Bootstrap 5.2.3
* Call **Ocelot APIs with HttpClientFactory**
* To register and login using UserManager with IdentityUser (**Temporarily made due to lack of Identity Server**)

#### Docker Compose establishment with all microservices on docker
* Containerization of microservices
* Containerization of databases
* Override Environment variables

## Run The Project

After the cloning this repository run below command at the root directory which include docker-compose.yml files;
```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```
You can launch microservices as below urls:
* **Catalog API -> http://host.docker.internal:5011/swagger/index.html**
* **Order API -> http://host.docker.internal:5012/swagger/index.html**
* **Customer API -> http://host.docker.internal:5013/swagger/index.html**
* **Basket API -> http://host.docker.internal:5014/swagger/index.html**
* **PhotoStock API -> http://host.docker.internal:5020/swagger/index.html**
* **Shopping.Aggregator -> http://host.docker.internal:5001/swagger/index.html**
* **API Gateway -> http://host.docker.internal:5000**
* **RabbitMQ Management Dashboard -> http://host.docker.internal:15672**   -- guest/guest
* **Elasticsearch -> http://host.docker.internal:9200**
* **Kibana -> http://host.docker.internal:5601**
* **Web UI -> http://host.docker.internal:5010**
                                	    
![mainscreen2](https://i.ibb.co/bbpdtsP/Untitled-1.jpg)
