using ApplicationCore.CoravelMailing;
using ApplicationCore.CoravelMailing.Models;
using Coravel.Mailer.Mail.Interfaces;
using Coravel.Queuing.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.HostedServices
{
    public class EmailHostedService : IHostedService, IDisposable
    {
        public readonly IMailer _mailer;
        private readonly IQueue _queue;
        private Task? InvoiceMailTask;
        private readonly CancellationTokenSource _cts = new();
        private readonly PeriodicTimer _timer = new(TimeSpan.FromMinutes(20));
        private readonly IServiceScopeFactory _scopeFactory;
        public EmailHostedService(IServiceScopeFactory scopeFactory, IMailer mailer, IQueue queue)
        {
            _scopeFactory = scopeFactory;
            _mailer = mailer;
            _queue = queue;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            InvoiceMailTask = SendPendingInvoiceMail();
        }

        public async Task SendPendingInvoiceMail()
        {
            while (await _timer.WaitForNextTickAsync(_cts.Token))
            {
                Console.WriteLine("Trying to send pending mail...");


                // Need to create a new scope because AddDbContext registers ApplicationDbContext as scoped by defualt
                // Which cannot be resolved in a IHostedService which is a singleton
                _queue.QueueAsyncTask(async () =>
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                        var emailSvc = scope.ServiceProvider.GetService<EmailServices>();

                        if (!db.PendingInvoiceMails.Any())
                        {
                            Console.WriteLine("There are no messages pending");

                            return;
                        }

                        var mailCount = db.PendingInvoiceMails.Count();

                        for (int i = 0; i < mailCount; i++)
                        {
                            var pendingMail = await db.PendingInvoiceMails.Include(x => x.OrderItems).Select(x => x).FirstAsync();
                            // Map pendingMail to InvoiceModel
                            var invoice = InvoiceModel.FromEntity(pendingMail);

                            try
                            {
                                await _mailer.SendAsync(new InvoiceMailable(invoice));

                                db.PendingInvoiceMails.Remove(pendingMail!);
                                db.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                // Temporary
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                });
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // In place to prevent the method from trying to stop a task that doesn't exist
            if (InvoiceMailTask is null)
            {
                return;
            }

            _cts.Cancel();
            _cts.Dispose();

            Console.WriteLine("Task was terminated.");
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
