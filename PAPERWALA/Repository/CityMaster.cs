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
    public class CityMaster : ICityMaster
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());
        public void InsertCity(CityDTO city)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@CityId", city.CityId); // Normal Parameters  
            para.Add("@CityName", city.CityName);
            para.Add("@StateId", city.StateId);
            para.Add("@CreatedDate", city.CreatedDate);
            para.Add("@CreatedBy", city.CreatedBy);
            para.Add("@ModifiedDate", city.ModifiedDate);
            para.Add("@ModifiedBy", city.ModifiedBy);
            para.Add("@DeleteStatus", "ACTIVE");
            var value = con.Query<int>("SP_Insert_Update_CityMaster", para, null, true, 0, CommandType.StoredProcedure);
            con.Close();
        }

        public IEnumerable<CityDTO> GetCitys()
        {
            con.Open();
            var Listscheme = con.Query<CityDTO>("select cty.*,StateName from Mst_City AS cty LEFT JOIN Mst_State AS st ON cty.StateId=st.StateId Where cty.DeleteStatus='ACTIVE'", null, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return Listscheme;
        }
    
    public int GetCityCount()
    {
        int cnt;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
        {
            using (SqlCommand sqlCommand = new SqlCommand("select count(*) AS total from Mst_City Where DeleteStatus='ACTIVE'", con))
            {
                con.Open();
                cnt = (int)sqlCommand.ExecuteScalar();
            }
            return cnt;
        }
    }
    public CityDTO GetCityByID(string CityId)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
        {
            string Query = "select * from Mst_City where CityId =" + CityId;

            var Scheme_list = con.Query<CityDTO>(Query, null, null, true, 0, CommandType.Text).Single();

            return Scheme_list;
        }

    }

    public void UpdateCity(CityDTO city)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
        {
            var para = new DynamicParameters();
            para.Add("@CityId", city.CityId); // Normal Parameters  
            para.Add("@CityName", city.CityName);
            para.Add("@StateId", city.StateId);
            para.Add("@CreatedDate", city.CreatedDate);
            para.Add("@CreatedBy", city.CreatedBy);
            para.Add("@ModifiedDate", city.ModifiedDate);
            para.Add("@ModifiedBy", city.ModifiedBy);
            para.Add("@DeleteStatus", "ACTIVE");
            var value = con.Query<int>("SP_Insert_Update_CityMaster", para, null, true, 0, CommandType.StoredProcedure);
        }
    }

    public void DeleteCity(string CityId)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
        {
            var para = new DynamicParameters();
            para.Add("@CityId", CityId); // Normal Parameters  
            var value = con.Query("SP_Delete_CityMaster", para, null, true, 0, CommandType.StoredProcedure);
        }
    }

    public void Save()
    {
        throw new NotImplementedException();
    }

    public bool CityNameExists(string CityName)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
        {
            var para = new DynamicParameters();
            para.Add("@CityName", CityName); // Normal Parameters  
            var value = con.Query<string>("Usp_checkDepot", para, null, true, 0, CommandType.StoredProcedure).First();

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

    public IEnumerable<CityDTO> GetCityByStateId(string StateId)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
        {
            var paramater = new DynamicParameters();
            paramater.Add("@StateId", StateId);
            var Plan_list = con.Query<CityDTO>("Usp_GetCityByStateId", paramater, null, true, 0, CommandType.StoredProcedure);
            return Plan_list;
        }
    }


    }
}