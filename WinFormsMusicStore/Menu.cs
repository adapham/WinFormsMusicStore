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
    
    public partial class Menu : Form
    {
        public List<Temp> list = new List<Temp>();
        MusicStoreContext context;  
        int id;
        List<int> listCart = new List<int>();
        DataTable dataTable = new DataTable();
        public Menu(MusicStoreContext context)
        {
            InitializeComponent();
            //context = new MusicStoreContext();
            this.context = context;
            bindGrid();
            dataTable.Columns.Add("AlbumId", typeof(int));
            dataTable.Columns.Add("GenreId", typeof(int));
            dataTable.Columns.Add("ArtisId", typeof(int));
            dataTable.Columns.Add("Title", typeof(string));
            dataTable.Columns.Add("Price", typeof(float));
            dataTable.Columns.Add("Quantity", typeof(int));
            //LoadDataGridView();

        }
         
        //private void LoadDataGridView()
        //{
        //    dataGridView2.Columns.Clear();
            
            
        //}

        void bindGrid()
        {
          
            dataGridView1.Columns.Clear();


            List<Album> albums1 = new List<Album>();
            albums1 = context.Albums.ToList<Album>();
            
            dataGridView1.DataSource  = albums1;
            
            dataGridView1.Columns["OrderDetails"].Visible = false;
            dataGridView1.Columns["AlbumUrl"].Visible = false;
            dataGridView1.Columns["Artis"].Visible = false;
            dataGridView1.Columns["Genre"].Visible = false; 
            // DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn
            //{
            //    Name = "Buy"
            //};
            //DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            //{
            //    Name = "quantity",
                
            //};
            //dataGridView1.Cell(1, 2).CellType = column.CellType.TextBox;

            
            
            //int count = dataGridView1.Columns.Count;
            
            //dataGridView1.Columns.Insert(count, checkBoxColumn);
            //dataGridView1.Columns.Insert(count+1, column);

            
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
        
        }
        int indexRow = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            //int count = dataGridView1.Columns.Count;
            //DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            //indexRow = e.RowIndex;
            //MessageBox.Show(indexRow.ToString());
            //textBoxQuantity.Text = row.Cells[1].Value.ToString();



            //if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            //{
            //    Reference the GridView Row.



            //    Set the CheckBox selection.

            //    row.Cells["Buy"].Value = !Convert.ToBoolean(row.Cells["Buy"].EditedFormattedValue);

            //    MessageBox.Show(row.Cells["Buy"].Value.ToString());
            //    If CheckBox is checked, display Message Box.
            //    int temp;
            //    if (Convert.ToBoolean(row.Cells["Buy"].Value))
            //    {
            //        temp = Int32.Parse(row.Cells["AlbumId"].Value.ToString());
            //        listCart.Add(temp);
            //        MessageBox.Show("Added " + row.Cells["Title"].Value);
            //    }
            //    else
            //    {
            //        temp = Int32.Parse(row.Cells["AlbumId"].Value.ToString());
            //        listCart.Remove(temp);
            //    }
            //    foreach (var n in listCart)
            //    {
            //        MessageBox.Show(n.ToString());
            //    }
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
           
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        int idar, gene;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.id= (int)dataGridView1.Rows[e.RowIndex].Cells["AlbumId"].Value;
           Album album = context.Albums.Find(id); 
            textBoxAlbumId.Text = dataGridView1.Rows[e.RowIndex].Cells["AlbumId"].Value.ToString();
            Artist artist=context.Artists.Find(album.ArtisId);
            Genre   genre = context.Genres.Find(album.GenreId);
            idar = album.ArtisId;
            gene = album.GenreId;
            textBoxGenreId.Text =artist.Name.ToString();
             textBoxArtisId.Text = genre.Name.ToString();
            textBoxTitle.Text = album.Title;
            textBoxPrice.Text = dataGridView1.Rows[e.RowIndex].Cells["Price"].Value.ToString();
        }
         
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAlbumId.Text) || string.IsNullOrEmpty(textBoxTitle.Text)
                || string.IsNullOrEmpty(textBoxPrice.Text)
                || string.IsNullOrEmpty(textBoxQuantity.Text)
                || Convert.ToInt32(textBoxQuantity.Text) ==0
                )
            {
                MessageBox.Show("input not empty or invalid");
                return;
            }

            //dataTable.Rows.Add(Convert.ToInt32(textBoxAlbumId.Text), Convert.ToInt32(textBoxGenreId.Text),
            //    Convert.ToInt32(textBoxArtisId.Text), textBoxTitle.Text,
            //    Convert.ToInt32(textBoxQuantity.Text));

            //dataGridView2.Columns.Clear();
            Temp temp = new Temp();
            temp.AlbumId = Convert.ToInt32(textBoxAlbumId.Text);
            temp.GenreId = idar;
            temp.ArtisId = gene;
            temp.Title = textBoxTitle.Text;
            temp.Price = float.Parse(textBoxPrice.Text);
            temp.Quantity = Convert.ToInt32(textBoxQuantity.Text);
            bool checkid = false; 
            foreach (var t in list)
            {
                if(t.AlbumId== Convert.ToInt32(textBoxAlbumId.Text))
                {
                    checkid = true;
                }
            }
            int tempQuantity = 0;
            if (checkid)
            {
                foreach (var t in list)
                {
                    if (t.AlbumId == Convert.ToInt32(textBoxAlbumId.Text))
                    {
                        t.Quantity += Convert.ToInt32(textBoxQuantity.Text);
                        tempQuantity = t.Quantity;
                    }
                }
            }
            else
            {
                list.Add(temp);
            }
            
            foreach (DataRow dr in dataTable.Rows) // search whole table
            {
                if ((int)dr["AlbumId"] == Convert.ToInt32(textBoxAlbumId.Text)) // if id==2
                {
                    dr["Quantity"] =tempQuantity; //change the name
                                                //break; break or not depending on you
                }
            }
            if (!checkid)
            {
                
                dataTable.Rows.Add(temp.AlbumId, temp.GenreId,
                    temp.ArtisId, temp.Title,temp.Price,
                    temp.Quantity);
            }
            
            dataGridView2.DataSource = dataTable;
            

        }

        private void textBoxQuantity_Validating(object sender, CancelEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBoxQuantity.Text, "[^0-9]"))
            {
                string temp = textBoxQuantity.Text;
                MessageBox.Show("Please enter only numbers.");

                textBoxQuantity.Text = textBoxQuantity.Text.Remove(textBoxQuantity.Text.Length - 1);
                textBoxQuantity.Text = temp;

            }
        }

        //Order o = new Order();
        private void btncheckout_Click(object sender, EventArgs e)
        {
            OrdersForCustomers ordersForCustomers = new OrdersForCustomers(list, context);
            DialogResult dialog = ordersForCustomers.ShowDialog();
            
            if(dialog == DialogResult.OK)
            {
                this.btncheckout.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }



        }
    }
}
