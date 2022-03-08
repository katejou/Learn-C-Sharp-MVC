using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_APi_Ex.Helpers;
using Web_APi_Ex.Models;

namespace Web_APi_Ex.Controllers
{
   
    public class CarsController : ApiController
    {

        List<CarSales> CarSalesNumber;

        // 這個是建構子！給值上面的屬性
        public CarsController()
        {
            Utility util = new Utility();
            var random1 = util.getNumbers(12);
            var random2 = util.getNumbers(12);

            CarSalesNumber = new List<CarSales> 
            { 
                new CarSales{ Id = 1,Car="BMW", Salesdata = random1 },
                new CarSales{ Id = 2,Car="BENZ", Salesdata = random2}
            };
        }

        // URL api/cars
        [AcceptVerbs("GET", "POST")]
        public IEnumerable<CarSales> getCarSalesNumber() 
        {
            return CarSalesNumber;
        }

        //URL api/cars/2
        //根據汔車Id找出銷售資料
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult getSingleCarSalesNumber(int id) 
        {
            var car = CarSalesNumber.FirstOrDefault(c=> c.Id == id);
            if (car == null) 
            {
                return NotFound();      
            }
            return Ok(car);
        }






    }
}
