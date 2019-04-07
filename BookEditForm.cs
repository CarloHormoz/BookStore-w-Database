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
    public partial class BookEditForm : Form
    {
        public BookEditForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clears fields and sets necessary fields to prepare for
        /// save.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewBookButton_Click(object sender, EventArgs e)
        {
            AuthorTextBox.Clear();
            TitleTextBox.Clear();
            ISBNTextBox.Clear();
            PriceTextBox.Clear();
            BookSelectBox.Enabled = false;
            SaveButton.Enabled = true;
            CancelButton.Enabled = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Display messagebox -> If user clicks no, cancel database update.
            if (MessageBox.Show("Confirm Update?", "", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            // Check for empty boxes.
            if (AuthorTextBox.Text == "" || TitleTextBox.Text == "" || ISBNTextBox.Text == "" || PriceTextBox.Text == "")
            {
                MessageBox.Show("Fields cannot be empty.");
                return;
            }

            string ConnectionString = "server=localhost;user=root;database=book store;password=";
            MySqlConnection DBConnect = new MySqlConnection(ConnectionString);
            DBConnect.Open();
            MySqlCommand cmd = new MySqlCommand();

            // If the combobox is disabled, add new book into database.
            if (!BookSelectBox.Enabled)
            {
                cmd.CommandText = $"Insert into books values(null,'{TitleTextBox.Text}','{AuthorTextBox.Text}','{ISBNTextBox.Text}','{PriceTextBox.Text}')";
                cmd.Connection = DBConnect;
                cmd.ExecuteNonQuery();
                DBConnect.Close();

                AuthorTextBox.Clear();
                TitleTextBox.Clear();
                ISBNTextBox.Clear();
                PriceTextBox.Clear();

                // Call function to update the combobox with new book added.
                BookSelectComboBox_Click(sender, e);

                BookSelectBox.Enabled = true;
                SaveButton.Enabled = false;
                MessageBox.Show("Book added to database.");
                return;
            }
            // Else update the currently selected book
            cmd.CommandText = $"Update books set title='{TitleTextBox.Text}', author='{AuthorTextBox.Text}',isbn='{ISBNTextBox.Text}',price='{PriceTextBox.Text}' where title='{BookSelectBox.Text}'";
            cmd.Connection = DBConnect;
            cmd.ExecuteNonQuery();
            DBConnect.Close();
            MessageBox.Show("Book successfully updated.");
        }

        /// <summary>
        /// If user confirms cancel, repopulate the textboxes with the
        /// currently selected item's (databased) information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            // Display messagebox -> If user clicks yes, cancel current changes.
            if (MessageBox.Show("Cancel?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                BookSelectBox_SelectedIndexChanged(sender, e);
                BookSelectBox.Enabled = true;
            }
        }

        /// <summary>
        /// Back to main menu on back button click.
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
        /// Initialize database connection to books in mySQL when combobox is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookSelectComboBox_Click(object sender, EventArgs e)
        {
            if (BookSelectBox.Items.Count != 0) return;

            // Change style to dropdownlist to avoid entries into combobox.
            BookSelectBox.DropDownStyle = ComboBoxStyle.DropDownList;

            string ConnectionString = "server=localhost;user=root;database=book store;password=";
            MySqlConnection DBConnect = new MySqlConnection(ConnectionString);

            MySqlDataAdapter da = new MySqlDataAdapter("select* from books", DBConnect);

            // Populate the comboBox with books from database.
            DBConnect.Open();
            DataSet ds = new DataSet();
            da.Fill(ds, "books");
            DBConnect.Close();
            BookSelectBox.DisplayMember = "Title";
            BookSelectBox.DataSource = ds.Tables["books"];
            CancelButton.Enabled = true;

        }

        /// <summary>
        /// Fill textboxes with appropriate data when book is selected from ComboBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookSelectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string connectionString = "server=localhost;user=root;database=book store;password=";
            using (MySqlConnection DBConnect = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand($"select title, author, isbn, price from books where title='{BookSelectBox.Text}'", DBConnect);
                // Open Connection and retrieve data using mysql reader
                // SRC: https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/retrieving-data-using-a-datareader
                DBConnect.Open();

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    TitleTextBox.Text = reader.GetString(0);
                    AuthorTextBox.Text = reader.GetString(1);
                    ISBNTextBox.Text = reader.GetString(2);
                    PriceTextBox.Text = reader.GetString(3);               
                }
                DBConnect.Close();
                SaveButton.Enabled = true;
            }
        }
    }
}
