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
    public class PaperRateMaster : IPaperRateMaster
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());
        string paperRate;
        public void InsertPaperRate(PaperRateDTO paperrate)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@PaperRateId", paperrate.PaperRateId); // Normal Parameters  
            para.Add("@StateId", paperrate.StateId);
            para.Add("@PaperId", paperrate.PaperId);
            para.Add("@CityId", paperrate.CityId);
            para.Add("@MondayRate", paperrate.MondayRate);
            para.Add("@TuesdayRate", paperrate.TuesdayRate);
            para.Add("@WednesdayRate", paperrate.WednesdayRate);
            para.Add("@ThursdayRate", paperrate.ThursdayRate);
            para.Add("@FridayRate", paperrate.FridayRate);
            para.Add("@SaturdayRate", paperrate.SaturdayRate);
            para.Add("@SundayRate", paperrate.SundayRate);
            para.Add("@CreatedDate", paperrate.CreatedDate);
            para.Add("@CreatedBy", paperrate.CreatedBy);
            para.Add("@ModifiedDate", paperrate.ModifiedDate);
            para.Add("@ModifiedBy", paperrate.ModifiedBy);
            para.Add("@DeleteStatus", "ACTIVE");
            var value = con.Query<int>("SP_Insert_Update_PaperRateMaster", para, null, true, 0, CommandType.StoredProcedure);
            con.Close();
        }

        public IEnumerable<PaperRateDTO> GetPaperRates()
        {
            con.Open();
            var Listscheme = con.Query<PaperRateDTO>("select pr.*,ct.CityName,ct.CityId,st.StateId,st.StateName,p.PaperId,p.PaperName from Mst_PaperRate AS pr LEFT JOIN Mst_City AS ct ON pr.CityId=ct.CityId Left Join Mst_Paper as p on pr.PaperId=p.PaperId left join Mst_State as st on pr.StateId=st.StateId Where pr.DeleteStatus='ACTIVE'", null, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return Listscheme;
        }

        public int GetPaperRateCount()
        {
            int cnt;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("select count(*) AS total from Mst_PaperRate Where DeleteStatus='ACTIVE'", con))
                {
                    con.Open();
                    cnt = (int)sqlCommand.ExecuteScalar();
                }
                return cnt;
            }
        }
        public PaperRateDTO GetPaperRateByID(string PaperRateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string Query = "select * from Mst_PaperRate where PaperRateId =" + PaperRateId;

                var Scheme_list = con.Query<PaperRateDTO>(Query, null, null, true, 0, CommandType.Text).Single();

                return Scheme_list;
            }

        }

        public void UpdatePaperRate(PaperRateDTO paperrate)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@PaperRateId", paperrate.PaperRateId); // Normal Parameters  
                para.Add("@StateId", paperrate.StateId);
                para.Add("@PaperId", paperrate.PaperId);
                para.Add("@CityId", paperrate.CityId);
                para.Add("@MondayRate", paperrate.MondayRate);
                para.Add("@TuesdayRate", paperrate.TuesdayRate);
                para.Add("@WednesdayRate", paperrate.WednesdayRate);
                para.Add("@ThursdayRate", paperrate.ThursdayRate);
                para.Add("@FridayRate", paperrate.FridayRate);
                para.Add("@SaturdayRate", paperrate.SaturdayRate);
                para.Add("@SundayRate", paperrate.SundayRate);
                para.Add("@CreatedDate", paperrate.CreatedDate);
                para.Add("@CreatedBy", paperrate.CreatedBy);
                para.Add("@ModifiedDate", paperrate.ModifiedDate);
                para.Add("@ModifiedBy", paperrate.ModifiedBy);
                para.Add("@DeleteStatus", "ACTIVE");
                var value = con.Query<int>("SP_Insert_Update_PaperRateMaster", para, null, true, 0, CommandType.StoredProcedure);
                con.Close();
            }
        }

        public void DeletePaperRate(string PaperRateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@PaperRateId", PaperRateId); // Normal Parameters  
                var value = con.Query("SP_Delete_PaperRateMaster", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool CityPaperRateExists(double CityPaperRate, int PaperId,int CityId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@Rate", CityPaperRate); // Normal Parameters 
                para.Add("@PaperId", PaperId); // Normal Parameters  
                para.Add("@CityId", CityId); // Normal Parameters   
                var value = con.Query<string>("Usp_checkPaperRate", para, null, true, 0, CommandType.StoredProcedure).First();

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

        public IEnumerable<PaperRateDTO> GetCityByStateId(string StateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@StateId", StateId);
                var Plan_list = con.Query<PaperRateDTO>("Usp_GetCityByStateId", paramater, null, true, 0, CommandType.StoredProcedure);
                return Plan_list;
            }
        }
        public IEnumerable<PaperRateDTO> GetPaperByCityId(string CityId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@CityId", CityId);
                var Plan_list = con.Query<PaperRateDTO>("Usp_GetPaperByCityId", paramater, null, true, 0, CommandType.StoredProcedure);
                return Plan_list;
            }
        }



        public string GetPaperRateByPaperId(string PaperId, string Tdate)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
               
                var para = new DynamicParameters();
                para.Add("@PaperId", PaperId);
                DateTime currentdate =Convert.ToDateTime(Tdate);
                string curday = currentdate.DayOfWeek.ToString();
                switch (curday)
                {
                    case "Monday":
                        var MondayRate = con.Query<string>("Usp_GetPaperRateByPaperId_Monday", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                        paperRate = MondayRate;
                        break;
                    case "Tuesday":
                        var TuesdayRate = con.Query<string>("Usp_GetPaperRateByPaperId_Tuesday", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                        paperRate = TuesdayRate;
                        break;
                    case "Wednesday":
                        var WednesdayRate = con.Query<string>("Usp_GetPaperRateByPaperId_Wednesday", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                        paperRate = WednesdayRate;
                        break;
                    case "Thursday":
                        var ThursdayRate = con.Query<string>("Usp_GetPaperRateByPaperId_Thursday", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                        paperRate = ThursdayRate;
                        break;
                    case "Friday":
                        var FridayRate = con.Query<string>("Usp_GetPaperRateByPaperId_Friday", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                        paperRate = FridayRate;
                        break;
                    case "Saturday":
                        var SaturdayRate = con.Query<string>("Usp_GetPaperRateByPaperId_Saturday", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                        paperRate = SaturdayRate;
                        break;
                    case "Sunday":
                        var SundayRate = con.Query<string>("Usp_GetPaperRateByPaperId_Sunday", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                        paperRate = SundayRate;
                        break;
                }
                return paperRate;
            }
        }



    }
}