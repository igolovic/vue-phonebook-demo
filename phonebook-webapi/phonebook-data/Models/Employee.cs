using System;
using System.Collections.Generic;

#nullable disable

namespace phonebook_data.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Permissions = new HashSet<Permission>();
        }

        public int Identifier { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int IdentifierRole { get; set; }
        public int IdentifierDepartment { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string JobDetails { get; set; }
        public int IdentifierLocation { get; set; }

        public virtual Department IdentifierDepartmentNavigation { get; set; }
        public virtual Location IdentifierLocationNavigation { get; set; }
        public virtual Role IdentifierRoleNavigation { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
