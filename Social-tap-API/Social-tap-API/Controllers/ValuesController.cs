using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocialtapAPI;

namespace Social_tap_API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller, IValuesController
    {
        public IStatistics Stats;  
        
        // Nenaudojami kintamamieji
        // public Bar Bars = new Bar();
        // public Review Reviews = new Review();
        // public string barName = "";
        // public Dictionary<string, IBarData> BarData = new Dictionary<string, IBarData>();
        // public static string BestBar;
        // public static double AllBarsAverage;
        // public static int Uses;
        
        
        /// <summary>
        /// Metodui paduodami 4 parametrai
        /// Patikrinama ar jie atitinka reikalavimus
        /// Suvienodinami pavadinimai
        /// Naudojant kitus metodus 
        /// į _barData Dictionary sudedamos reikšmės
        /// </summary>
        /// <param name="barName">baro pavadinimas</param>
        /// <param name="comment">komentaras</param>
        /// <param name="rate">žvaigždutės</param>
        /// <param name="beverage">įpilto alaus lygis</param>
        /// <returns>pridėjimo statusas</returns>
        [HttpPost("AddBarReview/{barName}/{comment}/{rate}/{beverage}")]
        public bool AddBarReview(string barName, string comment, int rate, int beverage) 
        {
            
            ICalculations calc = new Calculations();
            Console.WriteLine($"POST: AddBarReview/{barName}/{comment}/{rate}/{beverage}");
            if (!calc.Validation(rate, beverage, barName)) return false;
            barName = calc.BarNameAdaptation(barName);
            calc.AddReview(barName, rate, beverage);
            calc.AddBar(barName);
            
            return true;

        }
        /// Iškvietus šitą metodą
        /// Jis grąžina visą Dictionary į programą
        [HttpGet, Route("GetBarData")]
        public IDictionary<string, IBarData> GetBarData()
        {
            Console.WriteLine("GET: GetBarData/");
            var calc = new Calculations();
            return calc.GetBarData();

        }
        /// Iškvietus šitą metodą 
        /// Sukuriama informacija 
        /// Apie geriausią barą 
        /// Ir bendrus skaičius
        [HttpGet, Route("Stats")]
        public IStatistics GetStats()
        {
            Console.WriteLine("GET: Stats/");
            Stats = new Calculations().Stats();
            return Stats ?? new Statistics();
        }

    }
}
