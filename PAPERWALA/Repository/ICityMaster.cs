using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAPERWALA.Models;

namespace PAPERWALA.Repository
{
    public interface ICityMaster
    {
        void InsertCity(CityDTO city); // C
        IEnumerable<CityDTO> GetCitys(); // R
        CityDTO GetCityByID(string CityID); // R
        void UpdateCity(CityDTO city); //U
        void DeleteCity(string CityId); //D
        //void Save();
        bool CityNameExists(string CityName);
        IEnumerable<CityDTO> GetCityByStateId(string StateId); // R

        int GetCityCount();
    }
}