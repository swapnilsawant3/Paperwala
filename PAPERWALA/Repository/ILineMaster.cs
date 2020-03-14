using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAPERWALA.Models;

namespace PAPERWALA.Repository
{
    public interface ILineMaster
    {
        void InsertLine(LineDTO line); // C
        IEnumerable<LineDTO> GetLines(); // R
        LineDTO GetLineByID(string LineID); // R
        void UpdateLine(LineDTO line); //U
        void DeleteLine(string LineId); //D
        //void Save();
        bool LineExists(string LineName);
        IEnumerable<LineDTO> GetCityByStateId(string StateId); // R

       
    }
}