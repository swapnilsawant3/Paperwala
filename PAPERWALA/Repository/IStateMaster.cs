using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAPERWALA.Models;

namespace PAPERWALA.Repository
{
    public interface IStateMaster
    {
        void InsertState(StateDTO state); // C
        IEnumerable<StateDTO> GetStates(); // R
        StateDTO GetStateByID(string StateId); // R
        void UpdateState(StateDTO state); //U
        void DeleteState(string StateId); //D
        //void Save();
        bool StateNameExists(string StateName);

        int GetStateCount();
    }
}