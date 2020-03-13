using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PAPERWALA.Models
{
    [Table("Mst_Paper")]
    public class PaperDTO
    {
        [Key]
        public int PaperId { get; set; }

        [Display(Name = "State Name")]
        [Validate_Paper(ErrorMessage = "Select State")]
        public int? StateId { get; set; }
        [Display(Name = "State Name")]
        public string StateName { get; set; }

        [Display(Name = "City Name")]
        [Validate_Paper(ErrorMessage = "Select City")]
        public int? CityId { get; set; }
        [Display(Name = "City Name")]
        public string CityName { get; set; }


        [Display(Name = "Paper Name")]
        [Required(ErrorMessage = "Please Enter Paper Name")]
        [Remote("PaperNameExists", "Paper", HttpMethod = "POST", ErrorMessage = "Paper Name Already Exists ")]
        public string PaperName { get; set; }

        [Display(Name = "Language")]
        public string Language { get; set; }

        [NotMapped]
        public IEnumerable<StateDTO> ListState { get; set; }

        [NotMapped]
        public IEnumerable<CityDTO> ListCity { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeleteStatus { get; set; }
    }
    public class Validate_Paper : ValidationAttribute
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