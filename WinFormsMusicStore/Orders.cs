using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsMusicStore.Model;

namespace WinFormsMusicStore
{
    public partial class Orders : Form
    {
        MusicStoreContext context;
        int id;
        public Orders(MusicStoreContext context)
        {
            this.context = context;
            InitializeComponent();
            bindGrid(false);
        }

        public void bindGrid(bool check)
        {
            dataGridView1.Columns.Clear();
            if (check)
            {
                dataGridView1.DataSource = context.Orders
                 .Where(s => s.FirstName.Contains(textBoxFind.Text))
                 .OrderByDescending(s => s.FirstName  )
                 .ToList();

            }
            else
            {
                dataGridView1.DataSource = context.Orders.ToList();
            }
            
            dataGridView1.Columns["OrderDetails"].Visible = false;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
       
        private void newOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu(context);
            DialogResult d = menu.ShowDialog();

            if(d == DialogResult.OK )
            {
                bindGrid(false);

            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFirstName.Text) || string.IsNullOrEmpty(textAddress.Text)
                || string.IsNullOrEmpty(textcity.Text)
                || string.IsNullOrEmpty(textBoxState.Text)
                || string.IsNullOrEmpty(textBoxCountry.Text)
                || string.IsNullOrEmpty(textBoxPhone.Text)
                || string.IsNullOrEmpty(textBoxEmail.Text)
                || string.IsNullOrEmpty(textBoxTotal.Text)
                )
            {
                MessageBox.Show("input not empty");
                return;
            }
            try
            {
            Order order = context.Orders.Find(id);
            order.FirstName = textBoxFirstName.Text;
            order.OrderDate = dateTimePicker1.Value;
            order.Address = textAddress.Text;
            order.City = textcity.Text;
            order.State = textBoxState.Text;
            order.Country = textBoxCountry.Text;
            order.Phone = Int32.Parse(textBoxPhone.Text) ;
            order.Email = textBoxEmail.Text;
            order.Total = float.Parse(textBoxTotal.Text) ;
            context.Orders.Update(order);
            context.SaveChanges();
            bindGrid(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<OrderDetail> orderDetails = context.OrderDetails.ToList<OrderDetail>();
            try
            {
                foreach (OrderDetail n in orderDetails)
                {
                    if (n.OrderId == id)
                    {
                        OrderDetail orderdetail = context.OrderDetails.Find(n.OrderDetailId);
                        context.Remove(orderdetail);
                        
                    }
                   
                }
                Order order = context.Orders.Find(id);
                context.Remove(order);
                context.SaveChanges();
                MessageBox.Show("Deleted");
                bindGrid(false);
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Refresh();
            this.id = (int)dataGridView1.Rows[e.RowIndex].Cells["OrderId"].Value;
            //MessageBox.Show(id.ToString());
            textBoxFirstName.Text = (string)dataGridView1.Rows[e.RowIndex].Cells["FirstName"].Value;
            textAddress.Text = (string)dataGridView1.Rows[e.RowIndex].Cells["Address"].Value;
            textcity.Text = (string)dataGridView1.Rows[e.RowIndex].Cells["City"].Value;
            textBoxState.Text = (string)dataGridView1.Rows[e.RowIndex].Cells["State"].Value;
            textBoxCountry.Text = (string)dataGridView1.Rows[e.RowIndex].Cells["Country"].Value;
            textBoxPhone.Text = dataGridView1.Rows[e.RowIndex].Cells["Phone"].Value.ToString();
            textBoxEmail.Text = (string)dataGridView1.Rows[e.RowIndex].Cells["Email"].Value;
            textBoxTotal.Text = dataGridView1.Rows[e.RowIndex].Cells["Total"].Value.ToString();
            dateTimePicker1.Value = (DateTime)dataGridView1.Rows[e.RowIndex].Cells["OrderDate"].Value;
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bindGrid(true);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Orders_Activated(object sender, EventArgs e)
        {
            logoutToolStripMenuItem.Text = $"Logout ({Settings.UserName})";
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.UserName = "";
            MessageBox.Show("You are logged out");
            this.Close();
        }
    }
}
