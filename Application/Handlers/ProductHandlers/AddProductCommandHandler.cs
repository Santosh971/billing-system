using Application.Commands.ProductCommands;
using Application.Utility;
using Domain.DTOs.ProductDTOs;
using Domain.Models;
using Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductHandlers
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ApiResponse<ProductResponse>>
    {
        private readonly AppDbContext context;
        public AddProductCommandHandler(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<ApiResponse<ProductResponse>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<ProductResponse> lstProductResponse = new List<ProductResponse>();

                Product product = new Product();

                product.ProductName = request.ProductRequest.ProductName;
                product.Description = request.ProductRequest.Description;
                product.Price = request.ProductRequest.Price;
                product.StockQuantity = request.ProductRequest.StockQuantity;

                context.Products.Add(product);
                await context.SaveChangesAsync();
                
                var productResponse = Mappers.ProductMapper.MapToProductResponse(product);

                lstProductResponse.Add(productResponse);

                return new ApiResponse<ProductResponse>(lstProductResponse, System.Net.HttpStatusCode.Created, "Product Add Successfully", 0);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
