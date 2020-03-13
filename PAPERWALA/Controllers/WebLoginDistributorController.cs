using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PAPERWALA.Models;
using PAPERWALA.Repository;
using System.Web.Security;
using System.Web.Mvc;

namespace PAPERWALA.Controllers
{
   
    public class WebLoginDistributorController : ApiController
    {
        static readonly ILoginDistributor objLoginDistributor = new LoginDistributor();

        public DistributorDTO GetLogin(string username, string password)
        {
            DistributorDTO lgdistributor = new DistributorDTO();
            bool success = objLoginDistributor.Get_CheckUserExist(username, password);
            var UserID = objLoginDistributor.GetUserID_By_UserName(username);
            var StateID = objLoginDistributor.GetStateIdByUserID(UserID);
            var CityID = objLoginDistributor.GetCityIdByUserID(UserID);
            var Designation = objLoginDistributor.GetDesignationByUserID(Convert.ToString(UserID));
            if (success == true)
            {
                lgdistributor.DistributorId = Convert.ToInt32(UserID);
                lgdistributor.Designation = Designation;
                lgdistributor.UserName = username;
                lgdistributor.StateId = Convert.ToInt32(StateID);
                lgdistributor.CityId = CityID;
            }
            else
            {

            }

            return (lgdistributor);

        }
    }
}
