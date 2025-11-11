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
    public class DeleteProductByProductIdCommandHandler : IRequestHandler<DeleteProductByProductIdCommand, ApiResponse<ProductResponse>>
    {
        private readonly AppDbContext context;
        public DeleteProductByProductIdCommandHandler(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<ApiResponse<ProductResponse>> Handle(DeleteProductByProductIdCommand request, CancellationToken cancellationToken)
        {
            try
            {

                List<ProductResponse> lstProductResponse = new List<ProductResponse>();

                var product = await context.Products.Where(p => p.ProductId == request.ProductId).FirstOrDefaultAsync();

                if (product == null)
                {
                    return new ApiResponse<ProductResponse>(null, System.Net.HttpStatusCode.NotFound, "Product Not Found", 1);
                }


               context.Products.Remove(product);
               await context.SaveChangesAsync();

               var productResponse = Mappers.ProductMapper.MapToProductResponse(product);
 
               lstProductResponse.Add(productResponse);

               return new ApiResponse<ProductResponse>(lstProductResponse, System.Net.HttpStatusCode.OK, "Product Delete  Successfully", 0);


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
