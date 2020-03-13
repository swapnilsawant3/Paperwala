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
    public class DesignationMaster : IDesignationMaster
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());

        public void InsertDesignation(DesignationDTO desgnation)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@DesignationId", desgnation.DesignationId); // Normal Parameters  
            para.Add("@Designation", desgnation.Designation);
            para.Add("@CreatedDate", desgnation.CreatedDate);
            para.Add("@CreatedBy", desgnation.CreatedBy);
            para.Add("@ModifiedDate", desgnation.ModifiedDate);
            para.Add("@ModifiedBy", desgnation.ModifiedBy);
            para.Add("@DeleteStatus", "ACTIVE");
            var value = con.Query<int>("SP_Insert_Update_DesignationMaster", para, null, true, 0, CommandType.StoredProcedure);
            con.Close();
        }

        public IEnumerable<DesignationDTO> GetDesignations()
        {
            con.Open();
            var Listscheme = con.Query<DesignationDTO>("select * from Mst_Designation Where DeleteStatus!='DELETE'", null, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return Listscheme;

        }
        public int GetDesignationCount()
        {
            int cnt;

            using (SqlCommand sqlCommand = new SqlCommand("select count(*) AS total from Mst_Designation Where DeleteStatus!='DELETE'", con))
            {
                con.Open();
                cnt = (int)sqlCommand.ExecuteScalar();
            }
            return cnt;
        }

        public DesignationDTO GetDesignationByID(string DesignationId)
        {
            con.Open();
            string Query = "select * from Mst_Designation where DesignationId =" + DesignationId;
            var Scheme_list = con.Query<DesignationDTO>(Query, null, null, true, 0, CommandType.Text).Single();
            con.Close();
            return Scheme_list;


        }

        public void UpdateDesignation(DesignationDTO desgnation)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@DesignationId", desgnation.DesignationId); // Normal Parameters  
                para.Add("@Designation", desgnation.Designation);
                para.Add("@CreatedDate", desgnation.CreatedDate);
                para.Add("@CreatedBy", desgnation.CreatedBy);
                para.Add("@ModifiedDate", desgnation.ModifiedDate);
                para.Add("@ModifiedBy", desgnation.ModifiedBy);
                para.Add("@DeleteStatus", "ACTIVE");
                var value = con.Query<int>("SP_Insert_Update_DesignationMaster", para, null, true, 0, CommandType.StoredProcedure);
                con.Close();
            }
        }

        public void DeleteDesignation(string DesgnationId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@DesgnationId", DesgnationId); // Normal Parameters  
                var value = con.Query("SP_Delete_DesignationMaster", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool DesignationNameExists(string DesgnationName)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@DesgnationName", DesgnationName); // Normal Parameters  
                var value = con.Query<string>("Usp_checkDesignation", para, null, true, 0, CommandType.StoredProcedure).First();

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

    }
}