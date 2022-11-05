using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIENDA.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }

    public class UserRegisterModel
    {       
        
        [Required(ErrorMessage ="Por favor escriba el nombre del usuario")]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Por favor escriba los apellidos del usuario")]
        [MaxLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage ="Direccion de correo no válida")]
        [MaxLength(100, ErrorMessage ="Máximo 100 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Campo obligatorio")]
        [MinLength(5, ErrorMessage ="Minimo 5 caracteres")]
        [MaxLength(20, ErrorMessage ="Máximo 20 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [MinLength(5, ErrorMessage = "Minimo 5 caracteres")]
        [MaxLength(20, ErrorMessage = "Máximo 20 caracteres")]
        [Compare("Password", ErrorMessage ="La confirmación debe ser igual a la contraseña")]
        public string ConfirmPassword { get; set; }
    }

    public class UserLoginModel
    {

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Password { get; set; }
    }

    public class ChangePasswordModel
    {

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string NewPassword { get; set; }
    }

    public class RecoverPasswordModel
    {

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Email { get; set; }
    }
}
