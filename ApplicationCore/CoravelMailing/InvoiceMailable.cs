using ApplicationCore.CoravelMailing.Models;
using Coravel.Mailer.Mail;
using Domain.Entities;

namespace ApplicationCore.CoravelMailing
{
    public class InvoiceMailable : Mailable<InvoiceModel>
    {
        private InvoiceModel invoice;
        public InvoiceMailable(InvoiceModel _invoice) => invoice = _invoice;
        

        public override void Build()
        {
            var htmlString = GenerateHtmlInvoice();


              To(invoice.Recipient)
             .From("gamifywebshop@gmail.com")
             .Html(htmlString);
        }

        public string GenerateHtmlInvoice()
        {
            var totalOrderPrice = invoice.TotalOrderPrice();

            var test = "<h1 style=\"text-align:center\">Invoice for your order: </h1>";

            for (int i = 0; i < invoice.Products.Count(); i++)
            {
                test += $"<div style=\"text-align:center>\"" +
                    $"<section class=\"productContainer\">\r\n" +
                    $"<img class=\"productImage\" src=\"{invoice.Products[i].PictureUrl}\" height=\"113\" width=\"113\" border=\"0\" />\r\n" +
                    $"<p class=\"productDisplay\">" +
                    $"<b>{invoice.Products[i].ProductName}</b> <br>" +
                    $"<small>{invoice.Products[i].ProductBrand}</small></p>" +
                $"</section>" +
                $"<p>x {invoice.Products[i].Quantity} x {invoice.Products[i].Price}</p>" +
                $"<div class=\"priceContainer\">\r\n" +
                    $"</div>\r\n" +
                    $"<hr />";
            }

            test += $"<p class=\"priceSumTotal\">Total: {totalOrderPrice} kr</p>\r\n";

            return test;
        }
    }
}
