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
    public class DistributorMaster :IDistributorMaster
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());

        public void InsertDistributor(DistributorDTO distributor)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@DistributorId", distributor.DistributorId); // Normal Parameters  
            para.Add("@DistributorName", distributor.DistributorName);
            para.Add("@ContactPersonName", distributor.ContactPersonName);
            para.Add("@DesignationId", distributor.DesignationId);
            para.Add("@Address", distributor.Address);
            para.Add("@MobileNo", distributor.MobileNo);
            para.Add("@AltMobileNo", distributor.AltMobileNo);
            para.Add("@StateId", distributor.StateId);
            para.Add("@CityId", distributor.CityId);
            para.Add("@Area", distributor.Area);
            para.Add("@PinCode", distributor.Pincode);
            para.Add("@Email", distributor.Email);
            para.Add("@DateofBirth", distributor.DateofBirth);
            para.Add("@UserName", distributor.UserName);
            para.Add("@Password", distributor.Password);
            para.Add("@StartDate", distributor.StartDate);
            para.Add("@EndDate", distributor.EndDate);
            para.Add("@CreatedDate", distributor.CreatedDate);
            para.Add("@CreatedBy", distributor.CreatedBy);
            para.Add("@ModifiedDate", distributor.ModifiedDate);
            para.Add("@ModifiedBy", distributor.ModifiedBy);
            para.Add("@DeleteStatus", "ACTIVE");
            var value = con.Query<int>("SP_Insert_Update_DistributorMaster", para, null, true, 0, CommandType.StoredProcedure);
            con.Close();
        }

        public IEnumerable<DistributorDTO> GetDistributors()
        {
            con.Open();
            var Listscheme = con.Query<DistributorDTO>("select d.*,ct.CityName,ct.CityId,st.StateId,st.StateName from Mst_Distributor AS d LEFT JOIN Mst_City AS ct ON d.CityId=ct.CityId  left join Mst_State as st on d.StateId=st.StateId Where d.DeleteStatus='ACTIVE'", null, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return Listscheme;
        }
        public IEnumerable<DistributorDTO> webGetDistributorByDistributorId(string DistributorId)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            var para = new DynamicParameters();
            para.Add("@DistributorId", DistributorId); // Normal Parameters  
            var Listscheme = con.Query<DistributorDTO>("select r.*, ct.CityName,ct.CityId,st.StateId,st.StateName from Mst_Distributor AS r LEFT JOIN Mst_City AS ct ON r.CityId=ct.CityId  left join Mst_State as st on r.StateId=st.StateId Where r.DeleteStatus='ACTIVE' and r.DistributorId=@DistributorId", para, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return Listscheme;


        }

        public int GetDistributorCount()
        {
            int cnt;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("select count(*) AS total from Mst_Distributor Where DeleteStatus='ACTIVE'", con))
                {
                    con.Open();
                    cnt = (int)sqlCommand.ExecuteScalar();
                }
                return cnt;
            }
        }
        public DistributorDTO GetDistributorByID(string DistributorId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string Query = "select * from Mst_Distributor where DistributorId="+ DistributorId;

               var Scheme_list = con.Query<DistributorDTO>(Query, null, null, true, 0, CommandType.Text).Single();

                return Scheme_list;
            }

        }

        public void UpdateDistributor(DistributorDTO distributor)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@DistributorId", distributor.DistributorId); // Normal Parameters  
                para.Add("@DistributorName", distributor.DistributorName);
                para.Add("@ContactPersonName", distributor.ContactPersonName);
                para.Add("@DesignationId", distributor.DesignationId);
                para.Add("@Address", distributor.Address);
                para.Add("@MobileNo", distributor.MobileNo);
                para.Add("@AltMobileNo", distributor.AltMobileNo);
                para.Add("@StateId", distributor.StateId);
                para.Add("@CityId", distributor.CityId);
                para.Add("@Area", distributor.Area);
                para.Add("@PinCode", distributor.Pincode);
                para.Add("@Email", distributor.Email);
                para.Add("@DateofBirth", distributor.DateofBirth);
                para.Add("@UserName", distributor.UserName);
                para.Add("@Password", distributor.Password);
                para.Add("@StartDate", distributor.StartDate);
                para.Add("@EndDate", distributor.EndDate);
                para.Add("@CreatedDate", distributor.CreatedDate);
                para.Add("@CreatedBy", distributor.CreatedBy);
                para.Add("@ModifiedDate", distributor.ModifiedDate);
                para.Add("@ModifiedBy", distributor.ModifiedBy);
                para.Add("@DeleteStatus", "ACTIVE");
                var value = con.Query<int>("SP_Insert_Update_DistributorMaster", para, null, true, 0, CommandType.StoredProcedure);
                con.Close();
            }
        }
        public void ChangePassword(DistributorDTO distributor)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@DistributorId", HttpContext.Current.Session["UserId"]); // Normal Parameters  
            para.Add("@Password", distributor.NewPassword);
           
            var value = con.Query<int>("SP_Change_Password_Distributor", para, null, true, 0, CommandType.StoredProcedure);
            con.Close();
        }

        public void ChangePasswordAPI(DistributorDTO distributor)
        {
            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@DistributorId", distributor.DistributorId); // Normal Parameters  
                para.Add("@Password", distributor.Password);

                var value = con.Query<int>("SP_Change_Password_Distributor", para, null, true, 0, CommandType.StoredProcedure);
                
            }
            catch (Exception e) { throw; }
            finally { con.Close(); }
        }

        public string GetPassword(string DistId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@DistributorId", HttpContext.Current.Session["UserId"]);
                var VPassword = con.Query<string>("Usp_GetPaaword_By_DistributorId", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                return VPassword;
            }
        }
        public string GetPasswordAPI(string DistId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@DistributorId", DistId);
                var VPassword = con.Query<string>("Usp_GetPaaword_By_DistributorId", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                return VPassword;
            }
        }
        public string GetSumBalanceAmount(string DistId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@DistributorId", DistId);
                var VPassword = con.Query<string>("Usp_SumBalanceAMT_By_DistributorId", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                return VPassword;
            }
        }
        public void DeleteDistributor(string DistributorId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@DistributorId", DistributorId); // Normal Parameters  
                var value = con.Query("SP_Delete_DistributorMaster", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool RetailerExists(string RetailerName, int StateId, int CityId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@RetailerName", RetailerName); // Normal Parameters 
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

        public IEnumerable<DistributorDTO> GetCityByStateId(string StateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@StateId", StateId);
                var Plan_list = con.Query<DistributorDTO>("Usp_GetCityByStateId", paramater, null, true, 0, CommandType.StoredProcedure);
                return Plan_list;
            }
        }
    }
}