using System;
using System.Collections.Generic;

#nullable disable

namespace phonebook_data.Models
{
    public partial class Location
    {
        public Location()
        {
            Employees = new HashSet<Employee>();
        }

        public int Identifier { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
