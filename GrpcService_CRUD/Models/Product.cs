﻿namespace GrpcService_CRUD.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Describtion { get; set; }
        public Double? Price { get; set; }
    }
}
