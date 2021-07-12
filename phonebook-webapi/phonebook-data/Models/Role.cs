using System;
using System.Collections.Generic;

#nullable disable

namespace phonebook_data.Models
{
    public partial class Role
    {
        public Role()
        {
            Employees = new HashSet<Employee>();
            Permissions = new HashSet<Permission>();
        }

        public int Identifier { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
