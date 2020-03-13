using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PAPERWALA.Models
{
    public class DistributorMainSaleDTO
    {
        [Key]

        public int DistributorSaleId { get; set; }
        [Display(Name = "Sale Order")]
        [Required(ErrorMessage = "Please Enter Sale Order")]
        public string SaleOrder { get; set; }
        [Display(Name = "Retailer Name")]
        [Validate_Main_DistributorSale(ErrorMessage = "Select Paper")]
        public int RetailerId { get; set; }
        [Display(Name = "Retailer Name")]
        public string RetailerName { get; set; }

        [Display(Name = "Transaction Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please Enter Transaction Date")]
        public DateTime TransDate { get; set; }
        public int DistributorId { get; set; }
        

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeleteStatus { get; set; }

    }
}