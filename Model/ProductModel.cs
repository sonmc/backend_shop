using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ProductModel
    {
        public int ID { get; set; }
        public string ID_Category { get; set; }
        public string Name_Category { get; set; }
        public string Name_Product { get; set; }
        public string Images { get; set; }
        public double? Listed_Price { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int Evaluate { get; set; }
        public int Views { get; set; }
        public string IDCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Meta_Title { get; set; }
        public long? Status { get; set; }
        public long TotalItems { get; set; }
    }
}
