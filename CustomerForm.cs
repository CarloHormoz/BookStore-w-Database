using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using MySql.Data.MySqlClient;

namespace Lab1
{
    public partial class CustomerForm : Form
    {
        /*
        // List to hold customers to be displayed in ComboBox.
        private List<Customer> customers = new List<Customer>();
        */

        public CustomerForm()
        {
            InitializeComponent();

            /* Read all existing customers from text file, split fields using comma delimiter, create customer and add to list.
            string[] customerData = File.ReadAllLines("customers.txt");
            // If file was not empty.
            if (customerData.Length > 0)
            {
                foreach (var line in customerData)
                {
                    string[] fields = line.Split(',');
                    Customer tempCustomer = new Customer(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5], fields[6], fields[7]);
                    customers.Add(tempCustomer);
                    CustomerSelectBox.Items.Add(tempCustomer);
                }
            }
           // Set combobox to display first and last name of customers.  
            CustomerSelectBox.DisplayMember = "fullName";
        */
        }

        // Variables used for regular expression matching in saveButtonClick.
        string name = @"[^A-Za-z']*$";
        string city = @"^([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+)$";
        string phone = @"^([0-9]{10})$";    // Phone will only take format xxxxxxxxxx
        string email = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        string address = @"^([0-9]+\s+([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+))$";
        string zip = @"^([0-9]{5})$";


        /// <summary>
        /// Initially populate the comboBox with customers from mySQL
        /// database. Set the display and valuemembers of the box.
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
            CancelButton.Enabled = true;

        }

