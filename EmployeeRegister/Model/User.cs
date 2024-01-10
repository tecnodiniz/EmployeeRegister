using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRegister.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string userLogin { get; set; }

        [Required]
        [MaxLength(32)]
        public string UserEmail { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserPassword { get; set; }

        public ICollection<Employee> Employees{ get; set; }

    }
}
