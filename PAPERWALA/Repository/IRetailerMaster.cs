using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAPERWALA.Models;
namespace PAPERWALA.Repository
{
    public interface IRetailerMaster
    {
        void InsertRetailer(RetailerDTO retailer); // C
        IEnumerable<RetailerDTO> GetRetailers(string DistributorId); // R
        IEnumerable<RetailerDTO> webGetRetailers(string DistributorId); // R
        IEnumerable<RetailerDTO> webGetRetailerById(string RetailerId); // R
        RetailerDTO GetRetailerByID(string RetailerId); // R
        void UpdateRetailer(RetailerDTO retailer); //U
        void DeleteRetailer(string RetailerId); //D
        //void Save();
        bool RetailerExists(string RetailerName, int? StateId, int? CityId);
        IEnumerable<RetailerDTO> GetCityByStateId(string StateId); // R
        string GetPrevBalalnceByRetailerId(string PaperId); // R
        string GetRetailerCount(string DistributorId);
        
    }
}