using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using SocialtapAPI;


namespace Social_tap_API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller, IValues
    {
        public static Dictionary<string, BarData> _barData = new Dictionary<string, BarData>();
        public ValuesController(){

        }
        /// Metodui paduodami 4 parametrai
        /// Patikrinama ar jie atitinka reikalavimus
        /// Suvienodinami pavadinimai
        /// Naudojant kitus metodus 
        /// į _barData Dictionary sudedamos reikšmės
        [HttpPost("AddBarReview/{barName}/{comment}/{rate}/{beverage}")]
        public Boolean AddBarReview(string barName, string comment, int rate, int beverage) //rate-žvaigždutės, beverage-įpilto alaus lygis
        {
            Calculations calc = new Calculations();
            Console.WriteLine($"POST: AddBarReview/{barName}/{comment}/{rate}/{beverage}");
            if (!calc.Validation(rate, beverage, barName))
            {
                return false;
            }
            barName = calc.BarNameAdaptation(barName);
            _barData= calc.AddBarInfo(barName, beverage, rate, comment);
            return true;

        }
        /// Iškvietus šitą metodą
        /// Jis grąžina visą Dictionary į programą
        [HttpGet]
        [Route("GetBarData")]
        public IDictionary<string, BarData> GetBarData()
        {
            Console.WriteLine($"GET: GetBarData/");
            return _barData;

        }
        
    }
}
