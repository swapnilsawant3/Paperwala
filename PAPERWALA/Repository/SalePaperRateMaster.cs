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
    public class SalePaperRateMaster :ISalePaperRateMaster
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());
        string salepaperRate;

        public void InsertSalePaperRate(SalePaperRateDTO salepaperrate)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@SalePaperRateId", salepaperrate.SalePaperRateId); // Normal Parameters  
            para.Add("@StateId", salepaperrate.StateId);
            para.Add("@PaperId", salepaperrate.PaperId);
            para.Add("@CityId", salepaperrate.CityId);
            para.Add("@MondayRate", salepaperrate.MondayRate);
            para.Add("@TuesdayRate", salepaperrate.TuesdayRate);
            para.Add("@WednesdayRate", salepaperrate.WednesdayRate);
            para.Add("@ThursdayRate", salepaperrate.ThursdayRate);
            para.Add("@FridayRate", salepaperrate.FridayRate);
            para.Add("@SaturdayRate", salepaperrate.SaturdayRate);
            para.Add("@SundayRate", salepaperrate.SundayRate);
            para.Add("@CreatedDate", salepaperrate.CreatedDate);
            para.Add("@CreatedBy", salepaperrate.CreatedBy);
            para.Add("@ModifiedDate", salepaperrate.ModifiedDate);
            para.Add("@ModifiedBy", salepaperrate.ModifiedBy);
            para.Add("@DeleteStatus", "ACTIVE");
            var value = con.Query<int>("SP_Insert_Update_SalePaperRateMaster", para, null, true, 0, CommandType.StoredProcedure);
            con.Close();
        }

        public IEnumerable<SalePaperRateDTO> GetSalePaperRates()
        {
            con.Open();
            var Listscheme = con.Query<SalePaperRateDTO>("select pr.*,ct.CityName,ct.CityId,st.StateId,st.StateName,p.PaperId,p.PaperName from Mst_SalePaperRate AS pr LEFT JOIN Mst_City AS ct ON pr.CityId=ct.CityId Left Join Mst_Paper as p on pr.PaperId=p.PaperId left join Mst_State as st on pr.StateId=st.StateId Where pr.DeleteStatus='ACTIVE'", null, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return Listscheme;
        }

        public int GetSalePaperRateCount()
        {
            int cnt;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                using (SqlCommand sqlCommand = new SqlCommand("select count(*) AS total from Mst_SalePaperRate Where DeleteStatus='ACTIVE'", con))
                {
                    con.Open();
                    cnt = (int)sqlCommand.ExecuteScalar();
                }
                return cnt;
            }
        }

        public SalePaperRateDTO GetSalePaperRateByID(string SalePaperRateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string Query = "select * from Mst_SalePaperRate where SalePaperRateId =" + SalePaperRateId;

                var Scheme_list = con.Query<SalePaperRateDTO>(Query, null, null, true, 0, CommandType.Text).Single();

                return Scheme_list;
            }

        }

        public void UpdateSalePaperRate(SalePaperRateDTO salepaperrate)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                con.Open();
                var para = new DynamicParameters();
                para.Add("@SalePaperRateId", salepaperrate.SalePaperRateId); // Normal Parameters  
                para.Add("@StateId", salepaperrate.StateId);
                para.Add("@PaperId", salepaperrate.PaperId);
                para.Add("@CityId", salepaperrate.CityId);
                para.Add("@MondayRate", salepaperrate.MondayRate);
                para.Add("@TuesdayRate", salepaperrate.TuesdayRate);
                para.Add("@WednesdayRate", salepaperrate.WednesdayRate);
                para.Add("@ThursdayRate", salepaperrate.ThursdayRate);
                para.Add("@FridayRate", salepaperrate.FridayRate);
                para.Add("@SaturdayRate", salepaperrate.SaturdayRate);
                para.Add("@SundayRate", salepaperrate.SundayRate);
                para.Add("@CreatedDate", salepaperrate.CreatedDate);
                para.Add("@CreatedBy", salepaperrate.CreatedBy);
                para.Add("@ModifiedDate", salepaperrate.ModifiedDate);
                para.Add("@ModifiedBy", salepaperrate.ModifiedBy);
                para.Add("@DeleteStatus", "ACTIVE");
                var value = con.Query<int>("SP_Insert_Update_SalePaperRateMaster", para, null, true, 0, CommandType.StoredProcedure);
                con.Close();
            }
        }

        public void DeleteSalePaperRate(string salePaperRateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@SalePaperRateId", salePaperRateId); // Normal Parameters  
                var value = con.Query("SP_Delete_SalePaperRateMaster", para, null, true, 0, CommandType.StoredProcedure);
            }
        }
        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool CityPaperRateExists(double CityPaperRate, int PaperId, int CityId)
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


        public IEnumerable<SalePaperRateDTO> GetCityByStateId(string StateId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@StateId", StateId);
                var Plan_list = con.Query<SalePaperRateDTO>("Usp_GetCityByStateId", paramater, null, true, 0, CommandType.StoredProcedure);
                return Plan_list;
            }
        }
        public IEnumerable<SalePaperRateDTO> GetPaperByCityId(string CityId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@CityId", CityId);
                var Plan_list = con.Query<SalePaperRateDTO>("Usp_GetPaperByCityId", paramater, null, true, 0, CommandType.StoredProcedure);
                return Plan_list;
            }
        }


        public string GetSalePaperRateByPaperId(string PaperId, string Tdate)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {

                var para = new DynamicParameters();
                para.Add("@PaperId", PaperId);
                DateTime currentdate = Convert.ToDateTime(Tdate);
                string curday = currentdate.DayOfWeek.ToString();
                switch (curday)
                {
                    case "Monday":
                        var MondayRate = con.Query<string>("Usp_GetSalePaperRateByPaperId_Monday", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                        salepaperRate = MondayRate;
                        break;
                    case "Tuesday":
                        var TuesdayRate = con.Query<string>("Usp_GetSalePaperRateByPaperId_Tuesday", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                        salepaperRate = TuesdayRate;
                        break;
                    case "Wednesday":
                        var WednesdayRate = con.Query<string>("Usp_GetSalePaperRateByPaperId_Wednesday", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                        salepaperRate = WednesdayRate;
                        break;
                    case "Thursday":
                        var ThursdayRate = con.Query<string>("Usp_GetSalePaperRateByPaperId_Thursday", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                        salepaperRate = ThursdayRate;
                        break;
                    case "Friday":
                        var FridayRate = con.Query<string>("Usp_GetSalePaperRateByPaperId_Friday", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                        salepaperRate = FridayRate;
                        break;
                    case "Saturday":
                        var SaturdayRate = con.Query<string>("Usp_GetSalePaperRateByPaperId_Saturday", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                        salepaperRate = SaturdayRate;
                        break;
                    case "Sunday":
                        var SundayRate = con.Query<string>("Usp_GetSalePaperRateByPaperId_Sunday", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
                        salepaperRate = SundayRate;
                        break;
                }
                return salepaperRate;
            }
        }




    }
}