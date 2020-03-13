using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PAPERWALA.Models
{
    [Table("Mst_Designation")]
    public class DesignationDTO
    {
        [Key]
        public int DesignationId { get; set; }

        [Display(Name = "Designation")]
        [Required(ErrorMessage = "Please enter Designation ")]
        [Remote("DesignationNameExists", "Sate", HttpMethod = "POST", ErrorMessage = "Designation Name Already Exists ")]
        public string Designation { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeleteStatus { get; set; }
    }
}