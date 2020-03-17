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
    public class RetailerMaster : IRetailerMaster
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());
       
        public void InsertRetailer(RetailerDTO retailer)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@RetailerId", retailer.RetailerId); // Normal Parameters  
            para.Add("@RetailerName", retailer.RetailerName);
            para.Add("@RetailerAddress", retailer.RetailerAddress);
            para.Add("@Area", retailer.Area);
            para.Add("@PinCode", retailer.Pincode);
            para.Add("@MobileNo", retailer.MobileNo);
            para.Add("@AltMobileNo", retailer.AltMobileNo);
            para.Add("@StateId", retailer.StateId);
            para.Add("@CityId", retailer.CityId);
            para.Add("@Email", retailer.Email);
            para.Add("@DateofBirth", retailer.DateofBirth);
            para.Add("@RemainingAMT", retailer.RemainingAMT);
            para.Add("@DistributorId", retailer.CreatedBy);
            para.Add("@CreatedDate", retailer.CreatedDate);
            para.Add("@CreatedBy", retailer.CreatedBy);
            para.Add("@ModifiedDate", retailer.ModifiedDate);
            para.Add("@ModifiedBy", retailer.ModifiedBy);
            para.Add("@DeleteStatus", "ACTIVE");
            var value = con.Query<int>("SP_Insert_Update_RetailerMaster", para, null, true, 0, CommandType.StoredProcedure);
            con.Close();
        }

       

        public IEnumerable<RetailerDTO> webGetRetailers(string DistributorId)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            
                var para = new DynamicParameters();
                para.Add("@DistributorId", DistributorId); // Normal Parameters  
                var Listscheme = con.Query<RetailerDTO>("select r.*, ct.CityName,ct.CityId,st.StateId,st.StateName from Mst_Retailer AS r LEFT JOIN Mst_City AS ct ON r.CityId=ct.CityId  left join Mst_State as st on r.StateId=st.StateId Where r.DeleteStatus='ACTIVE' and r.DistributorId=@DistributorId ORDER BY RetailerId  DESC", para, null, true, 0, CommandType.Text).ToList();
                con.Close();
                return Listscheme;
           
            
        }
       
        public IEnumerable<RetailerDTO> webGetRetailerById(string RetailerId)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            var para = new DynamicParameters();
            para.Add("@RetailerId", RetailerId); // Normal Parameters  
            var Listscheme = con.Query<RetailerDTO>("select r.*,ct.CityName,ct.CityId,st.StateId,st.StateName from Mst_Retailer AS r LEFT JOIN Mst_City AS ct ON r.CityId=ct.CityId  left join Mst_State as st on r.StateId=st.StateId Where r.DeleteStatus='ACTIVE' and r.RetailerId=@RetailerId", para, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return Listscheme;


        }

        public IEnumerable<RetailerDTO> GetRetailers(string DistributorId)
        {
            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@DistributorId", HttpContext.Current.Session["UserID"]); // Normal Parameters  
                var Listscheme = con.Query<RetailerDTO>("select r.*,ct.CityName,ct.CityId,st.StateId,st.StateName from Mst_Retailer AS r LEFT JOIN Mst_City AS ct ON r.CityId=ct.CityId  left join Mst_State as st on r.StateId=st.StateId Where r.DeleteStatus='ACTIVE' and r.DistributorId=@DistributorId ORDER BY RetailerId  DESC", para, null, true, 0, CommandType.Text).ToList();
                return Listscheme;
            }
            catch (Exception e) { throw; }
            finally { con.Close(); }
           


        }

        public string GetRetailerCount(string DistributorId)
        {

            try
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@DistributorId", DistributorId); // Normal Parameters  
                var countRetailer = con.Query<string>("select count(*) AS total from Mst_Retailer as r Where r.DeleteStatus='ACTIVE' and r.DistributorId=@DistributorId", para, null, true, 0, CommandType.Text).FirstOrDefault();
                return countRetailer;
            }
            catch (Exception e) { throw; }
            finally { con.Close(); }
           
                      
            
        }
        public RetailerDTO GetRetailerByID(string RetailerId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string Query = "select * from Mst_Retailer where RetailerId =" + RetailerId;

                var Scheme_list = con.Query<RetailerDTO>(Query, null, null, true, 0, CommandType.Text).Single();

                return Scheme_list;
            }

        }

        public void UpdateRetailer(RetailerDTO retailer)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@RetailerId", retailer.RetailerId); // Normal Parameters 
                para.Add("@RetailerName", retailer.RetailerName);
                para.Add("@RetailerAddress", retailer.RetailerAddress);
                para.Add("@Area", retailer.Area);
                para.Add("@PinCode", retailer.Pincode);
                para.Add("@MobileNo", retailer.MobileNo);
                para.Add("@AltMobileNo", retailer.AltMobileNo);
                para.Add("@StateId", retailer.StateId);
                para.Add("@CityId", retailer.CityId);
                para.Add("@Email", retailer.Email);
                para.Add("@DateofBirth", retailer.DateofBirth);
                para.Add("@RemainingAMT", retailer.RemainingAMT);
                para.Add("@DistributorId", retailer.ModifiedBy);
                para.Add("@CreatedDate", retailer.CreatedDate);
                para.Add("@CreatedBy", retailer.CreatedBy);
                para.Add("@ModifiedDate", retailer.ModifiedDate);
                para.Add("@ModifiedBy", retailer.ModifiedBy);
                para.Add("@DeleteStatus", "ACTIVE");
                var value = con.Query<int>("SP_Insert_Update_RetailerMaster", para, null, true, 0, CommandType.StoredProcedure);
                con.Close();
            }
        }

        public void DeleteRetailer(string RetailerId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@RetailerId", RetailerId); // Normal Parameters  
                var value = con.Query("SP_Delete_RetailerMaster", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool RetailerExists(string RetailerName, int? StateId, int? CityId)
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

        public IEnumerable<RetailerDTO> GetCityByStateId(string StateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@StateId", StateId);
                var Plan_list = con.Query<RetailerDTO>("Usp_GetCityByStateId", paramater, null, true, 0, CommandType.StoredProcedure);
                return Plan_list;
            }
        }

        public string GetPrevBalalnceByRetailerId(string RetailerId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@RetailerId", RetailerId);
                var RemainingAMT = con.Query<string>("Usp_GetRemainingAMTByRetailerId", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                return RemainingAMT;
            }
        }


    }
}
