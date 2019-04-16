using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

/// <summary>
/// This program creates three Book objects, with basic information and pricing, and places them in a list for the user.
/// The user can pick and choose any combination of these three books to place an order. The program updates as necessary, calculating
/// totals, including tax, and the user can choose to confirm or cancel the order.
/// </summary>

namespace Lab1
{
    
    public partial class Form1 : Form
    {
        /*
        // List to hold books from "book.txt".
        private List<Book> books = new List<Book>();
        char delimiter = ',';
        */

        // The following two variables are used in the AddTitleButton_Click method to handle dataGridView bugs.
        private int currentRowIndex = 0;
        private bool firstTitleAdded = false;

        public Form1()
        {
            /*
            // Parse from text file and add to the book list.
            try
            {
                // Close reader and inFile even if exception is thrown.
                // SRC:https://stackoverflow.com/questions/86766/how-to-properly-handle-exceptions-when-performing-file-io 
                using (FileStream inFile = new FileStream("book.txt", FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(inFile))
                {
                    string bookIn;
                    string[] fields;
                    bookIn = reader.ReadLine();
                    while (bookIn != null)
                    {
                        fields = bookIn.Split(delimiter);
                        Book temp = new Book(fields[0], fields[1], fields[2], fields[3]);
                        books.Add(temp);
                        bookIn = reader.ReadLine();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File book.txt not found.", "File Read Error");
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
            */

            InitializeComponent();

            // Set default value for data grid title to avoid null exception in AddTitleButton_Click foreach loop.
            OrderSummaryData.Rows[0].Cells[0].Value = "No Selection Made";
        }

        /// <summary>
        /// The currently selected book in the ComboBox will be added to the order summary grid.
        /// If the selection already exists in the table, the quantity is updated and the line total recalculates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTitleButton_Click(object sender, EventArgs e)
        {
            // Check if user has selected a book in the dropDown menu. If not,
            // show message and set focus to the dropDown.
            if (BookSelectionBox.SelectedIndex == -1)
            {
                BookSelectionBox.Focus();
                MessageBox.Show("Select a Book from the DropDown Menu first.", "No Book Selected");
                return;
            }

            // Following block checks if value passed in quantity box is valid.
            // If not, display message and set focus to quantity box.
            int parse;
            if (int.TryParse(QuantityTextBox.Text, out parse) == false || parse == 0)
            {
                MessageBox.Show("Enter a valid quantity.", "Invalid Quantity");
                QuantityTextBox.Clear();
                QuantityTextBox.Focus();
                return;
            }

            string[] bookData = new string[4];
            string connectionString = "server=localhost;user=root;database=book store;password=";
            using (MySqlConnection DBConnect = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand($"select title, author, isbn, price from books where title='{BookSelectionBox.Text}'", DBConnect);
                // Open Connection and retrieve data using mysql reader
                // SRC: https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/retrieving-data-using-a-datareader
                DBConnect.Open();

                // Read book data into bookData array. 
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    bookData[0] = reader.GetString(0);
                    bookData[1] = reader.GetString(1);
                    bookData[2] = reader.GetString(2);
                    bookData[3] = reader.GetString(3);
                }
                DBConnect.Close();
            }

            Book tempBook = new Book(bookData[1], bookData[2], bookData[3], bookData[0]);     // Create a tempBook to alias the currently selected item in the comboBox.
            int currentQty = 0;         // Variable will track quantity of selected book already in the data grid.
            bool inTable = false;       // Variable tracks whether the selected book is already in the grid.

            // Foreach loop checks if entry already exists. Also updates global qty.
            foreach (DataGridViewRow row in OrderSummaryData.Rows)
            {
                // If entry already exists, update currentQty.
                if (row.Cells[0].Value.ToString().Contains(tempBook.Title))
                {
                    currentQty += Convert.ToInt32(row.Cells[2].Value);
                    currentRowIndex = row.Index;
                    inTable = true;
                }
            }


            // If entry didn't exist in table, create a row and set row index to the new row.
            if (!inTable)
            {
                // If this is a subsequent addition.
                if (firstTitleAdded)
                {
                    // Add a row and set to the new row's index - 1. I could not figure out why this was
                    // necessary but it was the only way I could get the form to work.
                    OrderSummaryData.Rows.Add();
                    currentRowIndex = OrderSummaryData.NewRowIndex - 1;
                }
                // If this is the first addition, set the bool and continue at first index.
                else
                {
                    firstTitleAdded = true;
                }
            }
            // Set the cells of the added row to the book's attributes.
            OrderSummaryData.Rows[currentRowIndex].Cells[0].Value = tempBook.Title;
            OrderSummaryData.Rows[currentRowIndex].Cells[1].Value = tempBook.Price;
            // Update the quantity with the previous qty, if any, plus the new user-input qty.
            OrderSummaryData.Rows[currentRowIndex].Cells[2].Value = Convert.ToInt32(QuantityTextBox.Text) + currentQty;

