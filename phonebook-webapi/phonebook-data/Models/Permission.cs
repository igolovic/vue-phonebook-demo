using System;
using System.Collections.Generic;

#nullable disable

namespace phonebook_data.Models
{
    public partial class Permission
    {
        public int Identifier { get; set; }
        public int IdentifierEmployee { get; set; }
        public int IdentifierDepartment { get; set; }
        public int IdentifierRole { get; set; }

        public virtual Department IdentifierDepartmentNavigation { get; set; }
        public virtual Employee IdentifierEmployeeNavigation { get; set; }
        public virtual Role IdentifierRoleNavigation { get; set; }
    }
}
