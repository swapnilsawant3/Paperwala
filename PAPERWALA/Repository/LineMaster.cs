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
    public class LineMaster : ILineMaster
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());

        public void InsertLine(LineDTO line)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@LineId", line.LineId); // Normal Parameters  
            para.Add("@LineName", line.LineName);
            para.Add("@StateId", line.StateId);
            para.Add("@CityId", line.CityId);
            para.Add("@ShopkeeperId", line.ShopkeeperId);
            
            para.Add("@CreatedDate", line.CreatedDate);
            para.Add("@CreatedBy", line.CreatedBy);
            para.Add("@ModifiedDate", line.ModifiedDate);
            para.Add("@ModifiedBy", line.ModifiedBy);
            para.Add("@DeleteStatus", "ACTIVE");
            var value = con.Query<int>("SP_Insert_Update_LineMaster", para, null, true, 0, CommandType.StoredProcedure);
            con.Close();
        }

        public IEnumerable<LineDTO> GetLines()
        {
            con.Open();
            var Listscheme = con.Query<LineDTO>("select ln.*,ct.CityName,st.StateName from Mst_Line AS ln Left join Mst_City as ct on ln.CityId=ct.CityId LEFT JOIN Mst_State AS st ON ln.StateId=st.StateId Where ln.DeleteStatus='ACTIVE'", null, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return Listscheme;
        }

        public LineDTO GetLineByID(string LineID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string Query = "select * from Mst_Line where LineId =" + LineID;

                var Scheme_list = con.Query<LineDTO>(Query, null, null, true, 0, CommandType.Text).Single();

                return Scheme_list;
            }

        }

        public void UpdateLine(LineDTO line)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@LineId", line.LineId); // Normal Parameters  
                para.Add("@LineName", line.LineName);
                para.Add("@StateId", line.StateId);
                para.Add("@CityId", line.CityId);
                para.Add("@ShopkeeperId", line.ShopkeeperId);

                para.Add("@CreatedDate", line.CreatedDate);
                para.Add("@CreatedBy", line.CreatedBy);
                para.Add("@ModifiedDate", line.ModifiedDate);
                para.Add("@ModifiedBy", line.ModifiedBy);
                para.Add("@DeleteStatus", "ACTIVE");
                var value = con.Query<int>("SP_Insert_Update_LineMaster", para, null, true, 0, CommandType.StoredProcedure);
                con.Close();
            }
        }

        public void DeleteLine(string LineId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@LineId", LineId); // Normal Parameters  
                var value = con.Query("SP_Delete_LineMaster", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
        public bool LineExists(string LineName)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@LineName", LineName); // Normal Parameters  
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

        public IEnumerable<LineDTO> GetCityByStateId(string StateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@StateId", StateId);
                var Plan_list = con.Query<LineDTO>("Usp_GetCityByStateId", paramater, null, true, 0, CommandType.StoredProcedure);
                return Plan_list;
            }
        }

    }
}