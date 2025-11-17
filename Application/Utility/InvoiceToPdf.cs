
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Application.Utility
{
    public class InvoiceItems
    {
        public string Description { get; set; }
        public int? Quantity { get; set; }        
        public decimal? UnitPrice { get; set; }
        public decimal? Amount => Quantity * UnitPrice;
    }

    public class InvoiceModel
    {
        public string InvoiceNumber { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string PaymentMode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyWebsite { get; set; }
        
        public List<InvoiceItems> Items { get; set; } = new();
    }

    public class InvoiceToPdf
    {


        public byte[] Generate(InvoiceModel model, string logoPath)
        {
            byte[] qrBytes = GenerateDummyQrCode();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    // HEADER
                    page.Header().Column(col =>
                    {
                        
                        col.Item().Row(row =>
                        {
                            // Left: Date
                            row.RelativeColumn().Column(c =>
                            {
                                c.Item()
                                 .AlignMiddle()
                                 .PaddingTop(5)
                                 .Text($"Date: {model.InvoiceDate:dd.MM.yyyy}");
                            });

                            // Center: Title
                            row.RelativeColumn().Column(c =>
                            {
                                c.Item()
                                 .AlignCenter()
                                 .AlignMiddle()
                                 .Text("Invice")
                                 .FontSize(17)
                                 .FontColor(Colors.Blue.Medium)
                                 .Bold();
                            });

                            // Right: Logo
                            row.RelativeColumn().AlignRight().Column(c =>
                            {
                                c.Item()
                                 .Width(65)
                                 .Height(65)
                                 .Padding(0)
                                 .Border(0)
                                 .PaddingBottom(35)
                                 .Background(Colors.Transparent)
                                 .AlignMiddle()
                                 .Image(logoPath)
                                 .FitArea();   // ensures correct scaling
                            });
                        });



                   

                        col.Item().PaddingTop(15).Row(row =>
                        {
                            row.RelativeColumn().Column(c =>
                            {
                                c.Item().Text($"Invoice: {model.InvoiceNumber}");
                                c.Item().Text($"Customer No: {model.CustomerNumber}");
                                c.Item().Text($"Payment: {model.PaymentMode}");
                                c.Item().Text($"Page: 1");
                            });



                            row.RelativeColumn().AlignRight().Column(c =>
                            {
                                c.Item().Text(model.CompanyName).Bold();
                                c.Item().Text("QuickBill Systems");
                                c.Item().Text(model.CompanyAddress);
                                c.Item().Text($"{model.CompanyEmail}, {model.CompanyWebsite}");

                            });

                        });
                    });

                    // Totals Section
                    decimal? totalNet = 0;
                    foreach (var i in model.Items)
                        totalNet += i.Amount;

                    // CONTENT
                    page.Content().Column(col =>
                    {
                        col.Item().PaddingTop(20).Text($"Product List")
                            .FontSize(16).Bold().FontColor(Colors.Blue.Medium);

                        // Table Header
                        col.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3); // Description
                                columns.RelativeColumn();   // Qty
                                columns.RelativeColumn();   // Unit Price
                                columns.RelativeColumn();   // Amount
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Product Name").FontColor(Colors.White).Bold();
                                header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Quantity").FontColor(Colors.White).Bold();
                                header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Unit Price").FontColor(Colors.White).Bold();
                                header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Amount").FontColor(Colors.White).Bold();
                            });

                            foreach (var item in model.Items)
                            {
                                table.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(item.Description);
                                table.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(item.Quantity.ToString());
                                table.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(5).Text($"{item.UnitPrice:F2}");
                                table.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(5).Text($"{item.Amount:F2}");
                            }

                            {

                                table.Cell().Padding(5).Text("Total Net");
                                table.Cell();
                                //table.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(item.Unit);
                                table.Cell();
                                table.Cell().Padding(5).Text($"{totalNet:F2}");

                            }
                        });

                       


                        // Dummy QR Section
                        col.Item().PaddingTop(30).Row(row =>
                        {


                            row.ConstantColumn(120).AlignCenter().Column(right =>
                            {
                                right.Item().Image(qrBytes, ImageScaling.FitArea);
                            });

                            row.RelativeColumn().Column(left =>
                            {
                             
                                row.RelativeColumn().AlignRight().Column(c =>
                                {
                                    c.Item().Text(model.CustomerName).Bold();
                                    c.Item().Text(model.CustomerAddress);
                                });
                            });

                        });
                    });

                    // FOOTER
                    page.Footer().AlignCenter().PaddingTop(10)
                        .Text("Powered By MindTap")
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                });
            });

            return document.GeneratePdf();
        }

        // Dummy QR generator
        private byte[] GenerateDummyQrCode()
        {
            using var gen = new QRCodeGenerator();
            using var data = gen.CreateQrCode("DUMMY-INVOICE-QR", QRCodeGenerator.ECCLevel.Q);
            using var qr = new QRCode(data);
            using var bmp = qr.GetGraphic(5, System.Drawing.Color.Black, System.Drawing.Color.White, true);
            using var ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
    }
}



