using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Customer
    {
        public string first { get; }
        public string last { get; }
        public string address { get; }
        public string city { get; }
        public string state { get; }
        public string zip { get; }
        public string phone { get; }
        public string email { get; }
        public string fullName { get; } // Variable stores first and last name for comboBox display.
        public Customer(string first, string last, string address, string city, string state, string zip, string phone, string email)
        {
            this.first = first;
            this.last = last;
            this.address = address;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.phone = phone;
            this.email = email;
            fullName = $"{first} {last}";
        }

    }
}
