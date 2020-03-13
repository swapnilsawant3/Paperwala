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
    public class DistributorTransactions : IDistributorTransactions
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());

        public void InsertDistributorTransaction(DistributorTransactionDTO DistributorTrans)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@TransDistributorId", DistributorTrans.TransDistributorId); // Normal Parameters  
            para.Add("@RetailerId", DistributorTrans.RetailerId);
            para.Add("@PaperId", DistributorTrans.PaperId);
            para.Add("@TransDate", DistributorTrans.TransDate);
            para.Add("@PaperRate", DistributorTrans.PaperRate);
            para.Add("@PaperQuantity", DistributorTrans.PaperQuantity);
            para.Add("@TotalPrice", DistributorTrans.TotalPrice);
            para.Add("@PamphletQuantity", DistributorTrans.PamphletQuantity);
            para.Add("@PamphletRate", DistributorTrans.PamphletRate);
            para.Add("@TotalPamphletAmount", DistributorTrans.TotalPamphletAmount);
            para.Add("@TotalFinalAmount", DistributorTrans.TotalFinalAmount);
            para.Add("@PaidAmount", DistributorTrans.PaidAmount);
            para.Add("@BalanceAmount", DistributorTrans.BalanceAmount);
            para.Add("@PrvBalanceAmount", DistributorTrans.PrvBalanceAmount);
            para.Add("@FinalBalaceAmount", DistributorTrans.FinalBalaceAmount);
            para.Add("@DistributorId", DistributorTrans.CreatedBy);
            para.Add("@CreatedDate", DistributorTrans.CreatedDate);
            para.Add("@CreatedBy", DistributorTrans.CreatedBy);
            para.Add("@ModifiedDate", DistributorTrans.ModifiedDate);
            para.Add("@ModifiedBy", DistributorTrans.ModifiedBy);
            para.Add("@DeleteStatus", "ACTIVE");
            var value = con.Query<int>("SP_Insert_Update_DistributorTransaction", para, null, true, 0, CommandType.StoredProcedure);
            con.Close();
        }
        public IEnumerable<DistributorTransactionDTO> GetDistributorTransactions(string DistributorId)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@DistributorId", HttpContext.Current.Session["UserID"]); // Normal Parameters  
            var ListDistributorTrans = con.Query<DistributorTransactionDTO>("select dt.*,p.PaperName from Trans_DistrbutorTransaction AS dt LEFT JOIN Mst_Retailer AS rt ON dt.RetailerId=rt.RetailerId  left join Mst_Paper as p on dt.PaperId=p.PaperId Where dt.DeleteStatus='ACTIVE' and dt.DistributorId=@DistributorId", para, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return ListDistributorTrans;
        }

        public void DeleteDistributorTransaction(string TransDistributorId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@TransDistributorId", TransDistributorId); // Normal Parameters  
                var value = con.Query("SP_Delete_DistributorTransaction", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DistributorTransactionDTO> GetRetailerByDistributerId(string DistributorId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@DistributorId", HttpContext.Current.Session["UserID"]);
                var ListRetailer = con.Query<DistributorTransactionDTO>("Usp_GetRetailerByDistributor", paramater, null, true, 0, CommandType.StoredProcedure).ToList();
                return ListRetailer;
            }
        }

        public IEnumerable<DistributorTransactionDTO>GetPaperByCityId(string CityId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@CityId", HttpContext.Current.Session["CityID"].ToString());
                var ListPaperbyCityId = con.Query<DistributorTransactionDTO>("Usp_GetPaperByCityId", paramater, null, true, 0, CommandType.StoredProcedure);
                return ListPaperbyCityId;
            }
        }

       
    }
}