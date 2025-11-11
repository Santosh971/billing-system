using Application.Commands.ProductCommands;
using Application.Utility;
using Domain.DTOs.ProductDTOs;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponse<ProductResponse>>
    {

        private readonly AppDbContext context;
        public UpdateProductCommandHandler(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<ApiResponse<ProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {

                List<ProductResponse> lstProductResponse = new List<ProductResponse>();

                var product = await context.Products.Where(p=>p.ProductId == request.ProductRequest.ProductId).FirstOrDefaultAsync();

                if (product == null)
                {
                    return new ApiResponse<ProductResponse>(null, System.Net.HttpStatusCode.NotFound, "Product Not Found", 1);
                }

                product.ProductName = request?.ProductRequest?.ProductName ?? product.ProductName;
                product.Price = request?.ProductRequest?.Price ?? product.Price;
                product.StockQuantity = request?.ProductRequest.StockQuantity ?? product.StockQuantity;
                product.Description = request?.ProductRequest?.Description ?? product.Description;      

                await context.SaveChangesAsync();

                var productResponse = Mappers.ProductMapper.MapToProductResponse(product);

                lstProductResponse.Add(productResponse);

                return new ApiResponse<ProductResponse>(lstProductResponse, System.Net.HttpStatusCode.OK, "Product Update  Successfully", 0);



            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
