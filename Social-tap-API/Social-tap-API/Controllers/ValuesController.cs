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
        public const int MAX_BEVERAGE_LEVEL = 10;
        public const int MAX_RATE = 5;
        public const int MIN_NAME_LENGHT = 1;
        public const int MIN_BEVERAGE_RATE_LEVEL = 0;
        private static double _sum;
        private static int _uses;
        private List<string> _hashTags = new List<string>();
        private static Dictionary<string, List<string>> _barInfo;
        private static Dictionary<string, List<int>> _barRates = new Dictionary<string, List<int>>();
        public static Dictionary<string, BarData> _barData = new Dictionary<string, BarData>();
        public ValuesController()
        {
            _barInfo = new Dictionary<string, List<string>>();
        }
        /// Metodui paduodami 4 parametrai
        /// Patikrinama ar jie atitinka reikalavimus
        /// Suvienodinami pavadinimai
        /// Naudojant kitus metodus 
        /// į _barData Dictionary sudedamos reikšmės
        [HttpPost("AddBarReview/{barName}/{comment}/{rate}/{beverage}")]
        public Boolean AddBarReview(string barName, string comment, int rate, int beverage) //rate-žvaigždutės, beverage-įpilto alaus lygis
        {

            if (rate > MAX_RATE || beverage > MAX_BEVERAGE_LEVEL || barName.Length < MIN_NAME_LENGHT || rate<= MIN_BEVERAGE_RATE_LEVEL || beverage<= MIN_BEVERAGE_RATE_LEVEL)
            {
                return false;
            }
            barName = barName.ToLower();
            barName = barName.Replace(" ", string.Empty).Replace("-", string.Empty).Replace(".", string.Empty);  // pasalinam visus tarpus, taškus ir -
            barName = char.ToUpper(barName[0]) + barName.Substring(1);
            if (!_barData.Keys.Contains(barName))
            {
                _barData.Add(barName,new BarData());
            }
            
            _barData[barName].BarUses++;
            _barData[barName].BeverageSum += beverage;
            _barData[barName].Comparison = Average(beverage);
            _barData[barName].Tags.AddRange(HashtagsFinder(comment)); 
            _barData[barName].RateAvg = BarRateAverage(barName, rate);
            _barData[barName].BeverageAvg = _barData[barName].BeverageSum/ _barData[barName].BarUses;
            return true;

        }
        /// Iškvietus šitą metodą
        /// Jis grąžina visą Dictionary į programą
        [HttpGet]
        [Route("GetBarData")]
        public IDictionary<string, BarData> GetBarData()
        {
            return _barData;

        }

        /// Į metodą paduodamas baro pavadinimas ir jo įvertinimas
        /// Web service laikome baro pavadinimą ir jo įvertinimų Listą 
        /// Atgal grąžiname tik baro įvertinimų vidurkį
        /// http://localhost:.../api/values/barrate/string_baropavadinimas/string_įvertinimas

        public double BarRateAverage(string barName, int rate)
        {

            if (_barRates.Keys.Contains(barName))
            {
                _barRates[barName].Add(rate);
            }
            else
            {
                _barRates[barName] = new List<int> { rate };
            }
            return _barRates[barName].Average();
        }
        
        /// Į metodą paduodama informacija kiek buvo įpilta 
        /// Web service saugoma informacija apie tai kiek kartų buvo pasinaudota programa 
        /// Grąžiname true jeigu įpilta geriau, false jeigu blogau. 
        /// Lyginimas vyksta ne su vieno baro statistika o su BENDRA 
        /// http://localhost:.../api/values/bevlvl/INT
       // [HttpPost("bevlvl/{beverageLevel}")]    
        public Boolean Average(int beverageLevel)
        {
            _uses++;
            _sum += beverageLevel;
            // sakysime, kad jeigu lygus vidurkiui, 
            // tai ipilta geriau
            return _sum / _uses <= beverageLevel;  
        }
        /// Į metodą paduodamas komentaras 
        /// Iš jo išrenkami žodžiai panaudoti su hashtagu 
        /// Grąžinamas hashtagų listas 
        /// http://localhost:.../api/values/tags/STRING
        //  [HttpPost("tags/{comment}")]
        public List<string> HashtagsFinder(string comment) 
        {
            /*
             * Hashtag'ą programoje reikės pakeist kuo nors kitu naudojant kintamasis.
             * Replace("#","Ę"), ir tada passinti į web API, kol kas dedu Ę nes neturėtų
             * būti naudojamas komentaruose kaip pirma raidė sakinio. Bet galima sugalvoti kuo kitu keisti.
             */
            var regex = new Regex(@"(?<=Ę)\w+");          
            var matches = regex.Matches(comment);

            foreach (Match m in matches)
            {
                _hashTags.Add(m.Value);

            }
            return _hashTags;
        }

        /// Į metodą paduodamas baro pavadinimas ir komentaras 
        /// Su baro pavadinimu, kuris yra Dictionary raktas pridedami hashtagai. 
        /// Jie gaunami iškvietus HashtagsFinder metodą
        /// Grąžinamas dictionary.
        /// http://localhost:.../api/values/names/STRING_baroPavadinimas/STRING_komentaras
       // [HttpPost("names/{barName}/{comment}")]
        public Dictionary<string, List<string>> CountBars(string barName, string comment)
        {
            
            if (_barInfo.Keys.Contains(barName))
            {
                _barInfo[barName].AddRange(HashtagsFinder(comment));
            }
            else
            {
                _barInfo[barName] = HashtagsFinder(comment);
            }
            
            return _barInfo;
        }
    }
}
