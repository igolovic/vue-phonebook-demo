# poc-phonebook-vui
**POC of phonebook implemented in Vue and .NET Core Web API**

This is a POC application consisting of frontend written in Vue.JS which calls web API written in .NET Core, data is stored in a relational database on SQL Server instance.   

    
**Frontend**   
    
Front end features a phonebook-like list of contacts with toolbar consisting of pagination controls (back/next buttons and page size selection), filter, and phonebook operator selection (this was implemented instead of phonebook operator's authentication/authorization since that was outside of scope of this POC). To set up application structure Vue-CLI was used. Bootstrap-Vue CSS library was used to achieve better visual appearance. Pagination, sorting, and filtering were "hand-made" implemented (not used from e.g. Bootstrap). Frontend application is in folder phonebook-client.          
Front-end screenshot:   
   
![screenshot](./frontend.png?raw=true)   
   
   
**Backend**   
    
It consists of APIs which are called from frontend using GET or POST methods:   
   
![screenshot](./backend.png?raw=true)   
   
https://localhost:44354/EmployeesWithPermissions - GET method returns a list of employees for which the backend determines that they have access rights to employees' contacts. This method returns a list of phonebook operators. From this list "Application operators" dropdown is populated. This method is called during inital loading of the application.   
   
https://localhost:44354/Employees - POST method returns a list of employees according to input parameter which is an identifier of phonebook operator, from this identifier backend determines to which employees' contacts an operator has access rights. From this list a grid with employees' contacts is populated.   

Backend application is in folder phonebook-webapi which contains Visual Studio solution with three projects. Service methods from project phonebook-webapi further call into projects phonebook-core and phonebook-data which implement layered architecture (presentation/business/data). Entity Framework is used to retrieve data from the database.
   
   
**Database**   
    
Database is implemented as a relational database hosted on SQL Server instance.    
Tables:   
_Employee_ - all employees including phonebook operators (which are also employees) are stored in this table   
_Department_ - codebook of departments in company (Sales, Manufacturing)   
_Location_ - codebook of company locations (Athens, Madrid)    
_Role_- codebook of employee roles (Worker, Manager)    
_Permission_ - one row in this table represents access right of an employee to department and role. For example:    
   
![screenshot](./permission.png?raw=true)
   
These records mean that employee with identifier 7 has access rights to employees from departments 1 and 2 for both roles 1 and 2.
   
Foreign key relationship diagram:
   
![screenshot](./sql-diagram.png?raw=true)
