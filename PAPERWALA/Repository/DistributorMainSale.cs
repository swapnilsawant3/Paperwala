using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using PAPERWALA.Models;
using System.Configuration;
using System.Data;

namespace PAPERWALA.Repository
{
    public class DistributorMainSale : IDistributorMainSale
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());

        public void InsertDistributorSale(DistributorMainSaleDTO Distributormainsale)
        {
            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@DistributorSaleId", Distributormainsale.DistributorSaleId); // Normal Parameters  
                para.Add("@SaleOrder", Distributormainsale.SaleOrder);
                para.Add("@RetailerId", Distributormainsale.RetailerId);
                para.Add("@TransDate", Distributormainsale.TransDate);
                //para.Add("@PaperRate", DistributorTrans.PaperRate);
                //para.Add("@PaperQuantity", DistributorTrans.PaperQuantity);
                //para.Add("@TotalPrice", DistributorTrans.TotalPrice);
                //para.Add("@PamphletQuantity", DistributorTrans.PamphletQuantity);
                //para.Add("@PamphletRate", DistributorTrans.PamphletRate);
                //para.Add("@TotalPamphletAmount", DistributorTrans.TotalPamphletAmount);
                //para.Add("@TotalFinalAmount", DistributorTrans.TotalFinalAmount);
                //para.Add("@PaidAmount", DistributorTrans.PaidAmount);
                //para.Add("@BalanceAmount", DistributorTrans.BalanceAmount);
                //para.Add("@PrvBalanceAmount", DistributorTrans.PrvBalanceAmount);
                //para.Add("@FinalBalaceAmount", DistributorTrans.FinalBalaceAmount);
                para.Add("@DistributorId", Distributormainsale.CreatedBy);
                para.Add("@CreatedDate", Distributormainsale.CreatedDate);
                para.Add("@CreatedBy", Distributormainsale.CreatedBy);
                //para.Add("@ModifiedDate", DistributorTrans.ModifiedDate);
                //para.Add("@ModifiedBy", DistributorTrans.ModifiedBy);
                para.Add("@DeleteStatus", "ACTIVE");
                var value = con.Query<int>("SP_Insert_MainDistributorSale", para, null, true, 0, CommandType.StoredProcedure);
            }
            catch (Exception) { throw; }
            finally { con.Close(); }

        }
    }
}