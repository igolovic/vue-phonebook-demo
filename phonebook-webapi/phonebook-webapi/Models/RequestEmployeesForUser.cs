using System.ComponentModel.DataAnnotations;

namespace phonebook_webapi.Models
{
    public class RequestEmployeesForUser
    {
        [Required]
        public int IdentifierUser { get; set; }
    }
}
