using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAPERWALA.Models;


namespace PAPERWALA.Repository
{
    public interface ISellerMaster
    {
        void InsertSeller(SellerDTO Seller); // C
        void ChangePassword(SellerDTO distributor); // C
        void ChangePasswordAPI(SellerDTO distributor); // C
        IEnumerable<SellerDTO> GetSellers(); // R
        SellerDTO GetSellerByID(string SellerId); // R
        void UpdateSeller(SellerDTO Seller); //U
        string GetPassword(string Password);//C-P
        string GetPasswordAPI(string Password);//API
        void DeleteSeller(string SellerId); //D
                                                      //void Save();
                                                      // bool DistributorExists(string DistributorName, int StateId, int CityId);
        IEnumerable<SellerDTO> GetCityByStateId(string StateId); // R

        int SellerCount();
    }
}