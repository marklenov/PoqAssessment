# POQ Assessment (.NET)

Application with REST API endpoint to return products and with ability to filter them

## Tools needed

Visual Studio 2022
.NET 6 SDK 

## How to test

1. Restore Nuget packages if needed. 
2. Build and run. 
3. Once application is run, Swagger will be opened in a browser, if not, go to: 
	https://localhost:7076/swagger/index.html
4. Endpoint named "Product" will be shown. 
5. 'Try it out' and enter paremeters and 'Execute'. 

## Extra: 

Technologies used: 
.NET 6, Swagger, DI, CORS, Automapper, Newtonsoft, GlobalExceptionHandling, Logging, RateLimit, xUnit

What is added to prevent vulnerabilities: 
1. CORS
2. API Rate limiting

Foldering: 
Solution Contains self-descriptive folders

Features:
IProductService is used to filter products with given parameters
IProductClientService is used to fetch data from external API

Technologies not inlcuded: 
Those technoligies were not used due to their possible redundancy in this small app: 
1. Polly
2. RepositoryPattern
3. In memory cache with decorator pattern
4. MediatR could be used instead of IServiceHandler
5. Authorization (Task requirements)

