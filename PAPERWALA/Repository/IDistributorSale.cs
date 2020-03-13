using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAPERWALA.Models;

namespace PAPERWALA.Repository
{
    public interface IDistributorSale
    {
        void InsertDistributorSale(DistributorSaleDTO Distributorsale); // C
        void UpdateDistributorSale(DistributorSaleDTO DistributorTrans); // C
        void UpdateDistributorSaleAPI(DistributorSaleDTO DistributorTrans); // API
        IEnumerable<DistributorSaleDTO> GetDistributorSales(string DistributorId); // R

        IEnumerable<DistributorSaleDTO> GetDistributorSalesAPI(string DistributorId); // R
        IEnumerable<DistributorSaleDTO> GetDistributorSalesTransactionBySaleOrderAPI(string DistributorId,string SaleOrder); // R
        
        // DistributorTransactionDTO GetPaperRateByID(string PaperRateId); // R
        //void UpdateDistributorSale(DistributorSaleDTO DistributorTrans); //U
        void DeleteDistributorSale(string DistributorSaleId); //D
                                                                      //void Save();
                                                                      // bool DistributorTransactionExists(double CityPaperRate, int PaperId, int CityId);
        IEnumerable<DistributorSaleDTO> GetRetailerByDistributerId(string DistributorId); // R
                                                                                          // IEnumerable<DistributorSaleDTO> GetPaperByCityId(string CityId); // R

        //IEnumerable<DistributorTransactionDTO> GetRetailerByDistributerId(DistributorTransactionDTO distributorid);
        // int GetPaperRateCount();

        //Sale Product

        void InsertDistributorSaleProduct(DistributorSaleProductDTO DistributorSaleProduct); // C
        void InsertDistributorSaleProductAPI(DistributorSaleDTO DistributorSaleProduct); // C
        IEnumerable<DistributorSaleDTO> GetDistributorSaleProducts(string SaleOrder); // R

        void DeleteDistributorSaleProduct(string DistributorSaleProductId); //D
        //IEnumerable<DistributorSaleProductDTO> GetRetailerByDistributerId(string DistributorId); // R
        IEnumerable<DistributorSaleProductDTO> GetPaperByCityId(string CityId); // R
        string GetFinalAmount_By_SaleOrder(string SaleOrder);
        string GetFinalAmount_By_SaleOrder_API(string SaleOrder);

    }
}