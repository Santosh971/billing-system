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
    public class DeleteProductByProductIdCommand : IRequest<ApiResponse<ProductResponse>>
    {
        public int ProductId {  get; set; }
        
        public DeleteProductByProductIdCommand(int productId)
        {
            ProductId = productId;  
        }

    }
}
