using ApplicationCore.DTOs;
using Coravel.Mailer.Mail.Interfaces;
using Microsoft.Extensions.Logging;
using Coravel.Queuing.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using Domain.Entities;
using X.PagedList;
using ApplicationCore.CoravelMailing;
using ApplicationCore.CoravelMailing.Models;

namespace ApplicationCore.Services
{
    public class EmailServices
    {
        private readonly IMailer _mailer;
        private readonly IQueue _queue;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public EmailServices(IMailer mailer, IQueue queue, IServiceScopeFactory serviceScopeFactory)
        {
            _mailer = mailer;
            _queue = queue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void SendInvoice(string email, string orderNumber, List<ShoppingCartItem> cartItems)
        {
            if (email is null)
            {
                return;
            }

            InvoiceModel model = new InvoiceModel
            {
                Recipient = email,
                Products = cartItems.Select(x => new ShoppingCartItemDto(x)).ToList()
            };

            // Queues in memory
            _queue.QueueAsyncTask(async () =>
            {
                try
                {
                    await _mailer.SendAsync(new InvoiceMailable(model));
                }
                catch(Exception ex)
                {

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

                        var pendingMail = new PendingInvoiceMail(email, orderNumber)
                        {
                            InvoiceItems = cartItems
                        };

                        db!.PendingInvoiceMails.Add(pendingMail);

                        db.SaveChanges();
                    }

                    Console.WriteLine("Message failed. Mail is added to database to pending status.");
                    Console.WriteLine(ex.Message);

                }

            });

        }

    }
}
