# Simple-Blog

Application with onion architecture.
It uses Entity Framework (Code First) for creating DB with
business models and seed some beginning data. 
For CRUD operations in controllers with data uses UnitOfWork pattern.
Dependecy Resolver - Autofac.
Application provides simple authentication with authentication mode="Forms".
Passwords are hashed by SHA256 and save result in DB.
