using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using phonebook_data.Models;

namespace phonebook_data
{
    public class Database
    {
        public static List<Employee> GetEmployeesWithPermissions()
        {
            using (var context = new phonebookContext())
            {
                var employees = (from e in context.Employees
                                    .Include(emp => emp.IdentifierLocationNavigation)
                                    .Include(emp => emp.IdentifierDepartmentNavigation)
                                    .Include(emp => emp.IdentifierRoleNavigation)
                                 where context.Permissions.Any(p => p.IdentifierEmployee == e.Identifier)
                                 select e)
                                 .ToList();

                return employees;
            }
        }

        public static List<Employee> GetEmployees()
        {
            using (var context = new phonebookContext())
            {
                var employees = (from e in context.Employees
                                    .Include(emp => emp.IdentifierLocationNavigation)
                                    .Include(emp => emp.IdentifierDepartmentNavigation)
                                    .Include(emp => emp.IdentifierRoleNavigation)
                                 select e)
                                 .ToList();

                return employees;
            }
        }

        /// <summary>
        /// Return employees for which the user has access right to retrieve them, based on role and department
        /// </summary>
        /// <param name="identifierUser"></param>
        /// <returns></returns>
        public static List<Employee> GetEmployeesForOperator(int identifierUser)
        {
            using (var context = new phonebookContext())
            {
                var employee = (from e in context.Employees
                                    .Include(emp => emp.IdentifierLocationNavigation)
                                    .Include(emp => emp.IdentifierDepartmentNavigation)
                                    .Include(emp => emp.IdentifierRoleNavigation)
                                join p in context.Permissions on e.IdentifierDepartment equals p.IdentifierDepartment
                                where e.IdentifierRole == p.IdentifierRole && p.IdentifierEmployee == identifierUser
                                select e)
                                .ToList();

                return employee;
            }
        }
    }
}
