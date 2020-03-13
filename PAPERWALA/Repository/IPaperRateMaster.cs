using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAPERWALA.Models;

namespace PAPERWALA.Repository
{
    public interface IPaperRateMaster
    {
        void InsertPaperRate(PaperRateDTO paperrate); // C
        IEnumerable<PaperRateDTO> GetPaperRates(); // R
        PaperRateDTO GetPaperRateByID(string PaperRateId); // R
        void UpdatePaperRate(PaperRateDTO paperrate); //U
        void DeletePaperRate(string PaperRateId); //D
        //void Save();
        bool CityPaperRateExists(double CityPaperRate, int PaperId, int CityId);
        IEnumerable<PaperRateDTO> GetCityByStateId(string StateId); // R
        IEnumerable<PaperRateDTO> GetPaperByCityId(string CityId); // R
        string GetPaperRateByPaperId(string PaperId, string Tdate); // R
        int GetPaperRateCount();
    }
}