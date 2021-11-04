using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class EventModel
    {
        public int ID { get; set; }
        public string Name_Event { get; set; }
        public string Description { get; set; }
        public int Percent { get; set; }
        public int Max { get; set; }
        public string Code { get; set; }
        public DateTime? Daystart { get; set; }
        public DateTime? Dayend { get; set; }
        public long? Status { get; set; }
    }
}
