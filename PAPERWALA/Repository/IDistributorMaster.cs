using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAPERWALA.Models;

namespace PAPERWALA.Repository
{
    public interface IDistributorMaster
    {
        void InsertDistributor(DistributorDTO distributor); // C
        void ChangePassword(DistributorDTO distributor); // C
        void ChangePasswordAPI(DistributorDTO distributor); // C
        IEnumerable<DistributorDTO> GetDistributors(); // R
        DistributorDTO GetDistributorByID(string DistributorId); // R
        void UpdateDistributor(DistributorDTO distributor); //U

        string GetPassword(string Password);//C-P
        string GetSumBalanceAmount(string DistId);//C-P
        
        string GetPasswordAPI(string Password);//API
        void DeleteDistributor(string DistributorId); //D
        //void Save();
       // bool DistributorExists(string DistributorName, int StateId, int CityId);
        IEnumerable<DistributorDTO> GetCityByStateId(string StateId); // R

        int GetDistributorCount();
    }
}