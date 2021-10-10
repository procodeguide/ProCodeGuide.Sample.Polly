using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProCodeGuide.Polly.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private Dictionary<int, string> _customerNameDict = null;

        public CustomerController()
        {
            if (_customerNameDict == null)
            {
                _customerNameDict = new Dictionary<int, string>();
                _customerNameDict.Add(1, "Pro Code Guide");
                _customerNameDict.Add(2, "Support - Pro Code Guide");
                _customerNameDict.Add(3, "Sanjay");
                _customerNameDict.Add(4, "Sanjay - Pro Code Guide");
            }
        }

        [HttpGet]
        [Route("GetCustomerName/{customerCode}")]
        public ActionResult<string> GetCustomerName(int customerCode)
        {
            if (_customerNameDict != null && _customerNameDict.ContainsKey(customerCode))
            {
                return _customerNameDict[customerCode];
            }
            return "Customer Not Found";
        }

        [HttpGet]
        [Route("GetCustomerNameWithTempFailure/{customerCode}")]
        public ActionResult<string> GetCustomerNameWithTempFailure(int customerCode)
        {
            try
            {
                Random rnd = new Random();
                int randomError = rnd.Next(1, 11);  // creates a number between 1 and 10

                if (randomError % 2 == 0)
                    throw new Exception();

                if (_customerNameDict != null && _customerNameDict.ContainsKey(customerCode))
                {
                    return _customerNameDict[customerCode];
                }
                return "Customer Not Found";
            }
            catch
            {
                //Log Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetCustomerNameWithDelay/{customerCode}")]
        public ActionResult<string> GetCustomerNameWithDelay(int customerCode)
        {
            Thread.Sleep(new TimeSpan(0, 2, 0));
            if (_customerNameDict != null && _customerNameDict.ContainsKey(customerCode))
            {
                return _customerNameDict[customerCode];
            }
            return "Customer Not Found";
        }

        [HttpGet]
        [Route("GetCustomerNameWithPermFailure/{customerCode}")]
        public ActionResult<string> GetCustomerNameWithPermFailure(int customerCode)
        {
            try
            {
                throw new Exception("Database Not Available");
            }
            catch
            {
                //Log Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
