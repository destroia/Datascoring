using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDto
{
    public class ChangedPasswordDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
