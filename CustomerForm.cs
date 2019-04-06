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

namespace Lab1
{
    public partial class CustomerForm : Form
    {
        // List to hold customers to be displayed in ComboBox.
        private List<Customer> customers = new List<Customer>();

        public CustomerForm()
        {
            InitializeComponent();
            // Read all existing customers from text file, split fields using comma delimiter, create customer and add to list.
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

        }

        // Variables used for regular expression matching in saveButtonClick.
        string name = "[^A-Za-z']";
        string phone = "^((1-([(][0-9]{3}[)] | [0-9]{3})-)[0-9]{3}-[0-9]{4})$";
        string email = "^[A-Z0-9._%+-]+@[A-Z0-9.-]+[.][A-Z]{2,4}$";
        string address = @"^[A - Za - z0 - 9] + (?:\s[A - Za - z0 - 9'_-]+)+$";
        string zip = "^d{5}$";

        /// <summary>
        /// When a customer is selected, enable the save button if first customer.
        /// Also populate the textBoxes with the selected customer's info.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerSelectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveButton.Enabled = true;
            // Extract info from currently selected customer and populate textboxes.
            Customer tempCustomer = (Customer)CustomerSelectBox.SelectedItem;
            FirstNameBox.Text = tempCustomer.first;
            LastNameBox.Text = tempCustomer.last;
            AddressBox.Text = tempCustomer.address;
            CityBox.Text = tempCustomer.city;
            StateBox.Text = tempCustomer.state;
            ZipBox.Text = tempCustomer.zip;
            PhoneBox.Text = tempCustomer.phone;
            EmailBox.Text = tempCustomer.email;
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

           // If no issues, delete customer from text file and list and update info.
            if (valid)
            {
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

            }
        }

        /// <summary>
        /// Add a new customer to the text file and comboBox. Disables comboBox until customer is successfully added.
        /// I set my add customer button to not set focus to first name box and to instead add the customer as soon as the button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewCustomerButton_Click(object sender, EventArgs e)
        {
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
        }

        /// <summary>
        /// Enables the CustomerSelectBox, when in "Add Customer" mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            CustomerSelectBox.Enabled = true;
        }
    }
}
