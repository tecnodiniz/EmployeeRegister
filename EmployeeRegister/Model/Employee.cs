using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRegister.Model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId {  get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        [Required]
        [MaxLength(32)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        public string Address { get; set; }

        public User User { get; set; }

    }
}