        /// <summary>
        /// When a customer is selected, enable the save button and
        /// populate the textBoxes with the selected customer's info.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerSelectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;user=root;database=book store;password=";
            using (MySqlConnection DBConnect = new MySqlConnection(connectionString))
            {
                // Extract first and last name from currently selected Combo Item.
                string [] name = new string[2];
                name = CustomerSelectBox.Text.Split();
                
                MySqlCommand command = new MySqlCommand($"select first,last,address,city,state,zip,phone,email from customer where first='{name[0]}' AND last='{name[1]}'", DBConnect);
                // Open Connection and retrieve data using mysql reader
                // SRC: https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/retrieving-data-using-a-datareader
                DBConnect.Open();

                // Read fields into textboxes.
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    FirstNameBox.Text = reader.GetString(0);
                    LastNameBox.Text = reader.GetString(1);
                    AddressBox.Text = reader.GetString(2);
                    CityBox.Text = reader.GetString(3);
                    StateBox.Text = reader.GetString(4);
                    ZipBox.Text = reader.GetString(5);
                    PhoneBox.Text = reader.GetString(6);
                    EmailBox.Text = reader.GetString(7);
                }
                DBConnect.Close();
                SaveButton.Enabled = true;
            }
        }

        /// <summary>
        /// Clicking button checks if all fields have plausible entires (not empty or wrong format). Then
        /// updates currently selected customer with new information. Uses email as unique identifier, but 
        /// does not check if email already exists. (Not sure how...)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            bool valid = true; // Boolean will be set to false if any tests fail, used to check if customer should be saved.
            if (FirstNameBox.Text == "" || !Regex.IsMatch(FirstNameBox.Text, name))
            {
                MessageBox.Show("Please enter your first name", "First name is a required field");
                FirstNameBox.Focus();
                valid = false;
            }
            if (LastNameBox.Text == "" || !Regex.IsMatch(LastNameBox.Text, name))
            {
                MessageBox.Show("Please enter your last name", "Last name is a required field");
                LastNameBox.Focus();
                valid = false;
            }
            if (AddressBox.Text == "" || !Regex.IsMatch(AddressBox.Text, address))
            {
                MessageBox.Show("Please enter your address", "Address is a required field");
                AddressBox.Focus();
                valid = false;
            }
            if (CityBox.Text == "" || !Regex.IsMatch(CityBox.Text, city))
            {
                MessageBox.Show("Please enter your city", "City is a required field");
                CityBox.Focus();
                valid = false;
            }
            if (StateBox.Text == "" || !Regex.IsMatch(StateBox.Text, name))
            {
                MessageBox.Show("Please enter your state", "State is a required field");
                StateBox.Focus();
                valid = false;
            }
            if (ZipBox.Text == "" || !Regex.IsMatch(ZipBox.Text, zip))
            {
                MessageBox.Show("Please enter your zip code", "Zip Code is a required field");
                ZipBox.Focus();
                valid = false;
            }
            if (PhoneBox.Text == "" || !Regex.IsMatch(PhoneBox.Text, phone))
            {
                MessageBox.Show("Please enter your phone number", "Phone number is a required field");
                PhoneBox.Focus();
                valid = false;
            }
            if (EmailBox.Text == "" || !Regex.IsMatch(EmailBox.Text, email))
            {
                MessageBox.Show("Please enter your email address", "Email is a required field");
                EmailBox.Focus();
                valid = false;
            }

            // If invalid entry, do not save.
            if (!valid) return;

            // Else
            string ConnectionString = "server=localhost;user=root;database=book store;password=";
            MySqlConnection DBConnect = new MySqlConnection(ConnectionString);
            DBConnect.Open();
            MySqlCommand cmd = new MySqlCommand();

            // If the combobox is disabled, add new customer into database.
            if (!CustomerSelectBox.Enabled)
            {

                cmd.CommandText = $"Insert into customer values" +
                    $"('null','{FirstNameBox.Text}'," +
                    $"'{LastNameBox.Text}'," +
                    $"'{AddressBox.Text}'," +
                    $"'{CityBox.Text}'," +
                    $"'{StateBox.Text}'," +
                    $"'{ZipBox.Text}'," +
                    $"'{PhoneBox.Text}'," +
                    $"'{EmailBox.Text}')";

                cmd.Connection = DBConnect;
                cmd.ExecuteNonQuery();
                DBConnect.Close();

                FirstNameBox.Clear();
                LastNameBox.Clear();
                AddressBox.Clear();
                CityBox.Clear();
                StateBox.Clear();
                ZipBox.Clear();
                PhoneBox.Clear();
                EmailBox.Clear();

                // Call function to update the combobox with new customer added.
                CustomerSelectBox_Click(sender, e);

                CustomerSelectBox.Enabled = true;
                SaveButton.Enabled = false;
                MessageBox.Show("Customer added to database.");
                return;
            }
            // Else update the currently selected customer with textbox info

            // Extract first and last name into string array.
            string[] custName = new string[2];
            custName = CustomerSelectBox.Text.Split();

            cmd.CommandText = $"Update customer set" +
                $" first='{FirstNameBox.Text}'," +
                $"last='{LastNameBox.Text}'," +
                $"address='{AddressBox.Text}'," +
                $"city='{CityBox.Text}'," +
                $"state='{StateBox.Text}'," +
                $"zip='{ZipBox.Text}'," +
                $"phone='{PhoneBox.Text}'," +
                $"email='{EmailBox.Text}'" +
                $" where first='{custName[0]}' AND last ='{custName[1]}'";
            cmd.Connection = DBConnect;
            cmd.ExecuteNonQuery();
            DBConnect.Close();
            MessageBox.Show("Customer successfully updated.");
            return;




            /*
            SaveButton.Enabled = false;     // Disable the save button again until new customer is selected from the comboBox.

            // Rewrite the entire file, omitting the line with the email of the customer that is being updated, since email is unique.
            // SRC: https://stackoverflow.com/questions/10371630/c-sharp-text-file-search-for-specific-word-and-delete-whole-line-of-text-that-co
            Customer tempCustomer = (Customer)(CustomerSelectBox.SelectedItem);
            var oldLines = File.ReadAllLines("customers.txt");
            var newLines = oldLines.Where(line => !line.Contains(tempCustomer.email));
            File.WriteAllLines("customers.txt", newLines);

            customers.Remove((Customer)CustomerSelectBox.SelectedItem);  // Remove old customer info from customer list.
            
            // Add new info to list.
            tempCustomer = new Customer(FirstNameBox.Text, LastNameBox.Text, AddressBox.Text, CityBox.Text, StateBox.Text, ZipBox.Text, PhoneBox.Text, EmailBox.Text);
            customers.Add(tempCustomer);

            // Close reader and inFile even if exception is thrown.
            // SRC:https://stackoverflow.com/questions/86766/how-to-properly-handle-exceptions-when-performing-file-io 
            try
            {
                using (FileStream outFile = new FileStream("customers.txt", FileMode.Append, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(outFile))
                {
                    // Write info to file.
                    writer.WriteLine($"{FirstNameBox.Text},{LastNameBox.Text},{AddressBox.Text},{CityBox.Text},{StateBox.Text},{ZipBox.Text},{PhoneBox.Text},{EmailBox.Text}");
                }

                // Clear Text Boxes.
                FirstNameBox.Clear();
                LastNameBox.Clear();
                AddressBox.Clear();
                CityBox.Clear();
                StateBox.Clear();
                ZipBox.Clear();
                PhoneBox.Clear();
                EmailBox.Clear();

                // Update comboBox with new info.
                CustomerSelectBox.Items.Remove(CustomerSelectBox.SelectedItem);
                CustomerSelectBox.Items.Add(tempCustomer);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File 'customers.txt' not found to write.", "File Write Error");
            }
            // Catch generic I/O exceptions.
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
            */

        }

        /// <summary>
        /// Clears all fields and sets necessary enables/disables to
        /// prepare for new customer entry into database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewCustomerButton_Click(object sender, EventArgs e)
        {
            FirstNameBox.Clear();
            LastNameBox.Clear();
            AddressBox.Clear();
            CityBox.Clear();
            StateBox.Clear();
            ZipBox.Clear();
            PhoneBox.Clear();
            EmailBox.Clear();
            CustomerSelectBox.Enabled = false;
            CancelButton.Enabled = true;
            SaveButton.Enabled = true;
            return;

            /*
            // When add customer button is first clicked, display message and disable box.
            if (CustomerSelectBox.Enabled == true)
            {
                MessageBox.Show("To add a customer, fill in the fields and click Add Customer again", "Add Customer");
                CustomerSelectBox.Enabled = false;
                return;
            }

            // Once form is in "add customer" mode, add customer normally.
            else
            {
                bool valid = true; // Boolean will be set to false if any tests fail, used to check if customer should be saved.
                if (FirstNameBox.Text == "" || Regex.IsMatch(FirstNameBox.Text, name))
                {
                    MessageBox.Show("Please enter your first name", "First name is a required field");
                    FirstNameBox.Focus();
                    valid = false;
                }
                if (LastNameBox.Text == "" || Regex.IsMatch(LastNameBox.Text, name))
                {
                    MessageBox.Show("Please enter your last name", "Last name is a required field");
                    LastNameBox.Focus();
                    valid = false;
                }
                if (AddressBox.Text == "" || Regex.IsMatch(AddressBox.Text, address))
                {
                    MessageBox.Show("Please enter your address", "Address is a required field");
                    AddressBox.Focus();
                    valid = false;
                }
                if (CityBox.Text == "" || Regex.IsMatch(CityBox.Text, name))
                {
                    MessageBox.Show("Please enter your city", "City is a required field");
                    CityBox.Focus();
                    valid = false;
                }
                if (StateBox.Text == "" || Regex.IsMatch(StateBox.Text, name))
                {
                    MessageBox.Show("Please enter your state", "State is a required field");
                    StateBox.Focus();
                    valid = false;
                }
                if (ZipBox.Text == "" || Regex.IsMatch(ZipBox.Text, zip))
                {
                    MessageBox.Show("Please enter your zip code", "Zip Code is a required field");
                    ZipBox.Focus();
                    valid = false;
                }
                if (PhoneBox.Text == "" || Regex.IsMatch(PhoneBox.Text, phone))
                {
                    MessageBox.Show("Please enter your phone number", "Phone number is a required field");
                    PhoneBox.Focus();
                    valid = false;
                }
                if (EmailBox.Text == "" || Regex.IsMatch(EmailBox.Text, email))
                {
                    MessageBox.Show("Please enter your email address", "Email is a required field");
                    EmailBox.Focus();
                    valid = false;
                }

                // If no issues, add customer to text file and ComboBox.
                if (valid)
                {
                    Customer tempCustomer = new Customer(FirstNameBox.Text, LastNameBox.Text, AddressBox.Text, CityBox.Text, StateBox.Text, ZipBox.Text, PhoneBox.Text, EmailBox.Text);
                    customers.Add(tempCustomer);

                    // Close reader and inFile even if exception is thrown.
                    // SRC:https://stackoverflow.com/questions/86766/how-to-properly-handle-exceptions-when-performing-file-io 
                    try
                    {
                        using (FileStream outFile = new FileStream("customers.txt", FileMode.Append, FileAccess.Write))
                        using (StreamWriter writer = new StreamWriter(outFile))
                        {
                            // Write info to file.
                            writer.WriteLine($"{FirstNameBox.Text},{LastNameBox.Text},{AddressBox.Text},{CityBox.Text},{StateBox.Text},{ZipBox.Text},{PhoneBox.Text},{EmailBox.Text}");
                            // Clear Text Boxes.
                            FirstNameBox.Clear();
                            LastNameBox.Clear();
                            AddressBox.Clear();
                            CityBox.Clear();
                            StateBox.Clear();
                            ZipBox.Clear();
                            PhoneBox.Clear();
                            EmailBox.Clear();
                            CustomerSelectBox.Items.Add(tempCustomer);
                            CustomerSelectBox.Enabled = true;
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        MessageBox.Show("File 'customers.txt' not found to write.", "File Write Error");
                    }
                    // Catch generic I/O exceptions.
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error");
                    }

                }
            }
            */
        }

        /// <summary>
        /// If user confirms cancel, repopulate textboxes with unchanged
        /// information currently in database for that customer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            // Display messagebox -> If user clicks yes, cancel current changes.
            if (MessageBox.Show("Cancel?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CustomerSelectBox_SelectedIndexChanged(sender, e);
                CustomerSelectBox.Enabled = true;
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
    }
}
