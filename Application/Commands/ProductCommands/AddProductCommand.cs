using Application.Utility;
using Domain.DTOs.ProductDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ProductCommands
{
    public class AddProductCommand : IRequest<ApiResponse<ProductResponse>>
    {
        public ProductRequest ProductRequest { get; set; }

        public AddProductCommand(ProductRequest productRequest)
        {
            ProductRequest = productRequest;        
        } 
    }
}
