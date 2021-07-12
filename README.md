# poc-phonebook-vui
**POC of phonebook implemented in Vue and .NET Core Web API**

This is a POC application consisting of front-end written in Vue.JS which calls web API written in .NET Core, data is stored in a database on SQL Server instance.   

**Front end** features a phonebook-like list of contacts with toolbar consisting of pagination controls (back/next buttons and page size selection), filter, and phonebook operator selection (this was implemented instead of phonebook operator's authentication/authorization since that was outside of scope of this POC).   
Front-end screenshot:   
![screenshot](./frontend.png?raw=true)   
   
**Backend** consists of APIs which are called from frontend using GET or POST methods:   
![screenshot](./backend.png?raw=true)   

https://localhost:44354/EmployeesWithPermissions - GET method returning employees for which the backend determines that they ave access right to any of the employees' contacts, this method returns list of phonebook operators. From this list "Application operators" dropdown is populated.   
   
https://localhost:44354/Employees - POST method returning a list of employees' phonebook contacts according to parameter which is identifier of phonebook operator, from this identifier backend determines to which employee contacts an operator has access right. From this list a grid with employees' contacts is populated.   

**Database** is implemented as a relational database hosted on SQL Server instance.    
Tables:   
Employee - all employees including phonebook operators (which are also employees) are stored in this table   
Department - codebook of departments in company (Sales, Manufacturing)   
Location - codebook of company locations (Athens, Madrid)    
Role - codebook of employee roles (Worker, Manager)    
Permission - one row in this table represents access right of an employee to department and role. For example:    

![screenshot](./permission.png?raw=true)

Means that employee with identifier 7 has access rights to employee contacts from departments 1 and 2 for both roles 1 and 2.

Foreign key relationship diagram:
![screenshot](./sql-diagram.png?raw=true)
