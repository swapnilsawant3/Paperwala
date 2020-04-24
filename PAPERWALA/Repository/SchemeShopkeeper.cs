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
    public class SchemeShopkeeper
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());

        public void InsertSchemeShopkeepe(SchemeShopkeeperDTO SShop)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@SchemeId", SShop.SchemeId); // Normal Parameters  
            para.Add("@SchemeName", SShop.SchemeName);
            para.Add("@SchemeDuration", SShop.SchemeDuration);
            para.Add("@SchemeAmount", SShop.SchemeAmount);
            para.Add("@Note", SShop.Note);
            
            para.Add("@CreatedDate", SShop.CreatedDate);
            para.Add("@CreatedBy", SShop.CreatedBy);
            para.Add("@ModifiedDate", SShop.ModifiedDate);
            para.Add("@ModifiedBy", SShop.ModifiedBy);
            para.Add("@DeleteStatus", "ACTIVE");
            var value = con.Query<int>("SP_Insert_Update_SchemeShopkeeper", para, null, true, 0, CommandType.StoredProcedure);
            con.Close();
        }


        public IEnumerable<SchemeShopkeeperDTO> GetSchemeShopkeepes()
        {
            con.Open();
            var Listscheme = con.Query<SchemeShopkeeperDTO>("select * from Mst_SchemeShopkeeper AS sshop Where sshop.DeleteStatus='ACTIVE'", null, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return Listscheme;
        }

        public SchemeShopkeeperDTO GetSchemeShopkeepeByID(string SShopId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string Query = "select * from Mst_SchemeShopkeeper where SchemeId =" + SShopId;

                var Scheme_list = con.Query<SchemeShopkeeperDTO>(Query, null, null, true, 0, CommandType.Text).Single();

                return Scheme_list;
            }

        }

        public void UpdateSchemeShopkeepe(SchemeShopkeeperDTO SShop)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@SchemeId", SShop.SchemeId); // Normal Parameters  
            para.Add("@SchemeName", SShop.SchemeName);
            para.Add("@SchemeDuration", SShop.SchemeDuration);
            para.Add("@SchemeAmount", SShop.SchemeAmount);
            para.Add("@Note", SShop.Note);

            para.Add("@CreatedDate", SShop.CreatedDate);
            para.Add("@CreatedBy", SShop.CreatedBy);
            para.Add("@ModifiedDate", SShop.ModifiedDate);
            para.Add("@ModifiedBy", SShop.ModifiedBy);
            para.Add("@DeleteStatus", "ACTIVE");
            var value = con.Query<int>("SP_Insert_Update_SchemeShopkeeper", para, null, true, 0, CommandType.StoredProcedure);
            con.Close();
        }

        public void DeleteSchemeShopkeepe(string SShopId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@CityId", SShopId); // Normal Parameters  
                var value = con.Query("SP_Delete_CityMaster", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}