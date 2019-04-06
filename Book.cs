using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Book
    {
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Price { get; set; }
        public string Title { get; set; }

        /// <summary>
        /// Default constructor, initializes meaningless strings to 
        /// populate the fields.
        /// </summary>
        public Book()
        {
            Author = "John Doe";
            ISBN = "000";
            Price = "$0.00";
            Title = "NoTitle";
        }

        /// <summary>
        /// Overloaded Constructor, initializes author, ISBN, price, and title
        /// with the passed arguments.
        /// </summary>
        /// <param name="Author"></param>
        /// <param name="ISBN"></param>
        /// <param name="Price"></param>
        /// <param name="Title"></param>
        public Book(string Author, string ISBN, string Price, string Title)
        {
            this.Author = Author;
            this.ISBN = ISBN;
            this.Price = Price;
            this.Title = Title;
        }


    }
}
