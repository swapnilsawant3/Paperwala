using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAPERWALA.Models;

namespace PAPERWALA.Repository
{
    public interface IPaperAllocationDistributorMaster
    {
        void InsertPaperAllocationDistributor(PaperAllocationDistributorDTO PAllocation); // C
        IEnumerable<PaperAllocationDistributorDTO> GetPaperAllocationDistributor(); // R
     
       
        void DeletePaperAllocationDistributor(string PaperAllocationId); //D
        //void Save();
       
    }
}