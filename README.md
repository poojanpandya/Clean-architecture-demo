# Clean-architecture-demo
* This Project is basically based on clean structure (onion structure) using .NET Core 3.1.
In this project I have Implemented some Basic APIs and Admin Panel as well.

* Implemented Authentication.

1. JWT Authentication For - web APIs
2. Identity Authentication For - Web Admin

* on API side I have used dapper for crud operations.
  I have used dapper generic method approach for crud opeartions.

* on Admin Side i have implemented Role based authentication functionality. You can change specific roles for users.

Structure Information As shown Below.

1. CoreAppDemo.MVC
 => our Main Project Which Includes Web Admin and Web APIs.
2. CoreApp.Application
=> Class Library For Service layer (interect with controller.)
3. CoreApp.Domain
=> Class Library For Entities.
4. CoreApp.Infra.Data
=> Class Library For Database Operations (dapper,entityframework).
5. CoreApp.Infrastructure.IoC
=> Class Library Registering Service (Inversion Of Control).

For Database Use Migration in CoreApp.Infrastructure.Data Project.
