using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace The_Inventory_Manager
{
    public partial class Search : UserControl
    {
        private DataTable dt = new DataTable();

        public Search()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            searchAction();
        }

        private void Search_Load(object sender, EventArgs e)
        {
            String connString = ConfigurationManager.ConnectionStrings["Inventory"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(
             "SELECT * FROM inventory", connection);
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);

                connection.Close();
            }

            searchView.DataSource = dt;
        }

        private void searchText_TextChanged(object sender, EventArgs e)
        {
            searchAction();
        }

        private void idText_TextChanged(object sender, EventArgs e)
        {
            searchAction();
        }

        /// <summary>
        /// Performs a search using id and name
        /// </summary>
        private void searchAction()
        {
            try
            {
                if (int.TryParse(idText.Text.Trim(), out int result))
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "name LIKE '%" + searchText.Text.Trim() + "%' AND id = '" + idText.Text.Trim() + "'";
                }
                else if (idText.Text.Trim().Length != 0)
                {
                    System.Windows.Forms.MessageBox.Show("Please only enter integer IDs");
                    idText.Clear();
                }
                else
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "name LIKE '%" + searchText.Text.Trim() + "%'";
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}