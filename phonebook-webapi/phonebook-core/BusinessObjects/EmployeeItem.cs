using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace phonebook_core.BusinessObjects
{
    public partial class EmployeeItem
    {
        public EmployeeItem()
        {
        }

        public int Identifier { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public int IdentifierRole { get; set; }
        public string RoleName { get; set; }
        [JsonIgnore]
        public int IdentifierDepartment { get; set; }
        public string DepartmentName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string JobDetails { get; set; }
        [JsonIgnore]
        public int IdentifierLocation { get; set; }
        public string LocationName { get; set; }
    }
}
