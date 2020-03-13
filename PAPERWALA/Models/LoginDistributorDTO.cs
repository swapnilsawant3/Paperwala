using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PAPERWALA.Models
{
    public class LoginDistributorDTO
    {
        [Required(ErrorMessage = "Enter Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter User name")]
        public string UserName { get; set; }

        public int StateId { get; set; }
        public int CityId { get; set; }
    }
}