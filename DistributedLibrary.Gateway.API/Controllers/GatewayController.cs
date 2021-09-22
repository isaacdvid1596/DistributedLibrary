using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistributedLibrary.Gateway.API.Models;
using RabbitMQ.Client;

namespace DistributedLibrary.Gateway.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class GatewayController : ControllerBase
    {
        [HttpGet("{bookId}")]
        public async Task <ActionResult<string>> GetBook(string bookId)
        {
            Guid title = Guid.NewGuid();
            string fileName = $@"C:\Users\isaac\Documents\{title}.txt";
            
            //crear archivo txt
            using (FileStream fs = System.IO.File.Create(fileName))
            {
                // Add some text to file    
                byte[] contents = new UTF8Encoding(true).GetBytes(fileName);
                fs.Write(contents, 0, contents.Length);
            }

            try
            {
                var book = bookId;

                var factory = new ConnectionFactory
                {
                    HostName = "localhost",
                    Port = 5672
                };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare("book-queue", false, false, false, null);
                        var body = Encoding.UTF8.GetBytes(book);
                        channel.BasicPublish(string.Empty, "book-queue", null, body);
                    }
                    /*escribir contenidos de autor y libro en text file*/
                }
                //returns file system location on get request
                return Ok(fileName);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
