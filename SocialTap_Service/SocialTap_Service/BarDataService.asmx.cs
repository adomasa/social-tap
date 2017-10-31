using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;

namespace SocialTap_Service
{
    /// <summary>
    /// Summary description for BarDataService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BarDataService : System.Web.Services.WebService
    {

        public static List<string> BarsNames = new List<string>(); //saugom barų pavadinimus čia 
        public static List<string> HashTags = new List<string>(); // saugome panaudotus hashtag'us


        static int uses; //kiek kartų iš viso buvo pasinaudota   
        static int sum; //visų įvertinimų suma 

        [WebMethod(Description = "Aplankytų barų sąrašas")]

        public int CountBars(string barName)  //keliuose skirtingose baruose yra apsilankyta 
        {
            barName = barName.ToUpper();

            if (BarsNames.Contains(barName) == false)
            {
                BarsNames.Add(barName);
            }

            uses++;
            return BarsNames.Count;
        }


        [WebMethod(Description = "Ar geriau įpylė?")]
        public Boolean Average(int barEvaluation)
        {
            sum += barEvaluation;
            if (barEvaluation > sum / uses)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [WebMethod(Description = "Randam hashtagus")]

        public List<string> HashtagsFinder(string comment)
        {
            var regex = new Regex(@"(?<=#)\w+");
            var matches = regex.Matches(comment);

            foreach (Match tag in matches)
            {
                HashTags.Add(tag.Value);
            }
            return HashTags;
        }
}
}
