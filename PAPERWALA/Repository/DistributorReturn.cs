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
    public class DistributorReturn : IDistributorReturn
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());

        public void InsertDistributorReturn(DistributorReturnDTO DistributorReturn)
        {
            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@DistributorReturnId", DistributorReturn.DistributorReturnId); // Normal Parameters  
                para.Add("@ReturnOrder", DistributorReturn.ReturnOrder);
                para.Add("@RetailerId", DistributorReturn.RetailerId);
                para.Add("@TransDate", DistributorReturn.TransDate);
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
                para.Add("@DistributorId", DistributorReturn.CreatedBy);
                para.Add("@CreatedDate", DistributorReturn.CreatedDate);
                para.Add("@CreatedBy", DistributorReturn.CreatedBy);
                //para.Add("@ModifiedDate", DistributorTrans.ModifiedDate);
                //para.Add("@ModifiedBy", DistributorTrans.ModifiedBy);
                para.Add("@DeleteStatus", "ACTIVE");
                var value = con.Query<int>("SP_Insert_MainDistributorReturn", para, null, true, 0, CommandType.StoredProcedure);
            }
            catch (Exception) { throw; }
            finally { con.Close(); }

        }

        public void UpdateDistributorReturn(DistributorReturnDTO DistributorTrans)
        {
            try
            {

                con.Open();
                var para = new DynamicParameters();
                //para.Add("@PaperRate", DistributorTrans.PaperRate);
                //para.Add("@PaperQuantity", DistributorTrans.PaperQuantity);
                //para.Add("@TotalPrice", DistributorTrans.TotalPrice);
                //para.Add("@PamphletQuantity", DistributorTrans.PamphletQuantity);
                //para.Add("@PamphletRate", DistributorTrans.PamphletRate);
                //para.Add("@TotalPamphletAmount", DistributorTrans.TotalPamphletAmount);
                para.Add("@ReturnOrder", HttpContext.Current.Session["ReturnOrder"]);
                para.Add("@TotalFinalAmount", DistributorTrans.TotalFinalAmount);
                para.Add("@PaidAmount", DistributorTrans.PaidAmount);
                para.Add("@BalanceAmount", DistributorTrans.BalanceAmount);
                para.Add("@PrvBalanceAmount", DistributorTrans.PrvBalanceAmount);
                para.Add("@FinalBalaceAmount", DistributorTrans.FinalBalaceAmount);
                para.Add("@DistributorId", DistributorTrans.ModifiedBy);
                para.Add("@ModifiedDate", DistributorTrans.ModifiedDate);
                para.Add("@ModifiedBy", DistributorTrans.ModifiedBy);
                para.Add("@DeleteStatus", "COMPLETE");
                var value = con.Query<int>("SP_Update_MainDistributorReturn", para, null, true, 0, CommandType.StoredProcedure);
                //con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally { con.Close(); }

        }

        public void UpdateDistributorReturnAPI(DistributorReturnDTO DistributorTrans)
        {
            try
            {

                con.Open();
                var para = new DynamicParameters();
                //para.Add("@PaperRate", DistributorTrans.PaperRate);
                //para.Add("@PaperQuantity", DistributorTrans.PaperQuantity);
                //para.Add("@TotalPrice", DistributorTrans.TotalPrice);
                //para.Add("@PamphletQuantity", DistributorTrans.PamphletQuantity);
                //para.Add("@PamphletRate", DistributorTrans.PamphletRate);
                //para.Add("@TotalPamphletAmount", DistributorTrans.TotalPamphletAmount);
                para.Add("@ReturnOrder", DistributorTrans.ReturnOrder);
                para.Add("@TotalFinalAmount", DistributorTrans.TotalFinalAmount);
                para.Add("@PaidAmount", DistributorTrans.PaidAmount);
                para.Add("@BalanceAmount", DistributorTrans.BalanceAmount);
                para.Add("@PrvBalanceAmount", DistributorTrans.PrvBalanceAmount);
                para.Add("@FinalBalaceAmount", DistributorTrans.FinalBalaceAmount);
                para.Add("@DistributorId", DistributorTrans.ModifiedBy);
                para.Add("@ModifiedDate", DistributorTrans.ModifiedDate);
                para.Add("@ModifiedBy", DistributorTrans.ModifiedBy);
                para.Add("@DeleteStatus", "COMPLETE");
                var value = con.Query<int>("SP_Update_MainDistributorReturn", para, null, true, 0, CommandType.StoredProcedure);
                // con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally { con.Close(); }
        }


        public IEnumerable<DistributorReturnDTO> GetDistributorReturns(string DistributorId)
        {
            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@DistributorId", HttpContext.Current.Session["UserID"]); // Normal Parameters  
                var ListDistributorTrans = con.Query<DistributorReturnDTO>("select dt.*,rt.RetailerName from Main_DistributorReturnInfo AS dt LEFT JOIN Mst_Retailer AS rt ON dt.RetailerId=rt.RetailerId where dt.DeleteStatus='ACTIVE' and dt.DistributorId=@DistributorId", para, null, true, 0, CommandType.Text).ToList();
                return ListDistributorTrans;
            }
            catch (Exception e) { throw; }
            finally { con.Close(); }

        }
        public IEnumerable<DistributorReturnDTO> GetDistributorReturnsAPI(string DistributorId)
        {
            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@DistributorId", DistributorId); // Normal Parameters  
                var ListDistributorTrans = con.Query<DistributorReturnDTO>("select dt.*,rt.RetailerName from Main_DistributorReturnInfo AS dt LEFT JOIN Mst_Retailer AS rt ON dt.RetailerId=rt.RetailerId where dt.DeleteStatus='COMPLETE' and dt.DistributorId=@DistributorId", para, null, true, 0, CommandType.Text).ToList();
                return ListDistributorTrans;
            }
            catch (Exception e) { throw; }
            finally { con.Close(); }

        }
        public IEnumerable<DistributorReturnDTO> GetDistributorReturnsTransactionByReturnOrderAPI(string DistributorId, string ReturnOrder)
        {
            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@DistributorId", DistributorId);
                para.Add("@ReturnOrder", ReturnOrder);// Normal Parameters  
                var ListDistributorTrans = con.Query<DistributorReturnDTO>("select dt.*,dp.*,rt.RetailerName from Main_DistrbutorReturnInfo AS dt left join Main_DistributorReturnProduct as dp on dt.ReturnOrder=dp.ReturnOrder LEFT JOIN Mst_Retailer AS rt ON dt.RetailerId=rt.RetailerId where dt.DeleteStatus='COMPLETE' and dt.DistributorId=@DistributorId and dt.ReturnOrder=@ReturnOrder", para, null, true, 0, CommandType.Text).ToList();
                return ListDistributorTrans;
            }
            catch (Exception e) { throw; }
            finally { con.Close(); }

        }
        public void DeleteDistributorReturn(string DistributorReturnId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@DistributorReturnId", DistributorReturnId); // Normal Parameters  
                var value = con.Query("SP_Delete_DistributorReturn", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public void DeleteDistributorReturnProduct(string DistributorReturnProductId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@DistributorReturnProductId", DistributorReturnProductId); // Normal Parameters  
                var value = con.Query("SP_Delete_DistributorReturnProduct", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DistributorReturnDTO> GetRetailerByDistributerId(string DistributorId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@DistributorId", HttpContext.Current.Session["UserID"]);
                var ListRetailer = con.Query<DistributorReturnDTO>("Usp_GetRetailerByDistributor", paramater, null, true, 0, CommandType.StoredProcedure).ToList();
                return ListRetailer;
            }
        }
        //Return Product

        public void InsertDistributorReturnProduct(DistributorReturnProductDTO DistributorReturnProduct)
        {
            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@DistributorReturnProductId", DistributorReturnProduct.DistributorReturnProductId); // Normal Parameters  
                para.Add("@ReturnOrder", DistributorReturnProduct.ReturnOrder);
                para.Add("@PaperId", DistributorReturnProduct.PaperId);
                para.Add("@PaperRate", DistributorReturnProduct.PaperRate);
                para.Add("@PaperQuantity", DistributorReturnProduct.PaperQuantity);
                para.Add("@TotalPrice", DistributorReturnProduct.TotalPrice);
                para.Add("@PamphletQuantity", DistributorReturnProduct.PamphletQuantity);
                para.Add("@PamphletRate", DistributorReturnProduct.PamphletRate);
                para.Add("@TotalPamphletAmount", DistributorReturnProduct.TotalPamphletAmount);
                para.Add("@TotalFinalAmount", DistributorReturnProduct.TotalFinalAmount);
                para.Add("@DistributorId", DistributorReturnProduct.CreatedBy);
                para.Add("@CreatedDate", DistributorReturnProduct.CreatedDate);
                para.Add("@CreatedBy", DistributorReturnProduct.CreatedBy);
                para.Add("@ModifiedDate", DistributorReturnProduct.ModifiedDate);
                para.Add("@ModifiedBy", DistributorReturnProduct.ModifiedBy);
                para.Add("@DeleteStatus", "ACTIVE");
                var value = con.Query<int>("SP_Insert_DistributorReturnProduct", para, null, true, 0, CommandType.StoredProcedure);

            }
            catch (Exception)
            {
                throw;
            }
            finally { con.Close(); }
        }
        public void InsertDistributorReturnProductAPI(DistributorReturnDTO DistributorReturnProduct)
        {
            try
            {

                con.Open();
                var para = new DynamicParameters();
                para.Add("@DistributorReturnProductId", DistributorReturnProduct.DistributorReturnProductId); // Normal Parameters  
                para.Add("@ReturnOrder", DistributorReturnProduct.ReturnOrder);
                para.Add("@PaperId", DistributorReturnProduct.PaperId);
                para.Add("@PaperRate", DistributorReturnProduct.PaperRate);
                para.Add("@PaperQuantity", DistributorReturnProduct.PaperQuantity);
                para.Add("@TotalPrice", DistributorReturnProduct.TotalPrice);
                para.Add("@PamphletQuantity", DistributorReturnProduct.PamphletQuantity);
                para.Add("@PamphletRate", DistributorReturnProduct.PamphletRate);
                para.Add("@TotalPamphletAmount", DistributorReturnProduct.TotalPamphletAmount);
                para.Add("@TotalFinalAmount", DistributorReturnProduct.TotalFinalAmount);
                para.Add("@DistributorId", DistributorReturnProduct.CreatedBy);
                para.Add("@CreatedDate", DistributorReturnProduct.CreatedDate);
                para.Add("@CreatedBy", DistributorReturnProduct.CreatedBy);
                para.Add("@ModifiedDate", DistributorReturnProduct.ModifiedDate);
                para.Add("@ModifiedBy", DistributorReturnProduct.ModifiedBy);
                para.Add("@DeleteStatus", "ACTIVE");
                var value = con.Query<int>("SP_Insert_DistributorReturnProduct", para, null, true, 0, CommandType.StoredProcedure);
                //con.Close();

            }
            catch (Exception)
            {
                throw;
            }
            finally { con.Close(); }
        }


        public IEnumerable<DistributorReturnDTO> GetDistributorReturnProducts(string ReturnOrder)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@ReturnOrder", ReturnOrder); // Normal Parameters  
            var ListDistributorReturnPRoducts = con.Query<DistributorReturnDTO>("select dt.*,p.PaperName from Main_DistributorReturnProduct AS dt left join Mst_Paper as p on dt.PaperId=p.PaperId Where dt.DeleteStatus='ACTIVE' and dt.ReturnOrder=@ReturnOrder", para, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return ListDistributorReturnPRoducts;
        }

        //public void DeleteDistributorReturnProduct(string DistributorReturnProductId)
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
        //    {
        //        var para = new DynamicParameters();
        //        para.Add("@DistributorReturnProductId", DistributorReturnProductId); // Normal Parameters  
        //        var value = con.Query("SP_Delete_DistributorReturnProduct", para, null, true, 0, CommandType.StoredProcedure);
        //    }
        //}

        public IEnumerable<DistributorReturnProductDTO> GetPaperByCityId(string CityId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@CityId", HttpContext.Current.Session["CityID"].ToString());
                var ListPaperbyCityId = con.Query<DistributorReturnProductDTO>("Usp_GetPaperByCityId", paramater, null, true, 0, CommandType.StoredProcedure);
                return ListPaperbyCityId;
            }
        }
        public string GetFinalAmount_By_ReturnOrder(string ReturnOrder)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@ReturnOrder", HttpContext.Current.Session["ReturnOrder"].ToString());
                return con.Query<string>("SP_TotalFinalAmount_by_ReturnOrder_Distributor", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public string GetFinalAmount_By_ReturnOrder_API(string ReturnOrder)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@ReturnOrder", ReturnOrder);
                return con.Query<string>("SP_TotalFinalAmount_by_ReturnOrder_Distributor", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
        }

    }
}