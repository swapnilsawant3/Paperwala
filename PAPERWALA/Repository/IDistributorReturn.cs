using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAPERWALA.Models;

namespace PAPERWALA.Repository
{
    public interface IDistributorReturn
    {
        void InsertDistributorReturn(DistributorReturnDTO DistributorReturn); // C
        void UpdateDistributorReturn(DistributorReturnDTO DistributorTrans); // C
        void UpdateDistributorReturnAPI(DistributorReturnDTO DistributorTrans); // API
        IEnumerable<DistributorReturnDTO> GetDistributorReturns(string DistributorId); // R

        IEnumerable<DistributorReturnDTO> GetDistributorReturnsAPI(string DistributorId); // R
        IEnumerable<DistributorReturnDTO> GetDistributorReturnsMainTransactionByReturnOrderAPI( string ReturnOrder); // R

        // DistributorTransactionDTO GetPaperRateByID(string PaperRateId); // R
        //void UpdateDistributorReturn(DistributorReturnDTO DistributorTrans); //U
        void DeleteDistributorReturn(string DistributorReturnId); //D
                                                              //void Save();
                                                              // bool DistributorTransactionExists(double CityPaperRate, int PaperId, int CityId);
        IEnumerable<DistributorReturnDTO> GetRetailerByDistributerId(string DistributorId); // R
                                                                                          // IEnumerable<DistributorReturnDTO> GetPaperByCityId(string CityId); // R

        //IEnumerable<DistributorTransactionDTO> GetRetailerByDistributerId(DistributorTransactionDTO distributorid);
        // int GetPaperRateCount();

        //Return Product

        void InsertDistributorReturnProduct(DistributorReturnProductDTO DistributorReturnProduct); // C
        void InsertDistributorReturnProductAPI(DistributorReturnDTO DistributorReturnProduct); // C
        IEnumerable<DistributorReturnDTO> GetDistributorReturnProducts(string ReturnOrder); // R

        void DeleteDistributorReturnProduct(string DistributorReturnProductId); //D
        //IEnumerable<DistributorReturnProductDTO> GetRetailerByDistributerId(string DistributorId); // R
        IEnumerable<DistributorReturnProductDTO> GetPaperByCityId(string CityId); // R
        IEnumerable<DistributorReturnProductDTO> GetPaperByCityIdnDistributor(string CityId, string DistributorId); // R
        string GetFinalAmount_By_ReturnOrder(string ReturnOrder);
        string GetFinalAmount_By_ReturnOrder_API(string ReturnOrder);
    }
}