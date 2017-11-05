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
        static double sum;
        static int uses;
        public List<string> HashTags = new List<string>();
        public static Dictionary<string, List<string>> barInfo = new Dictionary<string, List<string>>();
        public static Dictionary<string, List<int>> barRates = new Dictionary<string, List<int>>();
        public ValuesController()
        {

        }
        [HttpPost("barrate/{barName}/{rate}")] //kad išsikviesti reikia vesti http://localhost:.../api/values/barrate/string_baropavadinimas/string_įvertinimas
        public double BarRateAverage(string barName, int rate)
        {
            barName = barName.ToUpper();
            barName = barName.Replace(" ", string.Empty).Replace("-", string.Empty).Replace(".", string.Empty); //pasalinam visus tarpus, taškus ir -

            if (barRates.Keys.Contains(barName))
            {
                barRates[barName].Add(rate);
            }
            else
            {
                barRates[barName] = new List<int> { rate };
            }
            return barRates[barName].Average();
        }
        [HttpPost("bevlvl/{beverageLevel}")]    // kad išsikviesti reikia vesti http://localhost:.../api/values/bevlvl/INT
        public bool Average(int beverageLevel)
        {
            uses++;
            sum += beverageLevel;
            // sakysime, kad jeigu lygus vidurkiui, tai ipilta geriau
            return sum / uses <= beverageLevel;  
        }
        //  [HttpPost("tags/{comment}")]
        public List<string> HashtagsFinder(string comment) // kad išsikviesti reikia vesti http://localhost:.../api/values/tags/STRING
        {
            var regex = new Regex(@"(?<=Ę)\w+");          /*Hashtag'ą programoje reikės pakeist kuo nors kitu naudojant kintamasis.Replace("#","Ę"), ir tada passinti į web API, 
                                                             kol kas dedu Ę nes neturėtų būti naudojamas komentaruose kaip pirma raidė sakinio. Bet galima sugalvoti kuo kitu keisti*/
            var matches = regex.Matches(comment);

            foreach (Match m in matches)
            {
                HashTags.Add(m.Value);

            }
            return HashTags;
        }

        [HttpPost("names/{barName}/{comment}")]
        public Dictionary<string, List<string>> CountBars(string barName, string comment) //kad išsikviesti reikia vesti 
        {
            // http://localhost:.../api/values/names/STRING_baroPavadinimas/STRING_komentaras
            barName = barName.ToUpper();
            barName = barName.Replace(" ", string.Empty).Replace("-", string.Empty).Replace(".", string.Empty); // pasalinam visus tarpus, taškus ir -
            if (barInfo.Keys.Contains(barName))
            {
                barInfo[barName].AddRange(HashtagsFinder(comment));
            }
            else
            {
                barInfo[barName] = HashtagsFinder(comment);
            }

            return barInfo;
        }
    }
}
