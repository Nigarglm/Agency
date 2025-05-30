﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Agency.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Client {  get; set; }
        public string Image {  get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
