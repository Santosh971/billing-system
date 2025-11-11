using Application.Utility;
using Domain.DTOs.InvoiceDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.InvoiceCommands
{
    public class AddInvoiceCommand : IRequest<ApiResponse<InvoiceResponse>>
    {
        public InvoiceRequest InvoiceRequest { get; set; }

        public AddInvoiceCommand(InvoiceRequest invoiceRequest)
        {
            InvoiceRequest = invoiceRequest;
        }
    }
}
