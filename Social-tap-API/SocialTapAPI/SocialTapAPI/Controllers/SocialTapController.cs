using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace SocialTapAPI.Controllers
{
    public class SocialTapController : ApiController
    {
        public static List<string> HashTags = new List<string>();

        static int sum;
        static int uses;
        public string Get()
        {
            return "Welcome To Social Tap API";
        }


        public string Get(int Id)
        {
            uses++;
            sum += Id;
            if (sum / uses <= Id)
            {
                return "Geriau įpylė";
            }
            else
            {
                return "Blogiau įyplė";
            }
        }
    }
}
