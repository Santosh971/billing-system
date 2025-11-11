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
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ApiResponse<ProductResponse>>
    {
        private readonly AppDbContext context;
        public GetAllProductsQueryHandler(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<ApiResponse<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await context.Products.ToListAsync();

                if (products == null || products.Count == 0)
                {
                    return new ApiResponse<ProductResponse>(null, System.Net.HttpStatusCode.NotFound, "Product List is Empty", 1);
                }

                var lstProductsResponse = Mappers.ProductMapper.MapToProductResponseList(products);
                return new ApiResponse<ProductResponse>(lstProductsResponse, System.Net.HttpStatusCode.OK, "Product List ", 0);


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
