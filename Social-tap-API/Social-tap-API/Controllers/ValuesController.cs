using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using SocialtapAPI;
using Newtonsoft.Json.Linq;

namespace Social_tap_API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller, IValues
    {
        public static Dictionary<string, BarData> _barData = new Dictionary<string, BarData>();
        public static string _bestBar;
        public static double _all_bars_average;
        public static int _uses;
        public static string _stats;
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
            _stats = calc.Stats();
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
        /// Iškvietus šitą metodą 
        /// Sukuriama informacija 
        /// Apie geriausią barą 
        /// Ir bendrus skaičius
        [HttpGet]
        [Route("Stats")]
        public string GetStats()
        {
            Console.WriteLine($"GET: Stats/");

            return _stats;
        }

    }
}
