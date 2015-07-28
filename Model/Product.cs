using System.Collections.Generic;

namespace Model
{
    internal class Product
    {
        public Product()
        {
            this.Features=new List<Feature>();
        }

        public string Name { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public List<Feature> Features { get; set;}
        public decimal Version { get; set; }
    }
}
