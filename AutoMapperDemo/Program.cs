using AutoMapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapperDemo
{
    class Program
    {
        static void Main(string[] args)
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
