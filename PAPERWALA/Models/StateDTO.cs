using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace PAPERWALA.Models
{
    [Table("Mst_State")]
    public class StateDTO
    {
        [Key]
        public int StateId { get; set; }

        [Display(Name ="State Name")]
        [Required(ErrorMessage = "Please enter State Name")]
        [Remote("StateNameExists", "Sate", HttpMethod = "POST", ErrorMessage = "State Name Already Exists ")]
        public string StateName { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeleteStatus { get; set; }
    }
}