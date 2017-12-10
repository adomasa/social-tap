using System.Collections.Generic;
using SocialtapAPI;

namespace Social_Tap_Api
{
    public class Bar:IBar 
    {

        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Adress { get; set; }
        public List<Review> Reviews { get; set; }
        public int BarId { get;set; }

        public Bar(string barName)
        {
            Name = barName;
        }

        public Bar()
        {

        }
    }
}
