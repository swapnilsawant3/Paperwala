using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAPERWALA.Models;

namespace PAPERWALA.Repository
{
    public interface IDesignationMaster
    {
        void InsertDesignation(DesignationDTO designation); // C
        IEnumerable<DesignationDTO> GetDesignations(); // R
        DesignationDTO GetDesignationByID(string DesignationId); // R
        void UpdateDesignation(DesignationDTO designation); //U
        void DeleteDesignation(string DesignationId); //D
        //void Save();
        bool DesignationNameExists(string DesignationName);

        int GetDesignationCount();
    }
}