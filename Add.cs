using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace The_Inventory_Manager
{
    public partial class Add : UserControl
    {
        public Add()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Adds the product to the database
        /// </summary>
        private void addProduct()
        {
            String connString = ConfigurationManager.ConnectionStrings["Inventory"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(
             "INSERT INTO inventory (name, description, quantity, price) VALUES (@name, @description, @quantity, @price)", connection);

                cmd.Parameters.AddWithValue("@name", nameTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@description", descriptionTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@quantity", quantityNumeric.Value);
                cmd.Parameters.AddWithValue("@price", priceTextBox.Text.Trim());

                connection.Open();

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        /// <summary>
        /// Clears out all the text fields
        /// </summary>
        private void clearFields()
        {
            nameTextBox.Clear();
            descriptionTextBox.Clear();
            priceTextBox.Clear();
            quantityNumeric.Value = default;
        }

        /// <summary>
        /// Add product button action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            if (CommonMethods.textValid(nameTextBox) & CommonMethods.textValid(descriptionTextBox) & CommonMethods.priceValid(priceTextBox))
            {
                addProduct();
                errorText.Text = "Item added successfully.";
                errorText.BackColor = Color.Green;
                errorText.ForeColor = Color.White;
                errorText.Visible = true;
                clearFields();
            }
            else
            {
                errorText.Text = "Error adding the product. Please check the fields.";
                errorText.BackColor = Color.Red;
                errorText.ForeColor = Color.White;
                errorText.Visible = true;
            }
        }
    }
}