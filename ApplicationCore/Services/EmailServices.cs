using Coravel.Mailer.Mail.Interfaces;
using Coravel.Queuing.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using Domain.Entities;
using ApplicationCore.CoravelMailing;
using ApplicationCore.CoravelMailing.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

        public async void TrySendInvoice(string email, string orderNumber, List<OrderItem> orderItems)
        {

            if (email is null)
            {
                return;
            }

            InvoiceModel model = new()
            {
                Recipient = email,
                OrderNumber = orderNumber,
                OrderItems = orderItems,
            };

            // Queues in memory
            _queue.QueueAsyncTask(async () =>
            {
                try
                {
                    await _mailer.SendAsync(new InvoiceMailable(model));
                }
                catch (Exception ex)
                {

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

                        var pendingMail = new PendingInvoiceMail(email, orderNumber)
                        {
                            OrderNumber = orderNumber,
                            OrderItems = await db.OrderItems.Where(x => x.Order.OrderNumber == orderNumber).ToListAsync()
                        };

                        // Track orderitems and set to unchanged because otherwise the context can't save the pendingmail changes.
                        db.AttachRange(pendingMail.OrderItems);

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
