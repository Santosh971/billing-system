using Domain.DTOs.InvoiceDTOs;
using Domain.DTOs.InvoiceItemDTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class InvoiceItemMapper
    {
        public static InvoiceItemResponse MapToInvoiceItemResponse(InvoiceItem invoiceItem)
        {
            InvoiceItemResponse invoiceItemResponse = new InvoiceItemResponse();

            invoiceItemResponse.InvoiceItemId = invoiceItem.InvoiceItemId;
            invoiceItemResponse.ProductId = invoiceItem.ProductId;
            invoiceItemResponse.Quantity = invoiceItem.Quantity;    
            invoiceItemResponse.UnitPrice = invoiceItem.UnitPrice;
            invoiceItemResponse.SubTotal = invoiceItem.SubTotal;

            return invoiceItemResponse;
        }



        public static List<InvoiceItemResponse> MapToInvoiceItemResponseList(List<InvoiceItem> invoiceItems)
        {
            List<InvoiceItemResponse> lstInvoiceItemResponses = new List<InvoiceItemResponse>();

            foreach(var invoiceItem in invoiceItems)
            {
                InvoiceItemResponse invoiceItemResponse = new InvoiceItemResponse();

                invoiceItemResponse.InvoiceItemId = invoiceItem.InvoiceItemId;
                invoiceItemResponse.ProductId = invoiceItem.ProductId;
                invoiceItemResponse.Quantity = invoiceItem.Quantity;
                invoiceItemResponse.UnitPrice = invoiceItem.UnitPrice;
                invoiceItemResponse.SubTotal = invoiceItem.SubTotal;

                lstInvoiceItemResponses.Add(invoiceItemResponse);
            }

            return lstInvoiceItemResponses;
        }
    }
}
