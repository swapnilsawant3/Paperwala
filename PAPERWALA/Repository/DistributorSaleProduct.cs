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
    public class DistributorSaleProduct : IDistributorSaleProduct
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());

        public void InsertDistributorSaleProduct(DistributorSaleProductDTO DistributorSaleProduct)
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
            para.Add("@DistributorId", DistributorSaleProduct.CreatedBy);
            para.Add("@CreatedDate", DistributorSaleProduct.CreatedDate);
            para.Add("@CreatedBy", DistributorSaleProduct.CreatedBy);
            para.Add("@ModifiedDate", DistributorSaleProduct.ModifiedDate);
            para.Add("@ModifiedBy", DistributorSaleProduct.ModifiedBy);
            para.Add("@DeleteStatus", "ACTIVE");
            var value = con.Query<int>("SP_Insert_DistributorSaleProduct", para, null, true, 0, CommandType.StoredProcedure);
            con.Close();
        }

        public List<DisctributorSaleProductDTO>GetDistributorSaleProducts(string SaleOrder)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@SaleOrder", SaleOrder); // Normal Parameters  
            var ListDistributorSalePRoducts = con.Query<DisctributorSaleProductDTO>("select dt.*,p.PaperName from Main_DistrbutorSaleProduct AS dt left join Mst_Paper as p on dt.PaperId=p.PaperId Where dt.DeleteStatus='ACTIVE' and dt.SaleOrder=@SaleOrder", para, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return ListDistributorSalePRoducts;
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


    }
}