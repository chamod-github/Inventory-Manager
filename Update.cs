using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace The_Inventory_Manager
{
    public partial class Update : UserControl
    {
        private DataTable dt = new DataTable();

        public Update()
        {
            InitializeComponent();
        }

        private void Update_Load(object sender, EventArgs e)
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
            if (searchView.CurrentCell == null)
            {
                updateButton.Visible = false;
            }
        }

        /// <summary>
        /// Applies search filters to the search view
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

        private void searchButton_Click(object sender, EventArgs e)
        {
            searchAction();
        }

        private void searchView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            updateButton.Visible = true;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            // If a cell is selected
            if (searchView.CurrentCell != null)
            {
                // Set active user control to update, pass in the selected rows
                UpdatePage up = new UpdatePage(searchView.SelectedRows[0].Cells)
                {
                    Dock = DockStyle.Fill
                };
                Panel tmp = this.Parent as Panel;
                tmp.Controls.Clear();
                tmp.Controls.Add(up);
            }
        }
    }
}