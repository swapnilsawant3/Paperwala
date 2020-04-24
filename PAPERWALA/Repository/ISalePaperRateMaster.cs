using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAPERWALA.Models;

namespace PAPERWALA.Repository
{
    public interface ISalePaperRateMaster
    {

        void InsertSalePaperRate(SalePaperRateDTO salepaperrate); // C
        IEnumerable<SalePaperRateDTO> GetSalePaperRates(); // R
        SalePaperRateDTO GetSalePaperRateByID(string SalePaperRateId); // R
        void UpdateSalePaperRate(SalePaperRateDTO salepaperrate); //U
        void DeleteSalePaperRate(string salePaperRateId); //D
        //void Save();
        bool CityPaperRateExists(double CityPaperRate, int PaperId, int CityId);
        IEnumerable<SalePaperRateDTO> GetCityByStateId(string StateId); // R
        IEnumerable<SalePaperRateDTO> GetPaperByCityId(string CityId); // R
        string GetSalePaperRateByPaperId(string PaperId, string Tdate); // R
        int GetSalePaperRateCount();

    }
}