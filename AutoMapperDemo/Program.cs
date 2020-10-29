using System;
using System.Collections.Generic;

namespace AutoMapperDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<ProductEntity> productEntities = new List<ProductEntity>();
            var productEntity = new ProductEntity()
            {
                Name = "Product",
                Amount = DateTime.Now
            };

            productEntities.Add(productEntity);

            var productDTOList = ExtAutoMapper.GetInstance.MapList<ProductEntity, ProductDTO>(productEntities);

            var productDTO = ExtAutoMapper.GetInstance.Map<ProductEntity, ProductDTO>(productEntity);
        }
    }
}