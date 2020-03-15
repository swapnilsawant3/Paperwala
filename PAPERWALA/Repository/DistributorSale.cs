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
    public class DistributorSale : IDistributorSale
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());

        public void InsertDistributorSale(DistributorSaleDTO Distributorsale)
        {
            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@DistributorSaleId", Distributorsale.DistributorSaleId); // Normal Parameters  
                para.Add("@SaleOrder", Distributorsale.SaleOrder);
                para.Add("@RetailerId", Distributorsale.RetailerId);
                para.Add("@TransDate", Distributorsale.TransDate);
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
                para.Add("@DistributorId", Distributorsale.CreatedBy);
                para.Add("@CreatedDate", Distributorsale.CreatedDate);
                para.Add("@CreatedBy", Distributorsale.CreatedBy);
                //para.Add("@ModifiedDate", DistributorTrans.ModifiedDate);
                //para.Add("@ModifiedBy", DistributorTrans.ModifiedBy);
                para.Add("@DeleteStatus", "ACTIVE");
                var value = con.Query<int>("SP_Insert_MainDistributorSale", para, null, true, 0, CommandType.StoredProcedure);
            }
            catch (Exception) {  throw; }
            finally { con.Close(); }
            
        }

        public void UpdateDistributorSale(DistributorSaleDTO DistributorTrans)
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
                para.Add("@SaleOrder", HttpContext.Current.Session["SaleOrder"]);
                para.Add("@TotalFinalAmount", DistributorTrans.TotalFinalAmount);
                para.Add("@PaidAmount", DistributorTrans.PaidAmount);
                para.Add("@BalanceAmount", DistributorTrans.BalanceAmount);
                para.Add("@PrvBalanceAmount", DistributorTrans.PrvBalanceAmount);
                para.Add("@FinalBalaceAmount", DistributorTrans.FinalBalaceAmount);
                para.Add("@DistributorId", DistributorTrans.ModifiedBy);
                para.Add("@ModifiedDate", DistributorTrans.ModifiedDate);
                para.Add("@ModifiedBy", DistributorTrans.ModifiedBy);
                para.Add("@DeleteStatus", "COMPLETE");
                var value = con.Query<int>("SP_Update_MainDistributorSale", para, null, true, 0, CommandType.StoredProcedure);
                //con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally { con.Close(); }

        }

        public void UpdateDistributorSaleAPI(DistributorSaleDTO DistributorTrans)
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
                para.Add("@SaleOrder", DistributorTrans.SaleOrder);
                para.Add("@TotalFinalAmount", DistributorTrans.TotalFinalAmount);
                para.Add("@PaidAmount", DistributorTrans.PaidAmount);
                para.Add("@BalanceAmount", DistributorTrans.BalanceAmount);
                para.Add("@PrvBalanceAmount", DistributorTrans.PrvBalanceAmount);
                para.Add("@FinalBalaceAmount", DistributorTrans.FinalBalaceAmount);
                para.Add("@DistributorId", DistributorTrans.ModifiedBy);
                para.Add("@ModifiedDate", DistributorTrans.ModifiedDate);
                para.Add("@ModifiedBy", DistributorTrans.ModifiedBy);
                para.Add("@DeleteStatus", "COMPLETE");
                var value = con.Query<int>("SP_Update_MainDistributorSale", para, null, true, 0, CommandType.StoredProcedure);
               // con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally { con.Close(); }
        }


        public IEnumerable<DistributorSaleDTO> GetDistributorSales(string DistributorId)
        {
            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@DistributorId", HttpContext.Current.Session["UserID"]); // Normal Parameters  
                var ListDistributorTrans = con.Query<DistributorSaleDTO>("select dt.*,rt.RetailerName from Main_DistrbutorSaleInfo AS dt LEFT JOIN Mst_Retailer AS rt ON dt.RetailerId=rt.RetailerId where dt.DeleteStatus='ACTIVE' and dt.DistributorId=@DistributorId", para, null, true, 0, CommandType.Text).ToList();
                return ListDistributorTrans;
            }
            catch (Exception e) { throw; }
            finally { con.Close(); }
            
        }
        public IEnumerable<DistributorSaleDTO> GetDistributorSalesAPI(string DistributorId)
        {
            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@DistributorId", DistributorId); // Normal Parameters  
                var ListDistributorTrans = con.Query<DistributorSaleDTO>("select dt.*,rt.RetailerName from Main_DistrbutorSaleInfo AS dt LEFT JOIN Mst_Retailer AS rt ON dt.RetailerId=rt.RetailerId where dt.DeleteStatus='COMPLETE' and dt.DistributorId=@DistributorId", para, null, true, 0, CommandType.Text).ToList();
                return ListDistributorTrans;
            }
            catch (Exception e) { throw; }
            finally { con.Close(); }

        }
        public IEnumerable<DistributorSaleDTO> GetDistributorSalesTransactionBySaleOrderAPI(string DistributorId, string SaleOrder)
        {
            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@DistributorId", DistributorId);
                para.Add("@SaleOrder", SaleOrder);// Normal Parameters  
                var ListDistributorTrans = con.Query<DistributorSaleDTO>("select dt.*,dp.*,rt.RetailerName from Main_DistrbutorSaleInfo AS dt left join Main_DistrbutorSaleProduct as dp on dt.SaleOrder=dp.SaleOrder LEFT JOIN Mst_Retailer AS rt ON dt.RetailerId=rt.RetailerId where dt.DeleteStatus='COMPLETE' and dt.DistributorId=@DistributorId and dt.SaleOrder=@SaleOrder", para, null, true, 0, CommandType.Text).ToList();
                return ListDistributorTrans;
            }
            catch (Exception e) { throw; }
            finally { con.Close(); }

        }
        public IEnumerable<DistributorSaleDTO> GetDistributorSalesMainTransactionBySaleOrderAPI(string SaleOrder)
        {
            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@SaleOrder", SaleOrder);// Normal Parameters  
                var ListDistributorTrans = con.Query<DistributorSaleDTO>("select *,rt.RetailerName from Main_DistrbutorSaleInfo AS dt  JOIN Mst_Retailer AS rt ON dt.RetailerId=rt.RetailerId where dt.DeleteStatus='COMPLETE' and dt.SaleOrder=@SaleOrder", para, null, true, 0, CommandType.Text).ToList();
                return ListDistributorTrans;
            }
            catch (Exception e) { throw; }
            finally { con.Close(); }

        }

       
        public void DeleteDistributorSale(string DistributorSaleId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@DistributorSaleId", DistributorSaleId); // Normal Parameters  
                var value = con.Query("SP_Delete_DistributorSale", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public void DeleteDistributorSaleProduct(string DistributorSaleProductId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@DistributorSaleProductId", DistributorSaleProductId); // Normal Parameters  
                var value = con.Query("SP_Delete_DistributorSaleProduct", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DistributorSaleDTO> GetRetailerByDistributerId(string DistributorId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@DistributorId", HttpContext.Current.Session["UserID"]);
                var ListRetailer = con.Query<DistributorSaleDTO>("Usp_GetRetailerByDistributor", paramater, null, true, 0, CommandType.StoredProcedure).ToList();
                return ListRetailer;
            }
        }
        //Sale Product

        public void InsertDistributorSaleProduct(DistributorSaleProductDTO DistributorSaleProduct)
        {
            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@DistributorSaleProductId", DistributorSaleProduct.DistributorSaleProductId); // Normal Parameters  
                para.Add("@SaleOrder", DistributorSaleProduct.SaleOrder);
                para.Add("@PaperId", DistributorSaleProduct.PaperId);
                para.Add("@PaperRate", DistributorSaleProduct.PaperRate);
                para.Add("@PaperQuantity", DistributorSaleProduct.PaperQuantity);
                para.Add("@TotalPrice", DistributorSaleProduct.TotalPrice);
                para.Add("@PamphletQuantity", DistributorSaleProduct.PamphletQuantity);
                para.Add("@PamphletRate", DistributorSaleProduct.PamphletRate);
                para.Add("@TotalPamphletAmount", DistributorSaleProduct.TotalPamphletAmount);
                para.Add("@TotalFinalAmount", DistributorSaleProduct.TotalFinalAmount);
                para.Add("@DistributorId", DistributorSaleProduct.CreatedBy);
                para.Add("@CreatedDate", DistributorSaleProduct.CreatedDate);
                para.Add("@CreatedBy", DistributorSaleProduct.CreatedBy);
                para.Add("@ModifiedDate", DistributorSaleProduct.ModifiedDate);
                para.Add("@ModifiedBy", DistributorSaleProduct.ModifiedBy);
                para.Add("@DeleteStatus", "ACTIVE");
                var value = con.Query<int>("SP_Insert_DistributorSaleProduct", para, null, true, 0, CommandType.StoredProcedure);
               
            }
            catch (Exception)
            {
                throw;
            }
            finally { con.Close(); }
        }
        public void InsertDistributorSaleProductAPI(DistributorSaleDTO DistributorSaleProduct)
        {
            try {

            con.Open();
            var para = new DynamicParameters();
            para.Add("@DistributorSaleProductId", DistributorSaleProduct.DistributorSaleProductId); // Normal Parameters  
            para.Add("@SaleOrder", DistributorSaleProduct.SaleOrder);
            para.Add("@PaperId", DistributorSaleProduct.PaperId);
            para.Add("@PaperRate", DistributorSaleProduct.PaperRate);
            para.Add("@PaperQuantity", DistributorSaleProduct.PaperQuantity);
            para.Add("@TotalPrice", DistributorSaleProduct.TotalPrice);
            para.Add("@PamphletQuantity", DistributorSaleProduct.PamphletQuantity);
            para.Add("@PamphletRate", DistributorSaleProduct.PamphletRate);
            para.Add("@TotalPamphletAmount", DistributorSaleProduct.TotalPamphletAmount);
            para.Add("@TotalFinalAmount", DistributorSaleProduct.TotalFinalAmount);
            para.Add("@DistributorId", DistributorSaleProduct.CreatedBy);
            para.Add("@CreatedDate", DistributorSaleProduct.CreatedDate);
            para.Add("@CreatedBy", DistributorSaleProduct.CreatedBy);
            para.Add("@ModifiedDate", DistributorSaleProduct.ModifiedDate);
            para.Add("@ModifiedBy", DistributorSaleProduct.ModifiedBy);
            para.Add("@DeleteStatus", "ACTIVE");
            var value = con.Query<int>("SP_Insert_DistributorSaleProduct", para, null, true, 0, CommandType.StoredProcedure);
            //con.Close();

        } catch (Exception)
            {
                throw;
            }
            finally { con.Close(); }
}


        public IEnumerable<DistributorSaleDTO> GetDistributorSaleProducts(string SaleOrder)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@SaleOrder", SaleOrder); // Normal Parameters  
            var ListDistributorSalePRoducts = con.Query<DistributorSaleDTO>("select dt.*,p.PaperName from Main_DistrbutorSaleProduct AS dt left join Mst_Paper as p on dt.PaperId=p.PaperId Where dt.DeleteStatus='ACTIVE' and dt.SaleOrder=@SaleOrder", para, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return ListDistributorSalePRoducts;
        }

        //public void DeleteDistributorSaleProduct(string DistributorSaleProductId)
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
        //    {
        //        var para = new DynamicParameters();
        //        para.Add("@DistributorSaleProductId", DistributorSaleProductId); // Normal Parameters  
        //        var value = con.Query("SP_Delete_DistributorSaleProduct", para, null, true, 0, CommandType.StoredProcedure);
        //    }
        //}

        public IEnumerable<DistributorSaleProductDTO> GetPaperByCityId(string CityId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@CityId", HttpContext.Current.Session["CityID"].ToString());
                var ListPaperbyCityId = con.Query<DistributorSaleProductDTO>("Usp_GetPaperByCityId", paramater, null, true, 0, CommandType.StoredProcedure);
                return ListPaperbyCityId;
            }
        }
        public string GetFinalAmount_By_SaleOrder(string SaleOrder)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@SaleOrder", HttpContext.Current.Session["SaleOrder"].ToString());
                return con.Query<string>("SP_TotalFinalAmount_by_SaleOrder_Distributor", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public string GetFinalAmount_By_SaleOrder_API(string SaleOrder)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@SaleOrder", SaleOrder);
                return con.Query<string>("SP_TotalFinalAmount_by_SaleOrder_Distributor", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
        }

    }
}