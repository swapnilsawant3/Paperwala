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
    public class StateMaster : IStateMaster
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());

        public void InsertState(StateDTO state)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@StateId", state.StateId); // Normal Parameters  
            para.Add("@StateName", state.StateName);
            para.Add("@CreatedDate", state.CreatedDate);
            para.Add("@CreatedBy", state.CreatedBy);
            para.Add("@ModifiedDate", state.ModifiedDate);
            para.Add("@ModifiedBy", state.ModifiedBy);
            para.Add("@DeleteStatus", "ACTIVE");
           var value = con.Query<int>("SP_Insert_Update_StateMaster", para, null, true, 0, CommandType.StoredProcedure);
            con.Close();
        }

        public IEnumerable<StateDTO> GetStates()
        {
            con.Open();
            var Listscheme = con.Query<StateDTO>("select * from Mst_State Where DeleteStatus!='DELETE'", null, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return Listscheme;

        }
        public int GetStateCount()
        {
            int cnt;

            using (SqlCommand sqlCommand = new SqlCommand("select count(*) AS total from Mst_State Where DeleteStatus!='DELETE'", con))
            {
                con.Open();
                cnt = (int)sqlCommand.ExecuteScalar();
            }
            return cnt;
        }

        public StateDTO GetStateByID(string StateId)
        {
            con.Open();
                string Query = "select * from Mst_State where StateId =" + StateId;
                var Scheme_list = con.Query < StateDTO>(Query, null, null, true, 0, CommandType.Text).Single();
            con.Close();
            return Scheme_list;
            

        }

        public void UpdateState(StateDTO state)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@StateId", state.StateId); // Normal Parameters  
                para.Add("@StateName", state.StateName);
                para.Add("@CreatedDate", state.CreatedDate);
                para.Add("@CreatedBy", state.CreatedBy);
                para.Add("@ModifiedDate", state.ModifiedDate);
                para.Add("@ModifiedBy", state.ModifiedBy);
                para.Add("@DeleteStatus", state.DeleteStatus);
                var value = con.Query<int>("SP_Insert_Update_StateMaster", para, null, true, 0, CommandType.StoredProcedure);
                con.Close();
            }
        }

        public void DeleteState(string StateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@StateId", StateId); // Normal Parameters  
                var value = con.Query("SP_Delete_StateMaster", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool StateNameExists(string StateName)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@StateName", StateName); // Normal Parameters  
                var value = con.Query<string>("Usp_checkState", para, null, true, 0, CommandType.StoredProcedure).First();

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
