using Domain.DTOs.InvoiceDTOs;
using Domain.DTOs.InvoiceItemDTOs;
using Domain.Models;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class InvoiceMapper
    {
       
        public static InvoiceResponse MapToInvoiceResponse(Invoice invoice, List<InvoiceItem>items)
        {
            InvoiceResponse invoiceResponse = new InvoiceResponse();

            invoiceResponse.InvoiceId = invoice.InvoiceId;  
            invoiceResponse.UserId = invoice.UserId;
            invoiceResponse.CreatedAt = invoice.CreatedAt;
            invoiceResponse.TotalAmount = invoice.TotalAmount;
            invoiceResponse.PaymentMode = invoice.PaymentMode;
            
            if(items != null)
            {
               invoiceResponse.InvoiceItemResponses = Mappers.InvoiceItemMapper.MapToInvoiceItemResponseList(items);
            }

            return invoiceResponse;
        }
    }
}
