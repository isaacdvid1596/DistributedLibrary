using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistributedLibrary.Gateway.API.Models
{
    public class BookDto
    {
        public string Isbn { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public DateTime PublishDate { get; set; }

        public string Publisher { get; set; }

        public int NumberOfPages { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public int AuthorId { get; set; }
    }
}
