﻿using System;
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
        private static double _sum;
        private static int _uses;
        private static List<string> _hashTags;
        private static Dictionary<string, List<string>> _barInfo;
        private static Dictionary<string, List<int>> _barRates;
        
        public ValuesController()
        {
            _hashTags = new List<string>();
            _barInfo = new Dictionary<string, List<string>>();
            _barRates = new Dictionary<string, List<int>>();
        }
        /// VIETA PAAIŠKINIMUI KĄ DARO
        /// http://localhost:.../api/values/barrate/string_baropavadinimas/string_įvertinimas
        [HttpPost("barrate/{barName}/{rate}")] 
        public double BarRateAverage(string barName, int rate)
        {
            barName = barName.ToUpper();
            //pasalinam visus tarpus, taškus ir -
            barName = barName.Replace(" ", string.Empty).Replace("-", string.Empty).Replace(".", string.Empty); 

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
        
        /// VIETA PAAIŠKINIMUI KĄ DARO
        /// http://localhost:.../api/values/bevlvl/INT
        [HttpPost("bevlvl/{beverageLevel}")]    
        public bool Average(int beverageLevel)
        {
            _uses++;
            _sum += beverageLevel;
            // sakysime, kad jeigu lygus vidurkiui, tai ipilta geriau
            return _sum / _uses <= beverageLevel;  
        }
        /// VIETA PAAIŠKINIMUI KĄ DARO
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

        /// VIETA PAAIŠKINIMUI KĄ DARO
        /// http://localhost:.../api/values/names/STRING_baroPavadinimas/STRING_komentaras
        [HttpPost("names/{barName}/{comment}")]
        public Dictionary<string, List<string>> CountBars(string barName, string comment)
        {
            barName = barName.ToUpper();
            // pasalinam visus tarpus, taškus ir -
            barName = barName.Replace(" ", string.Empty).Replace("-", string.Empty).Replace(".", string.Empty); 
            
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
