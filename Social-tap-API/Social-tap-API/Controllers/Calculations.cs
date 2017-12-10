using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Social_tap_API;
using Social_Tap_Api;

namespace SocialtapAPI
{
    public class Calculations:ICalculations
    {
        private static Dictionary<string, List<int>> _barRates = new Dictionary<string, List<int>>();
        private List<string> _hashTags = new List<string>();
        static int _uses;
        static int _sum;
        public double _max;
        public static string _bestbar;
        public const int MAX_BEVERAGE_LEVEL = 10; //kiek daugiausiai gali ipilti
        public const int MAX_RATE = 5; // kiek daugiausiai gali duoti žvaigždučių 
        public const int MIN_NAME_LENGHT = 1; //trumpiausias įmanomas baro pavadinimas
        public const int MIN_BEVERAGE_RATE_LEVEL = 0; //kiek mažiausiai gali įpilti ir duoti žvaigždučių 

        public static Dictionary<string, IBarData> _barData = new Dictionary<string, IBarData>();
        public IBar Bars = new Bar();
        public IReview Reviews =new Review(); 

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

           /* old way
            if (_barRates.Keys.Contains(barName))
            {
                _barRates[barName].Add(rate);
            }
            else
            {
                _barRates[barName] = new List<int> { rate };
            }
            return _barRates[barName].Average(); */ 
            using(var db=new DatabaseContext())
            {
                var x = (from review in db.ReviewSet
                         where review.Bar.Name == barName
                         group review by new
                         {
                             review.Bar.Name,
                             review.Beverage
                         }
                        into temp
                         select new
                         {
                             Average = temp.Average(avg => avg.Beverage),
                             temp.Key.Beverage,
                             temp.Key.Name,
                         }).Single();
                return x.Average;
            }
        }

        /// Į metodą paduodama informacija kiek buvo įpilta 
        /// Web service saugoma informacija apie tai kiek kartų buvo pasinaudota programa 
        /// Grąžiname true jeigu įpilta geriau, false jeigu blogau. 
        /// Lyginimas vyksta ne su vieno baro statistika o su BENDRA 
        /// http://localhost:.../api/values/bevlvl/INT
       // [HttpPost("bevlvl/{beverageLevel}")]    
        public Boolean Average(int beverageLevel)
        {
            // old way
           /* _uses++;
            _sum += beverageLevel;
            // sakysime, kad jeigu lygus vidurkiui, 
            // tai ipilta geriau
            return _sum / _uses <= beverageLevel;*/
            using (var db =new DatabaseContext())
            {
               return db.ReviewSet.
                    Average(avg => avg.Beverage) <= beverageLevel;
            } 
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
        public bool Validation(int rate, int beverage, string barName)
        {
            return !(rate > MAX_RATE || beverage > MAX_BEVERAGE_LEVEL || barName.Length < MIN_NAME_LENGHT 
                  || rate < MIN_BEVERAGE_RATE_LEVEL || beverage < MIN_BEVERAGE_RATE_LEVEL);
        }
        /// Baro pavadinimui
        /// Priskiria reikiamas reiksmes
        /// Grąžina Dictionary
        public Dictionary<string, IBarData> AddBarInfo(string barName, int beverage, int rate, string comment) 
        {
            using (var db = new DatabaseContext())
            {
                if(IsBarNew(barName))
                {
                    db.BarSet.Add(new Bar(barName));
                    db.SaveChanges();
                }
            }
              
            // _barData[barName].BarUses++;
            //_barData[barName].BeverageSum += beverage;
            //_barData[barName].Comparison = Average(beverage);
            //_barData[barName].Tags.AddRange(HashtagsFinder(comment));
            // _barData[barName].RateAvg = BarRateAverage(barName, rate);
            // _barData[barName].BeverageAvg = _barData[barName].BeverageSum / _barData[barName].BarUses;
             
            return _barData;
        }

        public Boolean AddBar(string barName)
        {
            using (var db = new DatabaseContext())
            {
                if (IsBarNew(barName))
                {
                    db.BarSet.Add(new Bar(barName));
                    db.SaveChanges();
                    return true;
                }
            }
            return false;   
        }
        /// Patikrina ar naujas baras
        /// Jeigu naujas
        /// Sukuria naują elementą Dictionary
        public bool IsBarNew(string barName)
        {
            using (var db = new DatabaseContext())
            {
                if (db.BarSet
                    .Where(bar => bar.Name.Contains(barName))
                    .Count() == 0)
                {
                    return true; 
                }
            }
            return false;
            // old way 
            /*  if (!_barData.Keys.Contains(barName))
              {
                  _barData.Add(barName, new BarData());
                  return false;
              } */
        }

        public string BestBarRate()
        {    
           //old way
           /* foreach (string barName in _barData.Keys)
            {
                
                if (_barData[barName].RateAvg > _max)
                {
                    _max = _barData[barName].RateAvg;
                    _bestbar = barName;
                }
            }*/
            using (var db = new DatabaseContext())
            {
                var x = (from review in db.ReviewSet
                         group review by new
                         {
                             review.Bar.Name,
                             review.Rate
                         }
                         into temp
                         select new
                         {
                             Max =temp.Max(maximum => temp.Average(avg=>avg.Rate)),
                             temp.Key.Rate,
                             temp.Key.Name
                         }).Single();
                return x.Name;
            }
        }

        public Statistics Stats()
        {
            string bestBar = BestBarRate();
           /* string statsInfo = "Baro pavadinimas " + bestBar + " Jo žvaigždučių vidurkis" + _barData[bestBar].RateAvg +
                "Įpylimo įvertinimo vidurkis " + _barData[bestBar].BeverageAvg + " Visų barų įpylimo vidurkis " + AllBarsAverage()
                + "Programele pasinaudota " + _barData.Count + " baruose." + "Iš viso panaudojimų: " + _uses; */
          
            var stats = new Statistics
            {
                TopBarName = bestBar,
                TopBarRate = _barData[bestBar].RateAvg,
                TopBarAvgBeverageVolume = _barData[bestBar].BeverageAvg,
                TotalAvgBeverageVolume = (double)_sum / _uses,
                BarCount = _barData.Count,
                ReviewCount = _uses
            };

            return stats;
        }

        public Boolean AddReview(string barName,int rate, int beverage)
        {
            using (var db=new DatabaseContext())
            {
                if (Validation(rate, beverage, barName))
                {
                    db.ReviewSet.Add(new Review(rate, barName, beverage));
                    db.SaveChanges();
                    return true;
                }             
            }
            return false;
        }
    }
}
