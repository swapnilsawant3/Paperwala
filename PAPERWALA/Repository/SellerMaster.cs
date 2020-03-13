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
    public class SellerMaster :ISellerMaster
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());

        public void InsertSeller(SellerDTO Seller)
        {
            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@ShopkeeperId", Seller.ShopkeeperId); // Normal Parameters  
                para.Add("@ShopName", Seller.ShopName);
                para.Add("@ShopKeeperName", Seller.ShopKeeperName);
                para.Add("@DesignationId", Seller.DesignationId);
                para.Add("@Address", Seller.Address);
                para.Add("@MobileNo", Seller.MobileNo);
                para.Add("@DateofBirth", Seller.DateofBirth);
                para.Add("@StateId", Seller.StateId);
                para.Add("@CityId", Seller.CityId);
                para.Add("@Area", Seller.Area);
                para.Add("@PinCode", Seller.Pincode);
                para.Add("@Email", Seller.Email);
                para.Add("@UserName", Seller.UserName);
                para.Add("@Password", Seller.Password);
                para.Add("@StartDate", Seller.StartDate);
                para.Add("@EndDate", Seller.EndDate);
                para.Add("@CreatedDate", Seller.CreatedDate);
                para.Add("@CreatedBy", Seller.CreatedBy);
                para.Add("@ModifiedDate", Seller.ModifiedDate);
                para.Add("@ModifiedBy", Seller.ModifiedBy);
                para.Add("@DeleteStatus", "ACTIVE");
                var value = con.Query<int>("SP_Insert_Update_Shopkeeper", para, null, true, 0, CommandType.StoredProcedure);
                
            }
            catch (Exception e) { throw; }
            finally { con.Close(); }
        }

        public IEnumerable<SellerDTO> GetSellers()
        {
            con.Open();
            var Listscheme = con.Query<SellerDTO>("select d.*,ct.CityName,ct.CityId,st.StateId,st.StateName from Mst_ShopKeeper AS d LEFT JOIN Mst_City AS ct ON d.CityId=ct.CityId  left join Mst_State as st on d.StateId=st.StateId Where d.DeleteStatus='ACTIVE'", null, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return Listscheme;
        }
        public int SellerCount()
        {
            int cnt;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("select count(*) AS total from Mst_ShopKeeper Where DeleteStatus='ACTIVE'", con))
                {
                    con.Open();
                    cnt = (int)sqlCommand.ExecuteScalar();
                }
                return cnt;
            }
        }

        public SellerDTO GetSellerByID(string SellerId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string Query = "select * from Mst_ShopKeeper where ShopkeeperId=" + SellerId;

                var Seller_list = con.Query<SellerDTO>(Query, null, null, true, 0, CommandType.Text).Single();

                return Seller_list;
            }

        }

        public void UpdateSeller(SellerDTO Seller)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                try
                {
                    con.Open();
                    var para = new DynamicParameters();
                    para.Add("@ShopkeeperId", Seller.ShopkeeperId); // Normal Parameters  
                    para.Add("@ShopName", Seller.ShopName);
                    para.Add("@ShopKeeperName", Seller.ShopKeeperName);
                    para.Add("@DesignationId", Seller.DesignationId);
                    para.Add("@Address", Seller.Address);
                    para.Add("@MobileNo", Seller.MobileNo);
                    para.Add("@DateofBirth", Seller.DateofBirth);
                    para.Add("@StateId", Seller.StateId);
                    para.Add("@CityId", Seller.CityId);
                    para.Add("@Area", Seller.Area);
                    para.Add("@PinCode", Seller.Pincode);
                    para.Add("@Email", Seller.Email);
                    para.Add("@UserName", Seller.UserName);
                    para.Add("@Password", Seller.Password);
                    para.Add("@StartDate", Seller.StartDate);
                    para.Add("@EndDate", Seller.EndDate);
                    para.Add("@CreatedDate", Seller.CreatedDate);
                    para.Add("@CreatedBy", Seller.CreatedBy);
                    para.Add("@ModifiedDate", Seller.ModifiedDate);
                    para.Add("@ModifiedBy", Seller.ModifiedBy);
                    para.Add("@DeleteStatus", "ACTIVE");
                    var value = con.Query<int>("SP_Insert_Update_Shopkeeper", para, null, true, 0, CommandType.StoredProcedure);
                   // con.Close();
                }
                catch (Exception e) { throw; }
                finally { con.Close(); }
            }
        }

        public void ChangePassword(SellerDTO seller)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@ShopkeeperId", HttpContext.Current.Session["UserId"]); // Normal Parameters  
            para.Add("@Password", seller.NewPassword);

            var value = con.Query<int>("SP_Change_Password_Seller", para, null, true, 0, CommandType.StoredProcedure);
            con.Close();
        }
        public void ChangePasswordAPI(SellerDTO seller)
        {
            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@ShopkeeperId", seller.ShopkeeperId); // Normal Parameters  
                para.Add("@Password", seller.Password);

                var value = con.Query<int>("SP_Change_Password_Distributor", para, null, true, 0, CommandType.StoredProcedure);

            }
            catch (Exception e) { throw; }
            finally { con.Close(); }
        }

        public string GetPassword(string SellerId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@ShopkeeperId", HttpContext.Current.Session["UserId"]);
                var VPassword = con.Query<string>("Usp_GetPaaword_By_Seller", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                return VPassword;
            }
        }

        public string GetPasswordAPI(string SellerId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@ShopkeeperId", SellerId);
                var VPassword = con.Query<string>("Usp_GetPaaword_By_DistributorId", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                return VPassword;
            }
        }

        public void DeleteSeller(string SellerId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@ShopkeeperId", SellerId); // Normal Parameters  
                var value = con.Query("SP_Delete_SellerMaster", para, null, true, 0, CommandType.StoredProcedure);
            }
        }
        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool SellerExists(string SellerName, int StateId, int CityId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@RetailerName", SellerName); // Normal Parameters 
                para.Add("@StateId", StateId); // Normal Parameters  
                para.Add("@CityId", CityId); // Normal Parameters   
                var value = con.Query<string>("Usp_checkRetailer", para, null, true, 0, CommandType.StoredProcedure).First();

                if (value == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public IEnumerable<SellerDTO> GetCityByStateId(string StateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@StateId", StateId);
                var Plan_list = con.Query<SellerDTO>("Usp_GetCityByStateId", paramater, null, true, 0, CommandType.StoredProcedure);
                return Plan_list;
            }
        }
    }
}