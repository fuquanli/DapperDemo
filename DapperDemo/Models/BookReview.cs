using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemo.Models
{
    public class BookReview
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string Content { get; set; }

        public Book AssoicationWithBook { get; set; }

        public override string ToString()
        {
            return string.Format("{0})--[{1}]\t\"{2}\"", Id, BookId, Content);
        }
    }
}
