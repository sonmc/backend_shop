using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class AdvertisementModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public string Note { get; set; }
        public DateTime? Daystart { get; set; }
        public DateTime? Dayend { get; set; }
        public DateTime? CteateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? Status { get; set; }
    }
}
