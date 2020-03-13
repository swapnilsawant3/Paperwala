using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAPERWALA.Models;

namespace PAPERWALA.Repository
{
    public interface ILoginDistributor
    {
        IEnumerable<DistributorDTO> GetAllUsers();
        string GetUserID_By_UserName(string UserName);
        bool Get_CheckUserExist(string UserName, string Password);
        string GetDesignationByUserID(string UserId);
        string GetStateIdByUserID(string UserId);
        string GetCityIdByUserID(string UserId);
    }
}