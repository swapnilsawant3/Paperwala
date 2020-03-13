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
    public class LoginDistributor : ILoginDistributor
    {
        public IEnumerable<DistributorDTO> GetAllUsers()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                return con.Query<DistributorDTO>("SP_GetAllDistributors", null, null, true, 0, CommandType.StoredProcedure).ToList();
            }
        }

        public string GetUserID_By_UserName(string UserName)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@UserName", UserName);
                return con.Query<string>("SP_UserIDbyUserName_Distributor", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public string GetDesignationByUserID(string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@UserId", UserId);
                return con.Query<string>("Usp_getDesignationByUserID_Destributor", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
        }
        public string GetStateIdByUserID(string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@UserId", UserId);
                return con.Query<string>("Usp_getStateIdByUserID_Destributor", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
        }
        public string GetCityIdByUserID(string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@UserId", UserId);
                return con.Query<string>("Usp_getCityByUserID_Destributor", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
        }
        public bool Get_CheckUserExist(string UserName, string Password)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@UserName", UserName);
                para.Add("@Password", Password);
                return con.Query<bool>("SP_CheckUserExist_Distributor", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
        }

    }
}