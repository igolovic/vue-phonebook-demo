using System;
using System.Linq;
using System.Collections.Generic;

using phonebook_data;
using phonebook_data.Models;
using phonebook_core.BusinessObjects;

namespace phonebook_core
{
    public class EmployeeManager
    {
        /// <summary>
        /// Return employees who are operators of phonebook
        /// </summary>
        /// <returns></returns>
        public static List<EmployeeItem> GetEmployeesWithPermissions()
        {
            var employees = Database.GetEmployeesWithPermissions();
            return CreateEmployeeItems(employees);
        }

        /// <summary>
        /// Return all employees
        /// </summary>
        /// <returns></returns>
        public static List<EmployeeItem> GetEmployees()
        {
            var employees = Database.GetEmployees();
            return CreateEmployeeItems(employees);
        }

        /// <summary>
        /// Return employees for which the user has access right to retrieve them, based on role and department
        /// </summary>
        /// <param name="identifierUser"></param>
        /// <returns></returns>
        public static List<EmployeeItem> GetEmployees(int identifierUser)
        {
            var employees = Database.GetEmployeesForOperator(identifierUser);
            return CreateEmployeeItems(employees);
        }

        private static List<EmployeeItem> CreateEmployeeItems(List<Employee> employees)
        {
            return (from e in employees
                    select new EmployeeItem()
                    {
                        Identifier = e.Identifier,
                        Name = e.Name,
                        LastName = e.LastName,
                        IdentifierRole = e.IdentifierRole,
                        RoleName = e.IdentifierRoleNavigation.Name,
                        IdentifierDepartment = e.IdentifierDepartment,
                        DepartmentName = e.IdentifierDepartmentNavigation.Name,
                        MobileNumber = e.MobileNumber,
                        Email = e.Email,
                        JobDetails = e.JobDetails,
                        IdentifierLocation = e.IdentifierLocation,
                        LocationName = e.IdentifierLocationNavigation.Name
                    })
                    .ToList();
        }
    }
}
