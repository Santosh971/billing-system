using Application.Utility;
using Domain.DTOs.ProductDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ProductQueries
{
    public class GetAllProductsQuery : IRequest<ApiResponse<ProductResponse>>
    {

    }
}
