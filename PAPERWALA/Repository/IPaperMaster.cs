using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAPERWALA.Models;

namespace PAPERWALA.Repository
{
    public interface IPaperMaster
    {
        void InsertPaper(PaperDTO paper); // C
        IEnumerable<PaperDTO> GetPapers(); // R
        PaperDTO GetPaperByID(string PaperID); // R
        void UpdatePaper(PaperDTO paper); //U
        void DeletePaper(string PaperId); //D
        //void Save();
        bool PaperNameExists(string PaperName , int StateId);
        IEnumerable<PaperDTO> GetPaperByCityId(string CityId); // R
        IEnumerable<PaperDTO> GetPaperByCityIdnDistributorId(string CityId,string DistributorId); // R
        
        IEnumerable<PaperDTO> GetPaperByCityIdBySession(string CityId); // R
        
        int GetPaperCount();
    }
}