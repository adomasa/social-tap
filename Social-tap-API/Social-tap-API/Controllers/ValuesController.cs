﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Social_tap_API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {


        static double sum;
        static int uses;
        public static List<string> HashTags = new List<string>();
        public static List<string> BarsNames = new List<string>();
        public Dictionary<string, List<string>> barInfo = new Dictionary<string, List<string>>();
        [HttpGet("bevlvl/{beverageLevel}")]    // kad išsikviesti reikia vesti http://localhost:.../api/values/bevlvl/INT
        public string Average(int beverageLevel)
        {
            uses++;
            sum += beverageLevel;

            if (sum / uses <= beverageLevel) // sakysime, kad jeigu lygus vidurkiui, tai ipilta geriau 
            {
                return "Geriau įpylė";
            }
            else
            {
                return "Blogiau įyplė";
            }
        }
        [HttpGet("tags/{comment}")]
        public List<string> HashtagsFinder(string comment) // kad išsikviesti reikia vesti http://localhost:.../api/values/bevlvl/INT
        {
            var regex = new Regex(@"(?<=_)\w+");          /*Hashtag'ą programoje reikės pakeist kuo nors kitu naudojant kintamasis.Replace("#","_"), ir tada passinti į web API, 
                                                             kol kas dedu _ nes neturėtų būti naudojamas komentaruose*/
            var matches = regex.Matches(comment);

            foreach (Match m in matches)
            {
                HashTags.Add(m.Value);

            }
            return HashTags;
        }

        [HttpGet("names/{barName}/{comment}")]
        public Dictionary<string, List<string>> CountBars(string barName, string comment) // kad išsikviesti reikia vesti 
        {
            // http://localhost:.../api/values/names/STRING_baroPavadinimas/STRING_komentaras
            barName = barName.ToUpper();
            if (!barInfo.Keys.Contains(barName))
            {
                barInfo.Add(barName, HashtagsFinder(comment));
            }
            else
            {
                barInfo[barName] = HashtagsFinder(comment);
            }
            uses++;

            return barInfo;
        }

    }
}