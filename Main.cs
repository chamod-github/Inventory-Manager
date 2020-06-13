using System;
using System.Windows.Forms;

namespace The_Inventory_Manager
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Search products button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            changeUserControl(new Search());
        }

        /// <summary>
        /// Add product button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            changeUserControl(new Add());
        }

        /// <summary>
        /// Update product button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            changeUserControl(new Update());
        }

        /// <summary>
        /// Remove product button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            changeUserControl(new Remove());
        }

        /// <summary>
        /// Replaces the active usercontrol in the panel with the given user control
        /// </summary>
        /// <param name="userControl">User control to replace with</param>
        private void changeUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(userControl);
        }
    }
}