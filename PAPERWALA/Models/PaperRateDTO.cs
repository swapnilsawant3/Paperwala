using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PAPERWALA.Models
{
    [Table("Mst_PaperRate")]
    public class PaperRateDTO
    {
        [Key]
        public int PaperRateId { get; set; }

        [Display(Name = "Paper Name")]
        [Validate_PaperRate(ErrorMessage = "Select Paper")]
        public int? PaperId { get; set; }
        [Display(Name = "Paper Name")]
        public string PaperName { get; set; }

        [Display(Name = "City Name")]
        [Validate_PaperRate(ErrorMessage = "Select City")]
        public int? CityId { get; set; }
        [Display(Name = "City Name")]
        public string CityName { get; set; }

        [Display(Name = "State Name")]
        [Validate_PaperRate(ErrorMessage = "Select Sate")]
        public int? StateId { get; set; }
        [Display(Name = "State Name")]
        public string StateName { get; set; }

        [Display(Name = " Monday Rate")]
        [Required(ErrorMessage = "Please Enter Monday Rate")]
        public double MondayRate { get; set; }

        [Display(Name = " Tuesday Rate")]
        [Required(ErrorMessage = "Please Enter Tuesday Rate")]
        public double TuesdayRate { get; set; }

        [Display(Name = " Wednesday Rate")]
        [Required(ErrorMessage = "Please Enter Wednesday Rate")]
        public double WednesdayRate { get; set; }

        [Display(Name = " Thursday Rate")]
        [Required(ErrorMessage = "Please Enter Thursday Rate")]
        public double ThursdayRate { get; set; }

        [Display(Name = " Friday Rate")]
        [Required(ErrorMessage = "Please Enter Friday Rate")]
        public double FridayRate { get; set; }

        [Display(Name = " Saturday Rate")]
        [Required(ErrorMessage = "Please Enter Saturday Rate")]
        public double SaturdayRate { get; set; }

        [Display(Name = " Sunday Rate")]
        [Required(ErrorMessage = "Please Enter Sunday Rate")]
        public double SundayRate { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeleteStatus { get; set; }

        [NotMapped]
        public IEnumerable<PaperDTO> ListPaper { get; set; }
        public IEnumerable<StateDTO> ListState { get; set; }
        public IEnumerable<CityDTO> ListCity { get; set; }

       
    }

    public class Validate_PaperRate : ValidationAttribute
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