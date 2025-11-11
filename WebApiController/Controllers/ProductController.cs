using Application.Commands.ProductCommands;
using Application.Commands.UserCommands;
using Application.Queries.ProductQueries;
using Application.Queries.UserQueries;
using Application.Utility;
using Domain.DTOs.ProductDTOs;
using Domain.DTOs.UserDTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IMediator mediator;

        public ProductController(IMediator _mediator)
        {
            mediator = _mediator;
        }


        [HttpPost("AddProduct")]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> AddProduct([FromBody] ProductRequest productRequest)
        {
            var product = await mediator.Send(new AddProductCommand(productRequest));

            return Ok(product);
        }


        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> UpdateProduct([FromBody] ProductRequest productRequest)
        {
            var product = await mediator.Send(new UpdateProductCommand(productRequest));

            return Ok(product);
        }



        [HttpGet("GetProductById/{productId}")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> GetProductById(int productId)
        {
            var product = await mediator.Send(new GetProductByProductIdQuery(productId));

            return Ok(product);
        }



        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> GetAllProducts()
        {
            var products = await mediator.Send(new GetAllProductsQuery());

            return Ok(products);
        }




        [HttpDelete("DeleteProductById/{productId}")]
        public async Task<ActionResult<ApiResponse<UserResponse>>> DeleteProductById(int productId)
        {
            var product  = await mediator.Send(new DeleteProductByProductIdCommand(productId));

            return Ok(product);
        }

    }
}
