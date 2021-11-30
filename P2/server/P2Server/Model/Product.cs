using System;
using System.ComponentModel.DataAnnotations;
using P2Server.Misc;

namespace P2Server.Model
{
    public class Product
    {
        // Need to be implemented
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

    }
}
