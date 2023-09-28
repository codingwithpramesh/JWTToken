using System.ComponentModel.DataAnnotations;

namespace PortFolio.Models.DTO
{
    public class RegistrationModel
    {
        [Required (ErrorMessage ="Username is Required")]
        public string  Username { get; set; }

        [Required (ErrorMessage ="Email is Required")]
        public string Email { get; set; }

        [Required (ErrorMessage ="password is required")]
        public string Password { get; set; }
    }
}
