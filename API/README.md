
# .NET Core Accounts Manager Project

There are 2 Projects for Backend of Account Manager Project:
1) In InMemoryCacheAPI folder there is AccountManagement API that uses System.Runtime.Caching for Cache for storing the information
   
2) In RedisCacheAPI folder, there is  Account ManagementRedis API  that uses StackExchange.Redis for storing the information
   
This leverages .NET 6. You can learn .NET 6 more on [ASP.NET Core minimal APIs](https://www.dotnetthailand.com/web-frameworks/asp-net-core/asp-net-core-minimal-apis).

## Versioning
| GitHub Release | .NET Core Version | Diagnostics HealthChecks Version |
|----------------|------------ |---------------------|
| main | 6.0.100-preview.6.21355.2 | 2.2.0 |

## Project Structure
```
├── Constants
│   └── CacheKeys.cs
├── Controllers
│   └── AccountController.cs
│   └── CustomerController.cs
├── CustomException
│   └── CustomerNotFoundException.cs
├── dto
│   └── AccountDto.cs
│   └── CreateAccountRequest.cs
│   └── CustomerDto.cs
│   └── TransactionDto.cs
├── Helpers
│   └── MappingProfiles.cs
├── Dockerfile
├── Models
│   └── Account.cs
│   └── Customer.cs
│   └── Transaction.cs
├── Program.cs
├── Properties
│   └── launchSettings.json
├── Services
│   ├── Caching 
│   └── AccountService.cs
│   └── CustomerService.cs
│   └── TransactionService.cs
├── customers.json
├── ServiceRegistration.cs
├── docker-compose.yml
├── bin
│   └── Debug
├── configs
│   └── prod
├── AccountManagementRedis.csproj
├── Account ManagementRedis.sln
├── manifests
│   ├── deployment.yaml
│   └── service.yaml
```

- `Dockerfile` is .NET Core Web API Multistage Dockerfile (following Docker Best Practices)
- `KubernetesLocalProcessConfig.yaml` is [Bridge to Kubernetes](https://devblogs.microsoft.com/visualstudio/bridge-to-kubernetes-ga/) config to supports developing .NET Core Web API microservice on Kubernetes
- `configs` folder will contain .NET Core Web API centralized config structure
- `appsettings.Development.json` is .NET Core Web API development environment config
- `manifests` folder will contain Kubernetes manifests (deployment, service)
- `Program.cs` is .NET Core Web API environment variable mapping config 

## Setting Up

To setup this project, you need to clone the git repo

```sh
$ git clone https://github.com/miscociluca/Accounts-Manager.git
$ cd API/InMemoryCacheAPI
OR
$ cd API/RedisCacheAPI
depending on which backend project you want to run
```

followed by

```sh
$ docker-compose up
```

## Testing 
To test this projects, you need to 
```sh
$ cd API\UnitTest\TestAccountManagement
```
followed by

```sh
$ dotnet test
```

## Deploying .NET Core API  on Kubernetes

### Prerequisite:

- .NET Core Web API Docker Image

Preparing Config Map for .NET Core Web API microservice

```sh
$ kubectl apply -k configs/prod
```

To deploy the microservice on Kubernetes, run following command:

```sh
$ kubectl apply -f manifests
```

This will deploy it on Kubernetes with the centralized config.
