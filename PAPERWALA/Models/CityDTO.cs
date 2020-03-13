using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PAPERWALA.Models
{
    [Table("Mst_City")]
    public class CityDTO
    {
        [Key]
        public int CityId { get; set; }

        [Display(Name = "State Name")]
        [Validate_Depot(ErrorMessage = "Select State")]
        public int? StateId { get; set; }
        [Display(Name ="State Name")]
        public string StateName { get; set; }

        [Display(Name ="City Name")]
        [Required(ErrorMessage ="Please Enter City Name")]
        [Remote("CityNameExists", "City", HttpMethod = "POST", ErrorMessage = "City Name Already Exists ")]
        public string CityName { get; set; }

        [NotMapped]
        public IEnumerable<StateDTO> ListState { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeleteStatus { get; set; }
    }
    public class Validate_Depot : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == null)
                return false;
            else
                return true;
        }
    }
}