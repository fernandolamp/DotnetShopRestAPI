using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve ter no máximo 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve ter entre 3 e 20 caracters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve ter no máximo 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve ter entre 3 e 20 caracters")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
