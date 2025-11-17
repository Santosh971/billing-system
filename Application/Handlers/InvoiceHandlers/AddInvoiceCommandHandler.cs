using Application.Commands.InvoiceCommands;
using Application.Utility;
using Domain.DTOs.InvoiceDTOs;
using Domain.Models;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.InvoiceHandlers
{
    public class AddInvoiceCommandHandler : IRequestHandler<AddInvoiceCommand, ApiResponse<InvoiceResponse>>
    {
        private readonly AppDbContext context;


        public AddInvoiceCommandHandler(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<ApiResponse<InvoiceResponse>> Handle(AddInvoiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<InvoiceResponse> lstInvoiceResponse = new List<InvoiceResponse>();

                var user = await context.Users.Where(u => u.UserId == request.InvoiceRequest.UserId).FirstOrDefaultAsync();
                if (user == null)
                {
                    return new ApiResponse<InvoiceResponse>(null, HttpStatusCode.BadRequest, "User Not Exist for Create Invoice", 1);
                }

             

                //Create Invoice

                Invoice invoice = new Invoice();

                invoice.UserId = request.InvoiceRequest.UserId;
                invoice.CreatedAt = DateTime.Now;                

                context.Invoices.Add(invoice);
                await context.SaveChangesAsync();

                decimal? totalAmount = 0;
                foreach (var invoiceItem in request.InvoiceRequest.InvoiceItems)
                {
                    var product = await context.Products.Where(p => p.ProductId == invoiceItem.ProductId).FirstOrDefaultAsync();

                    totalAmount = totalAmount + ( product?.Price * invoiceItem.Quantity);


                    //Add Invoice Item

                    InvoiceItem item = new InvoiceItem();

                    item.InvoiceId = invoice.InvoiceId;
                    item.ProductId = invoiceItem?.ProductId;
                    item.Quantity = invoiceItem?.Quantity;
                    item.UnitPrice = product?.Price;
                    item.SubTotal = invoiceItem?.Quantity * product?.Price;

                    context.invoiceItems.Add(item);

                    product.StockQuantity = product.StockQuantity - invoiceItem?.Quantity;  
                    await context.SaveChangesAsync();
                }

                
           
                    invoice.TotalAmount = totalAmount;
                    invoice.PaymentMode = "cash";

                    await context.SaveChangesAsync();


                //Return Invoice response


                var invoiceItems = await context.invoiceItems.Where(it => it.InvoiceId == invoice.InvoiceId).Include(it => it.Product).ToListAsync();

                var invoiceResponse = Mappers.InvoiceMapper.MapToInvoiceResponse(invoice, user, invoiceItems);

                lstInvoiceResponse.Add(invoiceResponse);


                return new ApiResponse<InvoiceResponse>(lstInvoiceResponse, HttpStatusCode.Created, "Item Purchase And Invoice Created", 0);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
