using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CVGS.Areas.MVC.Models
{
    public partial class Event
    {
        public void @event()
        {
            EventData = new HashSet<@event>();
        }

        public int eventId { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string createdBy { get; set; }
        public string description { get; set; }

        public ICollection<@event> EventData { get; set; }
    }
}