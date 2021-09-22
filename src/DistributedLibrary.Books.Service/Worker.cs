using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.Text;
using DistributedLibrary.Gateway.API.Models;
using Newtonsoft.Json;

namespace DistributedLibrary.Books.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;


        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("book-queue", false, false, false, null);
            _consumer = new EventingBasicConsumer(_channel);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Received += async (model, content) =>
            {

                string json = "";
                List<BookDto> books = null;

                using (StreamReader j = new StreamReader("books.json"))
                {
                   json = j.ReadToEnd(); 
                   books = JsonConvert.DeserializeObject<List<BookDto>>(json);
                }

                //_channel.BasicPublish(string.Empty, "receive-payment-queue", null, books);

                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }
            };

            _channel.BasicConsume("book-queue", true, _consumer);

            return Task.CompletedTask;
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
