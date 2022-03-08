using Chart_ex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Web_APi_Ex.Helpers;
using Web_APi_Ex.Models;
using AcceptVerbsAttribute = System.Web.Http.AcceptVerbsAttribute;

namespace Chart_ex.Controllers
{
   
    public class CarsController : ApiController
    {

        List<Car> CarSalesNumber;

        // 這個是建構子！給值上面的屬性
        public CarsController()
        {
            Utility util = new Utility();
            var random1 = util.getNumbers(3);
            var random2 = util.getNumbers(3);

            CarSalesNumber = new List<Car> 
            { 
                new Car{ ID = 1, Brand ="BMW" , Sales = random1 },
                new Car{ ID = 2, Brand ="BENZ", Sales = random2}
            };
        }

        // URL api/cars
        [AcceptVerbs("GET", "POST")]
        public IEnumerable<Car> getCarSalesNumber() 
        {
            return CarSalesNumber;
        }

        //URL api/cars/2
        //根據汔車Id找出銷售資料
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult getSingleCarSalesNumber(int id) 
        {
            var car = CarSalesNumber.FirstOrDefault(c=> c.ID == id);
            if (car == null) 
            {
                return NotFound();      
            }
            return Ok(car);
        }

    }
}
