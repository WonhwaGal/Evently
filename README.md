<a href="https://github.com/WonhwaGal/Evently/blob/EventlyMain/README_RU.md" target="_blank"><img src="/readme_resources/Git hub button.png" alt="К русской версии" height="45" width="195"></a>
<hr>
<h1>Evently</h1>
<h5>August 2024</h5>

It is a full-featured web service for a ticket marketplace, built on an event-driven architecture. 
It offers complete functionality for managing social events and tickets, statistics collection and processing purchases/cancellations. 
The platform includes secure user authorization, inter-module communication, telemetry and a reverse proxy layer.

<h3>Features:</h3>

- Based on event-driven architecture with each module following Clean Architecture principles;
- **Identity** system via **KeyCloak**;
- **Patterns:** CQRS, IUnitOfWork, Repository, Inbox/Outbox, Saga, Decorator;
- Problems of exception handling, validation, caching and logging are solved with MediatR pipeline behavior interface;
- Uses domain and integration events and interceptors;
- **FluentValidation, Redis, Serilog, AutoMapper, KeyCloak, MassTransit, RabbitMQ, MongoDB, OpenTelemetry, Yarp, Scrutor** libraries;
- Includes basic unit testing as well as integration and architectural testing.

<br>Initially developed as modular monolith and then implemented transition to RabbitMQ and now moving towards microservices architecture.

<h4>Example of common logging behaviour:</h4>
<img align="center" width="80%" src="/readme_resources/bahaviour.png">
<h4>Interceptor example:</h4>
<img align="center" width="80%" src="/readme_resources/Intercaptor.png">
<h4>Idempotent handler for Inbox/Outbox pattern:</h4>
<img align="center" width="80%" src="/readme_resources/idempotentHandler.png">
<h4>Endpoint implementing a custom IEndpoint interface:</h4>
<img align="center" width="80%" src="/readme_resources/Endpoint_example2.png">
<h4>Result extention provides common logic for handling success and failure results:</h4>
<img align="center" width="80%" src="/readme_resources/ResultExtention.png">