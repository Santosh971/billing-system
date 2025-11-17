using Application.Commands.InvoiceCommands;
using Application.Commands.ProductCommands;
using Application.Utility;
using Domain.DTOs.InvoiceDTOs;
using Domain.DTOs.ProductDTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator mediator;

        private readonly IWebHostEnvironment env;

   
        public InvoiceController(IMediator _mediator, IWebHostEnvironment _env)
        {
            mediator = _mediator;
            env = _env;
        }



        //[HttpPost("GenerateInvoice")]
        //public async Task<ActionResult<ApiResponse<ProductResponse>>> GenerateInvoice([FromBody] InvoiceRequest invoiceRequest)
        //{
        //    var invoice = await mediator.Send(new AddInvoiceCommand(invoiceRequest));

        //    return Ok(invoice);
        //}



        //[HttpPost("GenerateInvoice")]
        //public async Task<IActionResult> GenerateInvoice([FromBody] InvoiceRequest invoiceRequest)
        //{
        //    var invoice = await mediator.Send(new AddInvoiceCommand(invoiceRequest));
        //    var invoiceResponse = invoice?.Data?.FirstOrDefault();
        //    // Generate PDF
        //    var pdfGenerator = new InvoiceToPdf();
        //    var pdfBytes = pdfGenerator.Generate(invoiceResponse);

        //    // Automatically trigger browser download
        //    var fileName = $"Invoice_{invoiceResponse.InvoiceId}.pdf";
        //    return File(pdfBytes, "application/pdf", fileName);

        //}


        [HttpPost("GenerateInvoice")]
        public async Task<IActionResult> GenerateInvoice([FromBody] InvoiceRequest invoiceRequest)
        {

              var invoice = await mediator.Send(new AddInvoiceCommand(invoiceRequest));
              var invoiceResponse = invoice?.Data?.FirstOrDefault();

            List<InvoiceItems> items = new List<InvoiceItems>();
            foreach (var item in invoiceResponse.InvoiceItemResponses)
            {
                InvoiceItems invoiceItem = new InvoiceItems();
                invoiceItem.Description = item.ProductResponse.ProductName;
                invoiceItem.Quantity = item.Quantity;
                invoiceItem.UnitPrice = item.UnitPrice;

                items.Add(invoiceItem);
            }

            //Dummy invoice model for testing
            var model = new InvoiceModel
            {
                InvoiceNumber = invoiceResponse.InvoiceId.ToString(),
                CustomerNumber = invoiceResponse.UserResponse.UserId.ToString(),
                InvoiceDate = DateTime.Parse(invoiceResponse.CreatedAt.ToString()),
                PaymentMode = invoiceResponse.PaymentMode,
                CustomerName = invoiceResponse.UserResponse.UserName,
                CustomerAddress =invoiceResponse.UserResponse.Address,
                CompanyName = "Company",
                CompanyAddress = "Address1, Zip City",
                CompanyEmail = "info@myweb",
                CompanyWebsite = "http://www.myweb",
              
                Items = items,
              
            };

            string logoPath = Path.Combine(env.WebRootPath, "images", "MindTapLogo2.png");
            // Generate PDF
            var pdfBytes = new InvoiceToPdf().Generate(model,logoPath);

            // Return as File content
            return File(pdfBytes, "application/pdf", $"Invoice_{invoiceResponse.InvoiceId.ToString()}.pdf");
        }

    }
}
