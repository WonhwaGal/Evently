<a href="https://github.com/WonhwaGal/Evently/blob/EventlyMain/README_RU.md" target="_blank"><img src="/readme_resources/Git hub button.png" alt="К русской версии" height="45" width="195"></a>
<hr>
<h1>Evently</h1>
<h5>August 2024</h5>

This is a clean architecture web service for an ticket marketplace.<br>
It allows to create and modify events, their categories as well as assign different tickets to them.<br> 
For the users there is an option of registering, adding tickets to the cart or removing them from the cart, and to get the current cart content.

<h3>Features:</h3>

- Implements **Modular Monolith** architectural pattern;
- Uses **Clean Architecture**;
- **Identity** system via **KeyCloak**;
- **Patterns:** CQRS, IUnitOfWork, Repository, Assembly reference;
- Problems of exception handling, validation, caching and logging are solved with MediatR pipeline behavior interface;
- Uses domain and integration events and interceptors;
- **FluentValidation, Redis, Serilog, AutoMapper, KeyCloak** libraries;
- Intermodule communication via PublicApi and event bus (**MassTransit**);

<br>Initially developed with intermodule communication via domain events and PublicApi.
<br>Then PublicApi was replaced by Integration events.

<h4>Common logging behaviour:</h4>
<img align="center" width="80%" src="/readme_resources/bahaviour.png">
<h4>Interceptor:</h4>
<img align="center" width="80%" src="/readme_resources/Intercaptor.png">
<h4>Endpoint example:</h4>
<img align="center" width="80%" src="/readme_resources/Endpoint_example.png">
<h4>Result extention provides common logic for handling success and failure results:</h4>
<img align="center" width="80%" src="/readme_resources/ResultExtention.png">
