using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;

namespace The_Inventory_Manager
{
    public partial class UpdatePage : UserControl
    {
        private int id;

        public UpdatePage(DataGridViewCellCollection selectedCells)
        {
            InitializeComponent();

            // Set the text boxes to passed in cell values
            nameTextBox.Text = selectedCells["name"].Value.ToString();
            descriptionTextBox.Text = selectedCells["description"].Value.ToString();
            quantityNumeric.Value = int.Parse(selectedCells["quantity"].Value.ToString());
            priceTextBox.Text = selectedCells["price"].Value.ToString();
            id = int.Parse(selectedCells["id"].Value.ToString());
        }

        /// <summary>
        /// Update the product in the database
        /// </summary>
        private void updateProduct()
        {
            String connString = ConfigurationManager.ConnectionStrings["Inventory"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(
             "UPDATE inventory SET name=@name,description= @description,quantity= @quantity, price=@price WHERE id = @id", connection);

                cmd.Parameters.AddWithValue("@name", nameTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@description", descriptionTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@quantity", quantityNumeric.Value);
                cmd.Parameters.AddWithValue("@price", SqlMoney.Parse(priceTextBox.Text.Trim()));
                cmd.Parameters.AddWithValue("@id", int.Parse(id.ToString()));

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
        /// When Update button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (CommonMethods.textValid(nameTextBox) & CommonMethods.textValid(descriptionTextBox) & CommonMethods.priceValid(priceTextBox))
            {
                updateProduct();
                errorText.Text = "Item updated successfully.";
                errorText.BackColor = Color.Green;
                errorText.ForeColor = Color.White;
                errorText.Visible = true;

                // Back to update search page
                Panel tmp = this.Parent as Panel;
                Update up = new Update
                {
                    Dock = DockStyle.Fill
                };
                tmp.Controls.Clear();
                tmp.Controls.Add(up);
            }
            else
            {
                errorText.Text = "Error updating the product. Please check the fields.";
                errorText.BackColor = Color.Red;
                errorText.ForeColor = Color.White;
                errorText.Visible = true;
            }
        }
    }
}