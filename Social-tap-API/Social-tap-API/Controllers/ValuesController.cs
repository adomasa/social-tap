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
        public const int MAX_BEVERAGE_LEVEL = 10; //kiek daugiausiai gali ipilti
        public const int MAX_RATE = 5; // kiek daugiausiai gali duoti žvaigždučių 
        public const int MIN_NAME_LENGHT = 1; //trumpiausias įmanomas baro pavadinimas
        public const int MIN_BEVERAGE_RATE_LEVEL = 0; //kiek mažiausiai gali įpilti ir duoti žvaigždučių 
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
            if (!_barData.Keys.Contains(barName))
            {
                _barData.Add(barName,new BarData());
            }
            
            _barData[barName].BarUses++;
            _barData[barName].BeverageSum += beverage;
            _barData[barName].Comparison = calc.Average(beverage);
            _barData[barName].Tags.AddRange(calc.HashtagsFinder(comment)); 
            _barData[barName].RateAvg = calc.BarRateAverage(barName, rate);
            _barData[barName].BeverageAvg = _barData[barName].BeverageSum/ _barData[barName].BarUses;
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
