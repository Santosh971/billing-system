using Application.Queries.ProductQueries;
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
    public class GetProductByProductIdQueryHandler : IRequestHandler<GetProductByProductIdQuery, ApiResponse<ProductResponse>>
    {

        private readonly AppDbContext context;
        public GetProductByProductIdQueryHandler(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<ApiResponse<ProductResponse>> Handle(GetProductByProductIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                List<ProductResponse> lstProductResponse = new List<ProductResponse>();

                var product = await context.Products.Where(p => p.ProductId == request.ProductId).FirstOrDefaultAsync();

                if (product == null)
                {
                    return new ApiResponse<ProductResponse>(null, System.Net.HttpStatusCode.NotFound, "Product Not Found", 1);
                }

                var productResponse = Mappers.ProductMapper.MapToProductResponse(product);

                lstProductResponse.Add(productResponse);

                return new ApiResponse<ProductResponse>(lstProductResponse, System.Net.HttpStatusCode.OK, "Product Found  Successfully", 0);


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
