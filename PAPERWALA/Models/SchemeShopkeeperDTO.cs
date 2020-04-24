using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PAPERWALA.Models
{
    [Table("Mst_SchemeShopkeeper")]

    public class SchemeShopkeeperDTO
    {
        [Key]
        public int SchemeId { get; set; }

        [Display(Name = "Scheme Name")]
        [Required(ErrorMessage = "Please Enter Scheme Name")]
        public string SchemeName { get; set; }

        [Display(Name = "Scheme Duration")]
        [Required(ErrorMessage = "Please Enter Scheme Duration")]
        public string SchemeDuration { get; set; }

        [Display(Name = "Scheme Amount")]
        [Required(ErrorMessage = "Please Enter Scheme Amount")]
        public decimal SchemeAmount { get; set; }

        [Display(Name = "Note")]
        public string Note { get; set; }


        [NotMapped]
        public IEnumerable<StateDTO> ListState { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeleteStatus { get; set; }

    }
}