using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PAPERWALA.Models
{
    [Table("Mst_Line")]
    public class LineDTO
    {
        [Key]
        public int LineId { get; set; }
        [Display(Name = "Line Name")]
        [Required(ErrorMessage = "Please Enter Line Name")]
        public string LineName { get; set; }

        [Display(Name = "State Name")]
        [Validate_Depot(ErrorMessage = "Select State")]
        public int? StateId { get; set; }
        [Display(Name = "State Name")]
        public string StateName { get; set; }

        public int? CityId { get; set; }

        [Display(Name = "City Name")]
        [Required(ErrorMessage = "Please Enter City Name")]
       
        public string CityName { get; set; }
        public int? ShopkeeperId { get; set; }
        [NotMapped]
        public IEnumerable<StateDTO> ListState { get; set; }
        public IEnumerable<CityDTO> ListCity { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeleteStatus { get; set; }
    }
}