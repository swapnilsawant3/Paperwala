using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAPERWALA.Models;

namespace PAPERWALA.Repository
{
    public interface IDistributorTransactions
    {
        void InsertDistributorTransaction(DistributorTransactionDTO DistributorTrans); // C
        IEnumerable<DistributorTransactionDTO> GetDistributorTransactions(string DistributorId); // R
       // DistributorTransactionDTO GetPaperRateByID(string PaperRateId); // R
       // void UpdatePaperRate(DistributorTransactionDTO paperrate); //U
        void DeleteDistributorTransaction(string TransDistributorId); //D
        //void Save();
       // bool DistributorTransactionExists(double CityPaperRate, int PaperId, int CityId);
        IEnumerable<DistributorTransactionDTO>GetRetailerByDistributerId( string DistributorId); // R
        IEnumerable<DistributorTransactionDTO>GetPaperByCityId(string CityId); // R
       
        //IEnumerable<DistributorTransactionDTO> GetRetailerByDistributerId(DistributorTransactionDTO distributorid);
        // int GetPaperRateCount();
    }
}