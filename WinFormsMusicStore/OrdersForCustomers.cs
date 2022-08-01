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
    public partial class OrdersForCustomers : Form
    {
        MusicStoreContext context;
        int id;float tolta=0;
        List<Album> listAlbum = new List<Album>();
        List<Temp> listOrder = new List<Temp>();
        public OrdersForCustomers(List<Temp> temps, MusicStoreContext context)
        {
            InitializeComponent();
            listOrder = temps;
            //context = new MusicStoreContext();
            this.context = context;
            
            foreach (var n in listOrder)
            {
                tolta += n.Price * n.Quantity;
            }

            
            bindGrid();
            
        }
        

         void bindGrid()
        {
            dataGridView1.Columns.Clear();
            //foreach (var n in listOrder)
            //{
            //    Album album = context.Albums.Find(n);
            //    listAlbum.Add(album);
            //}
            dataGridView1.DataSource = listOrder;
            //dataGridView1.Columns["OrderDetails"].Visible = false;
            //dataGridView1.Columns["AlbumUrl"].Visible = false;
            //dataGridView1.Columns["Artis"].Visible = false;
            //dataGridView1.Columns["Genre"].Visible = false;
            textBoxTotal.Text = tolta.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFirstName.Text) || string.IsNullOrEmpty(textBoxAddress.Text)
                || string.IsNullOrEmpty(textBoxCity.Text)
                || string.IsNullOrEmpty(textBoxState.Text)
                || string.IsNullOrEmpty(textBoxCountry.Text)
                || string.IsNullOrEmpty(textBoxPhone.Text)
                || string.IsNullOrEmpty(textBoxEmail.Text)
                )
            {
                MessageBox.Show("Order faild(input invalid)");
                return;
            }
            
            
                try
                {
                    Order order = new Order();
                    order.FirstName = textBoxFirstName.Text;
                    order.OrderDate = DateTime.Now.Date;
                    order.Address = textBoxAddress.Text;
                    order.City = textBoxCity.Text;
                    order.State = textBoxState.Text;
                    order.Country = textBoxCountry.Text;
                    order.Phone = Int32.Parse(textBoxPhone.Text);
                    order.Email = textBoxEmail.Text;
                    order.Total = Int32.Parse(textBoxTotal.Text);

                    using (var context = new MusicStoreContext())
                    {
                        context.Orders.Add(order);
                        context.SaveChanges();

                        this.id = order.OrderId; // Yes it's here
                    }

                    //MessageBox.Show(id.ToString());
                    foreach (var n in listOrder)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderId = this.id;
                        orderDetail.AlbumId = n.AlbumId;
                        orderDetail.Quantity = n.Quantity;
                        orderDetail.UnitPrice = n.Price;
                        context.OrderDetails.Add(orderDetail);
                        context.SaveChanges();
                    }
                    MessageBox.Show("Order successful");
                this.Close();



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
           
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
