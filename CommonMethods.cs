using System.Drawing;
using System.Windows.Forms;

namespace The_Inventory_Manager
{
    public static class CommonMethods
    {
        public static bool priceValid(TextBox price)
        {
            if (decimal.TryParse(price.Text.Trim(), out decimal result))
            {
                price.BackColor = default(Color);
                return true;
            }
            else
            {
                price.BackColor = Color.Red;
                return false;
            }
        }

        /// <summary>
        /// Check if given textbox is empt
        /// /// </summary>
        public static bool textValid(TextBox entry)
        {
            if (entry.TextLength == 0)
            {
                entry.BackColor = Color.Red;
                return false;
            }
            else
            {
                entry.BackColor = default(Color);
                return true;
            }
        }
    }
}