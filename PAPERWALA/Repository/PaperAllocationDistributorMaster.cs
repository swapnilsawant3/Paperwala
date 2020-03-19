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
    public class PaperAllocationDistributorMaster : IPaperAllocationDistributorMaster
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString());

        public void InsertPaperAllocationDistributor(PaperAllocationDistributorDTO PAllocation)
        {
            con.Open();
            var para = new DynamicParameters();
            para.Add("@PaperAllocateId", PAllocation.PaperAllocateId); // Normal Parameters  
            para.Add("@PaperId", PAllocation.PaperId);
            para.Add("@CityId", PAllocation.CityId);
            para.Add("@DistributorId", PAllocation.DistributorId);
            para.Add("@CreatedDate", PAllocation.CreatedDate);
            para.Add("@CreatedBy", PAllocation.CreatedBy);
            para.Add("@ModifiedDate", PAllocation.ModifiedDate);
            para.Add("@ModifiedBy", PAllocation.ModifiedBy);
            para.Add("@DeleteStatus", "ACTIVE");
            var value = con.Query<int>("SP_Insert_PaperAllocateDistributorMaster", para, null, true, 0, CommandType.StoredProcedure);
            con.Close();
        }

        public IEnumerable<PaperAllocationDistributorDTO> GetPaperAllocationDistributor()
        {
            con.Open();
            var Listscheme = con.Query<PaperAllocationDistributorDTO>("select pa.*,ct.CityId,ct.CityName, p.PaperId,p.PaperName,dt.DistributorName from Mst_PaperAllocateDistributor AS pa LEFT JOIN Mst_City AS ct ON pa.CityId=ct.CityId Left Join Mst_Paper as p on pa.PaperId=p.PaperId Left Join Mst_Distributor as dt on pa.DistributorId=dt.DistributorId  Where pa.DeleteStatus='ACTIVE'", null, null, true, 0, CommandType.Text).ToList();
            con.Close();
            return Listscheme;
        }

        public void DeletePaperAllocationDistributor(string PaperAllocationId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@PaperAllocationId", PaperAllocationId); // Normal Parameters  
                var value = con.Query("SP_Delete_PaperAllocationDistributorMaster", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }


    }
}