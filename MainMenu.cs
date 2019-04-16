using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Lab1
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            // Initialize the database, if it does not exist.
            string ConnectionString = "server=localhost;user=root;password=";
            MySqlConnection DBConnect = new MySqlConnection(ConnectionString);
            DBConnect.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "CREATE DATABASE if not exists `book store` ; ";

            cmd.Connection = DBConnect;
            cmd.ExecuteNonQuery();
            DBConnect.Close();

            // Create book table.
            ConnectionString = "server=localhost;user=root;database=book store;password=";
            DBConnect = new MySqlConnection(ConnectionString);
            DBConnect.Open();
            cmd.CommandText = "CREATE TABLE if not exists `books` ( " +
                              "`book_id` int(4) NOT NULL AUTO_INCREMENT, " +
                              "`title` varchar(50) NOT NULL, " +
                              "`author` varchar(30) NOT NULL, " +
                              "`isbn` varchar(20) NOT NULL, " +
                              "`price` varchar(10) NOT NULL, " +
                              "PRIMARY KEY(book_id), " +
                              "UNIQUE KEY isbn(isbn)" +
                              ") ENGINE = InnoDB DEFAULT CHARSET = utf8;";
            cmd.Connection = DBConnect;
            cmd.ExecuteNonQuery();

            // Add books to book table.
            cmd.CommandText = "INSERT ignore into `books` VALUES( " +
                               "'null', 'C# Coding', 'John Jack', " +
                               "'12312312312312', '$22.56');" +
                               "INSERT ignore into `books` VALUES( " +
                               "'null', 'Green Eggs and Ham', " +
                               "'Dr. Seuss', '1234567890', '$4.54');" +
                               "INSERT ignore into `books` VALUES(" +
                               "'null', 'How to get an A', " +
                               "'Shams', '0987654321', '$95.55');";
            cmd.Connection = DBConnect;
            cmd.ExecuteNonQuery();

            // Create Customer Table.
            cmd.CommandText = "CREATE TABLE if not exists `customer` ( " +
                              "`cust_id` int(4) NOT NULL AUTO_INCREMENT, " +
                              "`first` varchar(15) NOT NULL, " +
                              "`last` varchar(15) NOT NULL, " +
                              "`address` varchar(50) NOT NULL, " +
                              "`city` varchar(15) NOT NULL, " +
                              "`state` varchar(2) NOT NULL, " +
                              "`zip` varchar(5) NOT NULL, " +
                              "`phone` varchar(10) NOT NULL, " +
                              "`email` varchar(30) NOT NULL, " +
                              "PRIMARY KEY(cust_id), " +
                              "UNIQUE KEY email(email)" +
                              ") ENGINE = InnoDB DEFAULT CHARSET = utf8;";
            cmd.Connection = DBConnect;
            cmd.ExecuteNonQuery();

            // Create Orders Table
            cmd.CommandText = "CREATE TABLE if not exists `orders` ( " +
                              "`order_id` int(7) NOT NULL AUTO_INCREMENT, " +
                              "`cust_id` int(4) NOT NULL, " +
                              "`sub-total` decimal(12,2) NOT NULL, " +
                              "`tax` decimal(10,2) NOT NULL, " +
                              "`total` decimal(12,2) NOT NULL, " +
                              "`order_date` date NOT NULL, " +
                              "PRIMARY KEY(order_id), " +
                              "FOREIGN KEY(cust_id) REFERENCES customer(cust_id)" +
                              ") ENGINE = InnoDB DEFAULT CHARSET = utf8;";
            cmd.Connection = DBConnect;
            cmd.ExecuteNonQuery();

            // Create Order details Table
            cmd.CommandText = "CREATE TABLE if not exists `order_details` ( " +
                              "`order_id` int(7) NOT NULL, " +
                              "`book_id` int(7) NOT NULL, " +
                              "`quantity_of_books` int(4) NOT NULL, " +
                              "`line_total` decimal(12,2) NOT NULL, " +
                              "FOREIGN KEY(order_id) REFERENCES orders(order_id), " +
                              "FOREIGN KEY(book_id) REFERENCES books(book_id)" +
                              ") ENGINE = InnoDB DEFAULT CHARSET = utf8;";
            cmd.Connection = DBConnect;
            cmd.ExecuteNonQuery();
            DBConnect.Close();
        }

        private void CustomerButton_Click(object sender, EventArgs e)
        {
            CustomerForm form = new CustomerForm();
            this.Hide();
            form.Show();
        }

        private void BookButton_Click(object sender, EventArgs e)
        {
            BookEditForm form = new BookEditForm();
            this.Hide();
            form.Show();
        }

        private void OrderButton_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.Show();
        }
    }
}
