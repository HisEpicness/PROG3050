using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVGS.Models
{
    public class Event
    {
        public void eventData()
        {
            EventData = new HashSet<eventData>();
        }

        public int eventId { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string createdBy { get; set; }
        public string description { get; set; }

        public ICollection<eventData> EventData { get; set; }
    }
}