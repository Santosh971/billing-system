using Domain.DTOs.ProductDTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class ProductMapper
    {

        public static ProductResponse MapToProductResponse(Product product)
        {
                ProductResponse productResponse = new ProductResponse();    

                productResponse.ProductId = product.ProductId;
                productResponse.ProductName = product.ProductName;
                productResponse.Price = product.Price;
                productResponse.Description = product.Description;
                productResponse.StockQuantity = product.StockQuantity;
         
                return productResponse;
        }


        public static List<ProductResponse> MapToProductResponseList(List<Product> products)
        {
            List<ProductResponse> productResponses = new List<ProductResponse>();

            foreach (var product in products)
            {
                ProductResponse productResponse = new ProductResponse();

                productResponse.ProductId = product.ProductId;
                productResponse.ProductName = product.ProductName;
                productResponse.Price = product.Price;
                productResponse.Description = product.Description;
                productResponse.StockQuantity = product.StockQuantity;

                productResponses.Add(productResponse);

            }
        
        return productResponses;    
        
        }
    }
}
