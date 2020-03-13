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
    
    public class PaperMaster : IPaperMaster
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());
        public void InsertPaper(PaperDTO paper)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@PaperId", paper.PaperId); // Normal Parameters  
            para.Add("@PaperName", paper.PaperName);
            para.Add("@StateId", paper.StateId);
            para.Add("@CityId", paper.CityId);
            para.Add("@Language", paper.Language);

            para.Add("@CreatedDate", paper.CreatedDate);
            para.Add("@CreatedBy", paper.CreatedBy);
            para.Add("@ModifiedDate", paper.ModifiedDate);
            para.Add("@ModifiedBy", paper.ModifiedBy);
            para.Add("@DeleteStatus", "ACTIVE");
            var value = con.Query<int>("SP_Insert_Update_PaperMaster", para, null, true, 0, CommandType.StoredProcedure);
            con.Close();
        }

        public IEnumerable<PaperDTO> GetPapers()
        {
            con.Open();
            var Listscheme = con.Query<PaperDTO>("select pap.*,StateName,CityName from Mst_Paper AS pap LEFT JOIN Mst_State AS st ON pap.StateId=st.StateId  left join Mst_City as ct on pap.CityId = ct.CityId Where pap.DeleteStatus='ACTIVE'", null, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return Listscheme;
        }

        public int GetPaperCount()
        {
            int cnt;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("select count(*) AS total from Mst_Paper Where DeleteStatus='ACTIVE'", con))
                {
                    con.Open();
                    cnt = (int)sqlCommand.ExecuteScalar();
                }
                return cnt;
            }
        }
        public PaperDTO GetPaperByID(string PaperId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string Query = "select * from Mst_Paper where PaperId = " + PaperId;

                var Scheme_list = con.Query<PaperDTO>(Query, null, null, true, 0, CommandType.Text).Single();

                return Scheme_list;
            }

        }

        public void UpdatePaper(PaperDTO paper)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@PaperId", paper.PaperId); // Normal Parameters  
                para.Add("@PaperName", paper.PaperName);
                para.Add("@StateId", paper.StateId);
                para.Add("@CityId", paper.CityId);
                para.Add("@Language", paper.Language);
                para.Add("@CreatedDate", paper.CreatedDate);
                para.Add("@CreatedBy", paper.CreatedBy);
                para.Add("@ModifiedDate", paper.ModifiedDate);
                para.Add("@ModifiedBy", paper.ModifiedBy);
                para.Add("@DeleteStatus", "ACTIVE");
                var value = con.Query<int>("SP_Insert_Update_PaperMaster", para, null, true, 0, CommandType.StoredProcedure);
                con.Close();
            }
        }

        public void DeletePaper(string PaperId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@PaperId", PaperId); // Normal Parameters  
                var value = con.Query("SP_Delete_PaperMaster", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool PaperNameExists(string PaperName , int StateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@PaperName", PaperName); // Normal Parameters  
                para.Add("@StateId", StateId);
                var value = con.Query<string>("Usp_checkPaper", para, null, true, 0, CommandType.StoredProcedure).First();

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

        public IEnumerable<PaperDTO> GetPaperByCityId(string CityId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@CityId", CityId);
                var Plan_list = con.Query<PaperDTO>("Usp_GetPaperByCityId", paramater, null, true, 0, CommandType.StoredProcedure);
                return Plan_list;
            }
        }

    }
}