            // Extract the price, without the dollar sign, and multiply it with the quantity.
            double num1 = Convert.ToDouble(OrderSummaryData.Rows[currentRowIndex].Cells[2].Value);
            double num2 = Convert.ToDouble(tempBook.Price.Substring(1));
            double total = num1 * num2;
            OrderSummaryData.Rows[currentRowIndex].Cells[3].Value = total.ToString("C2");   // Add to 0.00 to keep 2 numbers after decimal point.

            // Calculate subtotal, tax, and total. Re-using num1 and num2 as subtotal and tax, respectively.
            num1 = 0; num2 = 0; total = 0;
            // Loop adds each line total and stores in num1 (subtotal).
            foreach (DataGridViewRow row in OrderSummaryData.Rows)
            {
                string temp = row.Cells[3].Value.ToString();   // First create a temp string with the line total string.
                num1 += Convert.ToDouble(temp.Substring(1));   // Extract the string without the '$' and store as subtotal.
            }

            num2 = 0.1 * num1;      // Calculate tax (10% of subtotal).
            total = num1 + num2;    // Total = tax + subtotal.

            // Set the textBoxes with subtotal, tax, total.
            SubtotalTextBox.Text = num1.ToString("C2");
            TaxTextBox.Text = num2.ToString("C2");
            TotalTextBox.Text = total.ToString("C2");
            CancelOrderButton.Enabled = true;       // Allow cancel order 
        }

        /// <summary>
        /// Clears the order box and submits order to order.txt file if user has books in the
        /// order box. Otherwise displays error message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmOrderButton_Click(object sender, EventArgs e)
        {
            if (TotalTextBox.Text.Equals("$0.00"))
            {
                MessageBox.Show("Add at least one book first.", "No Books Ordered");
                return;
            }
            else if (CustomerSelectBox.DropDownStyle == ComboBoxStyle.DropDown)
            {
                MessageBox.Show("Must select a customer.", "No Customer Selected");
                return;
            }

            // Else confirm order and write to "order.txt". Push to mySQL table orders and order_details after writing to text.
            MessageBox.Show("Order Confirmed!");
            firstTitleAdded = false;
            currentRowIndex = 0;
            try
            {
                using (FileStream outFile = new FileStream("orders.txt", FileMode.Append, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(outFile))
                {
                    foreach (DataGridViewRow row in OrderSummaryData.Rows)
                    {
                        string tempTitle = row.Cells[0].Value.ToString();
                        string tempPrice = row.Cells[1].Value.ToString();
                        string tempQty = row.Cells[2].Value.ToString();
                        string tempTotal = row.Cells[3].Value.ToString();
                        writer.WriteLine($"{tempTitle},{tempPrice},{tempQty},{tempTotal}");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File 'orders.txt' not found to write.", "File Write Error");
            }
            // Catch generic I/O exceptions.
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }

            /********* Write to database. ***********/

            string ConnectionString = "server=localhost;user=root;database=book store;password=";
            MySqlConnection DBConnect = new MySqlConnection(ConnectionString);
            DBConnect.Open();

            // Extract customer's first and last name into string array.
            string[] custName = new string[2];
            custName = CustomerSelectBox.Text.Split();

            /*** Push to mySQL orders table. ***/
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = $"Insert into orders values(" +
                $"'null'," +
                $"(Select cust_id from customer where " +
                $"first='{custName[0]}' " +
                $"AND last='{custName[1]}')," +
                $"cast('{SubtotalTextBox.Text.Substring(1)}' as decimal(12,2))," +
                $"cast('{TaxTextBox.Text.Substring(1)}' as decimal(10,2))," +
                $"cast('{TotalTextBox.Text.Substring(1)}' as decimal(12,2))," +
                $"CURRENT_DATE)";

            cmd.Connection = DBConnect;
            cmd.ExecuteNonQuery();
            DBConnect.Close();

            // Get total quantity of books on order.
            int qtyOfBooks;

            /*** Push to mySQL order_details. ***/
            DBConnect.Open();
            foreach (DataGridViewRow row in OrderSummaryData.Rows)
            {
                qtyOfBooks = 0;
                qtyOfBooks += Convert.ToInt32(row.Cells[2].Value);
                // max(order_id) will get the most recent (current) order.
                cmd.CommandText = $"Insert into order_details values(" +
                                  $"(Select MAX(order_id) from orders)," +
                                  $"(Select book_id from books where title='{Convert.ToString(row.Cells[0].Value)}')," +
                                  $"{qtyOfBooks}, " +
                                  $"{Convert.ToDecimal(Convert.ToString(row.Cells[3].Value).Substring(1))})";
                cmd.Connection = DBConnect;
                cmd.ExecuteNonQuery();
            }


            // Extra Credit: Create a reader to iterate through customer first and
            // and save fields to display.
            cmd.CommandText = "Select * from customer INNER JOIN orders " +
                              "USING (cust_id) where order_id = (" +
                              "select MAX(order_id) from orders)";

            string first = "";
            string last = "";
            string email = "";
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                first = reader.GetString(1);
                last = reader.GetString(2);
                email = reader.GetString(8);
            }
            reader.Close();
            // Get each book's information to display.
            cmd.CommandText = "Select * from books inner join " +
                              "order_details using (book_id) where " +
                              "order_id = (select max(order_id) from " +
                              "order_details)";
            
            reader = cmd.ExecuteReader();

            List<Book> bookList = new List<Book>();
            List<int> qty = new List<int>();
            while (reader.Read())
            {
                Book tempBook = new Book(reader.GetString(2), reader.GetString(3),
                                         reader.GetString(4), reader.GetString(1));
                bookList.Add(tempBook);
                qty.Add(reader.GetInt32(6));
            }
            reader.Close();

            StringBuilder str = new StringBuilder();
            int index = 0;
            foreach (Book book in bookList)
            {
                str.Append(book.Title);
                str.Append(" | " + book.Author);
                str.Append(" | " + book.ISBN);
                str.Append(" | " + book.Price + " ea");
                str.Append(" | " + qty[index] + " Unit(s) Ordered\n\n");
                index++;
            }
            MessageBox.Show($"Customer Info: \nName: {first} {last}\n" +
                            $"Email: {email}\n\nItems Ordered: \n" +
                            $"{str}Total Order Cost: {TotalTextBox.Text}", "Order Information");
            DBConnect.Close();


            // Clear variables and fields.
            OrderSummaryData.Rows.Clear();

            OrderSummaryData.Rows[0].Cells[0].Value = "No Selection Made";
            CancelOrderButton.Enabled = false;
            SubtotalTextBox.Text = "$0.00";
            TaxTextBox.Text = "$0.00";
            TotalTextBox.Text = "$0.00";
        }

        /// <summary>
        /// Resets the datagridview and clears the bool variable used in adding title method if user confirms
        /// in the displayed messageBox.
        /// Also clears all relevant textbox fields (Tax, Total, etc).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelOrderButton_Click(object sender, EventArgs e)
        {
            // Display messagebox -> If user clicks yes, cancel order and reset relevant fields.
            if (MessageBox.Show("Cancel Order?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                OrderSummaryData.Rows.Clear();
                firstTitleAdded = false;
                currentRowIndex = 0;
                OrderSummaryData.Rows[0].Cells[0].Value = "No Selection Made";
                CancelOrderButton.Enabled = false;
                SubtotalTextBox.Text = "$0.00";
                TaxTextBox.Text = "$0.00";
                TotalTextBox.Text = "$0.00";
            }
        }

        /// <summary>
        /// Populates the textbox fields with the properties of the book selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookSelectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Do not fire selection changed when user first clicks (box is populated).
            if (BookSelectionBox.DropDownStyle == ComboBoxStyle.DropDown)
            {
                // Change style to dropdownlist to avoid entries into combobox.
                BookSelectionBox.DropDownStyle = ComboBoxStyle.DropDownList;
                return;
            }

            // Else
            string connectionString = "server=localhost;user=root;database=book store;password=";
            using (MySqlConnection DBConnect = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand($"select author, isbn, price from books where title='{BookSelectionBox.Text}'", DBConnect);
                // Open Connection and retrieve data using mysql reader
                // SRC: https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/retrieving-data-using-a-datareader
                DBConnect.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    AuthorTextBox.Text = reader.GetString(0);
                    ISBNTextBox.Text = reader.GetString(1);
                    PriceTextBox.Text = reader.GetString(2);
                }
                DBConnect.Close();
            }
        }

