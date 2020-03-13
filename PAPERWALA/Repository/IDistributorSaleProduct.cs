using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAPERWALA.Models;

namespace PAPERWALA.Repository
{
    public interface IDistributorSaleProduct
    {
        void InsertDistributorSaleProduct(DistributorSaleProductDTO DistributorSaleProduct); // C
        List<DisctributorSaleProductDTO> GetDistributorSaleProducts(string SaleOrder); // R
           
        void DeleteDistributorSaleProduct(string DistributorSaleProductId); //D
        //IEnumerable<DistributorSaleProductDTO> GetRetailerByDistributerId(string DistributorId); // R
        IEnumerable<DistributorSaleProductDTO> GetPaperByCityId(string CityId); // R
    }
}