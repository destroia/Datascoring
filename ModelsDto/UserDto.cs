using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDto
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200,MinimumLength = 1,ErrorMessage ="El campo email es requerido")]
        public string  Email { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "El campo nombre es requerido")]
        public string Name { get; set; }
        [StringLength(200, MinimumLength = 1, ErrorMessage = "El campo apellido es requerido")]
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
