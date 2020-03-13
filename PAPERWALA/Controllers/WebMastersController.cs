using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PAPERWALA.Models;
using PAPERWALA.Repository;

namespace PAPERWALA.Controllers
{
    public class WebMastersController : ApiController
    {
        static readonly IStateMaster objStateM = new StateMaster();
        static readonly ICityMaster objCityM = new CityMaster();
        // static readonly IPhotoGallery objPhotogall = new PhotoGallery();
        // static readonly INewsGallery objNewsgall = new NewsGallery();

        [HttpGet]
        [Route("api/webmasters/StateList")]
        public IEnumerable<StateDTO> StateList()
        {
            return objStateM.GetStates();
        }

        [HttpGet]
        [Route("api/webmasters/CityList")]
        public IEnumerable<CityDTO> CityList()
        {
            return objCityM.GetCitys();
        }

        [HttpGet]
        [Route("api/webmasters/GetCityByStateId")]
        public IEnumerable<CityDTO> GetCityByStateId(string stateid)
        {
            return objCityM.GetCityByStateId(stateid);
        }
    }
}
