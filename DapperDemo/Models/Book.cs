using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemo.Models
{
    public class Book
    {
        public Book()
        {
            Reviews = new List<BookReview>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Press { get; set; }

        public List<BookReview> Reviews { get; set; }

        public override string ToString()
        {
            return string.Format("[Id] : {0} [Name] : {1} [Author] : {2} [Press] : {3}", Id, Name, Author, Press);
        }
    }
}
