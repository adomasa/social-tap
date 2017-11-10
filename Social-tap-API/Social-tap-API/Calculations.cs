using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialtapAPI
{
    public class Calculations:ICalculations
    {
        private static Dictionary<string, List<int>> _barRates = new Dictionary<string, List<int>>();
        private List<string> _hashTags = new List<string>();
        static int _uses;
        static int _sum;
        public const int MAX_BEVERAGE_LEVEL = 10; //kiek daugiausiai gali ipilti
        public const int MAX_RATE = 5; // kiek daugiausiai gali duoti žvaigždučių 
        public const int MIN_NAME_LENGHT = 1; //trumpiausias įmanomas baro pavadinimas
        public const int MIN_BEVERAGE_RATE_LEVEL = 0; //kiek mažiausiai gali įpilti ir duoti žvaigždučių 
        public Calculations(){

        }

        public string BarNameAdaptation(string barName)
        {
            barName = barName.ToLower();
            barName = barName.Replace(" ", string.Empty).Replace("-", string.Empty).Replace(".", string.Empty);  // pasalinam visus tarpus, taškus ir -
            barName = char.ToUpper(barName[0]) + barName.Substring(1);

            return barName;
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
        /// <summary>
        /// Patikrina ar paduoti tinkami kintamieji
        /// Ar rate mažiau lygu 5 ir daugiau nei 0
        /// Ar beverage mažiau lygu 10 ir daugiau nei 0 
        /// Ar baro vardas yra bent 1 simblos 
        public Boolean Validation(int rate, int beverage, string barName)
        {
            return !(rate > MAX_RATE || beverage > MAX_BEVERAGE_LEVEL || barName.Length < MIN_NAME_LENGHT 
                  || rate <= MIN_BEVERAGE_RATE_LEVEL || beverage <= MIN_BEVERAGE_RATE_LEVEL);
        }
    }
}
