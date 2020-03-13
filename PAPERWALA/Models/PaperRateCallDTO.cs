using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PAPERWALA.Models
{
    [Table("Mst_PaperRate")]
    public class PaperRateCallDTO
    {
        [Display(Name = " Paper Rate Trans")]
        public double PaperRateForTrans { get; set; }
    }
}