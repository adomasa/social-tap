﻿using Social_tap_API;
using Social_Tap_Api;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SocialtapAPI
{
    public class Calculations:ICalculations
    {
        private readonly List<string> _hashTags = new List<string>();
        private const int MaxBeverageLevel = 10; //kiek daugiausiai gali ipilti
        private const int MaxRate = 5; // kiek daugiausiai gali duoti žvaigždučių 
        private const int MinNameLenght = 1; //trumpiausias įmanomas baro pavadinimas
        private const int MinBeverageRateLevel = 0; //kiek mažiausiai gali įpilti ir duoti žvaigždučių 
        private const int Error = -1; // jei kažkas negerai
        private static Dictionary<string, IBarData> BarData = new Dictionary<string, IBarData>();
       
        // Nenaudojami kintamieji
        // private const int ForAverage = 1; // jei skaičiuoti baro įpilto alaus vidurkį
        // private const int ForRate = 2; // jei skaičiuoti baro žvaigždučių vidurkį
        // public static string Bestbar;

        public string BarNameAdaptation(string barName)
        {
            string[] forbiddenSymbols = {" ", ".", "-"};

            barName = barName.ToLower();
            // pasalinam visus tarpus, taškus ir -
            forbiddenSymbols.ToList().ForEach(i => barName = barName.Replace(i, string.Empty));
            // barName = barName.Replace(" ", string.Empty).Replace("-", string.Empty).Replace(".", string.Empty);  
            barName = char.ToUpper(barName[0]) + barName.Substring(1);

            return barName;
        }
        /// Į metodą paduodamas baro pavadinimas ir jo įvertinimas
        /// Web service laikome baro pavadinimą ir jo įvertinimų Listą 
        /// Atgal grąžiname tik baro įvertinimų vidurkį
        /// http://localhost:.../api/values/barrate/string_baropavadinimas/string_įvertinimas
        public double BarRateAverage(string barName)
        {
            using(var db=new DatabaseContext())
            {

                var avg = db.ReviewSet
                    .Where(y => y.Bar.Name == barName)
                    .Select(y => y.Rate)
                    .Average();
                return avg;
            }
        }

        /// Į metodą paduodama informacija kiek buvo įpilta 
        /// Web service saugoma informacija apie tai kiek kartų buvo pasinaudota programa 
        /// Grąžiname true jeigu įpilta geriau, false jeigu blogau. 
        /// Lyginimas vyksta ne su vieno baro statistika o su BENDRA 
        /// http://localhost:.../api/values/bevlvl/INT
       // [HttpPost("bevlvl/{beverageLevel}")]    
        public bool Average(int beverageLevel)
        {
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
            return !(rate > MaxRate || beverage > MaxBeverageLevel || barName.Length < MinNameLenght 
                  || rate < MinBeverageRateLevel || beverage < MinBeverageRateLevel);
        }
        /// Baro pavadinimui
        /// Priskiria reikiamas reiksmes
        /// Grąžina Dictionary
        public Dictionary<string, IBarData> AddBarInfo(string barName, int beverage, int rate, string comment) 
        {
            using (var db = new DatabaseContext())
            {
                if (!IsBarNew(barName)) return BarData;
                db.BarSet.Add(new Bar(barName));
                db.SaveChanges();
            }

            return BarData;
        }

        

        // Todo: apdoroti AddBar grąžinamą bool reikšmę 
        public bool AddBar(string barName)
        {
            using (var db = new DatabaseContext())
            {
                if (!IsBarNew(barName)) return false;
                db.BarSet.Add(new Bar(barName));
                db.SaveChanges();
                return true;
            }
        }
        /// Patikrina ar naujas baras
        /// Jeigu naujas
        /// Sukuria naują elementą Dictionary
        public bool IsBarNew(string barName)
        {
            using (var db = new DatabaseContext())
            {
                return !db.BarSet.Any(bar => bar.Name == barName);
            }
        }

        public string BestBarRate()
        {    
            using (var db = new DatabaseContext())
            {
                var best = (from bars in db.ReviewSet
                            group bars by new
                            {
                                bars.Rate,
                                bars.Bar.Name                             
                           }
                           into barsAvg
                           let average = barsAvg.Average(b => b.Rate)
                           orderby average descending
                           select new
                           {
                               bestname = barsAvg,
                               barsAvg.Key.Name,
                               average 
                           }).First();
                return best.Name;
            }
        }

        public IStatistics Stats()
        {
            var bestBar = BestBarRate();
            /* string statsInfo = "Baro pavadinimas " + bestBar + " Jo žvaigždučių vidurkis" + _barData[bestBar].RateAvg +
                 "Įpylimo įvertinimo vidurkis " + _barData[bestBar].BeverageAvg + " Visų barų įpylimo vidurkis " + AllBarsAverage()
                 + "Programele pasinaudota " + _barData.Count + " baruose." + "Iš viso panaudojimų: " + _uses; */
            using (var db = new DatabaseContext())
            {
                var stats = new Statistics
                {
                    TopBarName = bestBar,
                    TopBarRate = BarAverage(bestBar, 1),
                    TopBarAvgBeverageVolume = BarAverage(bestBar, 2),
                    TotalAvgBeverageVolume = db.ReviewSet.Average(avg => avg.Beverage),
                    BarCount = db.BarSet.Count(),
                    ReviewCount = db.ReviewSet.Count()
                };

                return stats;
            }
        }

        public bool AddReview(string barName, int rate, int beverage)
        {
            using (var db = new DatabaseContext())
            {
                db.ReviewSet.Add(new Review(rate, barName, beverage));
                db.SaveChanges();
                return true;
            }
        }

        public double BarAverage (string barName, int index)
        {
            using (var db = new DatabaseContext ())
            {
                IQueryable<Review> values;
                switch (index)
                {
                    case 1:
                        values = db.ReviewSet.Where(name => name.Bar.Name == barName);
                        if (!values.Any()) return 0;
                        return values.Average(avg => avg.Beverage);
                    case 2:
                        values = db.ReviewSet.Where(name => name.Bar.Name == barName);
                        if (!values.Any()) return 0;
                        return values.Average(avg => avg.Rate);
                }

                return Error;
            }
        }

        public Dictionary<string, IBarData> GetBarData()
        {
            using (var db = new DatabaseContext())
            {
                var data = new Dictionary<string, IBarData>();
                foreach(var bar in db.BarSet)
                {
                    if (data.Keys.Count(x => x == bar.Name) == 0 || !data.Keys.Contains(bar.Name))
                    {
                        // to prevent null values in db again
                        if (bar.Name == null) continue;

                        data.Add(bar.Name,
                        new BarData(BarAverage(bar.Name, 2),
                        BarAverage(bar.Name, 1),
                        db.ReviewSet
                        .Count(review => review.Bar.Name == bar.Name)));
                    }
                    else
                    {
                        data[bar.Name].BeverageAvg = BarAverage(bar.Name, 1);
                        data[bar.Name].RateAvg = BarAverage(bar.Name, 1);
                        data[bar.Name].BarUses= db.ReviewSet.Count(review => review.Bar.Name == bar.Name);
                    }
                }
                return data;
            }
        }
    }
}