        /// <summary>
        /// This event will populate the ComboBox from mySQL database (books table) on first user click only.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookSelectionBox_Click(object sender, EventArgs e)
        {

            string ConnectionString = "server=localhost;user=root;database=book store;password=";
            MySqlConnection DBConnect = new MySqlConnection(ConnectionString);

            MySqlDataAdapter da = new MySqlDataAdapter("select* from books", DBConnect);

            // Populate the comboBox with books from database.
            DBConnect.Open();
            DataSet ds = new DataSet();
            da.Fill(ds, "books");
            DBConnect.Close();
            BookSelectionBox.DataSource = ds.Tables["books"];
            BookSelectionBox.DisplayMember = "title";
        }

        /// <summary>
        /// Back to main menu on button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, EventArgs e)
        {
            MainMenu form = new MainMenu();
            this.Close();
            form.Show();
        }

        /// <summary>
        /// Populate the customer comboBox with customer info
        /// from mySQL database on click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerSelectBox_Click(object sender, EventArgs e)
        {
            if (CustomerSelectBox.Items.Count != 0) return;

            // Change style to dropdownlist to avoid entries into combobox.
            CustomerSelectBox.DropDownStyle = ComboBoxStyle.DropDownList;

            string ConnectionString = "server=localhost;user=root;database=book store;password=";
            MySqlConnection DBConnect = new MySqlConnection(ConnectionString);

            // Use concat to display customers full name.
            MySqlDataAdapter da = new MySqlDataAdapter("select concat(first, ' ',last) as fullName from customer", DBConnect);

            // Populate the comboBox with books from database.
            DBConnect.Open();
            DataSet ds = new DataSet();
            da.Fill(ds, "customer");
            DBConnect.Close();

            CustomerSelectBox.DisplayMember = "fullName";
            CustomerSelectBox.ValueMember = "fullName";
            CustomerSelectBox.DataSource = ds.Tables["customer"];

        }
    }
}
