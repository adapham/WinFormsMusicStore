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
    public partial class CustomizeArtists : Form
    {
        MusicStoreContext context;
        int id;
        public CustomizeArtists(MusicStoreContext context)
        {
            this.context= context;
            InitializeComponent();
            bindGrid(false);
        }

         void bindGrid(bool check)
        {
            dataGridView1.Columns.Clear();
            if (check)
            {
                dataGridView1.DataSource = context.Artists
                 .Where(s => s.Name.Contains(textBoxSearch.Text))
                 .OrderByDescending(s => s.Name)
                 .ToList();

            }
            else
            {
                dataGridView1.DataSource = context.Artists.ToList();
            }
            
            dataGridView1.Columns["Albums"].Visible = false;
           
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {

Artist artist = new Artist();
            artist.Name = textBox1.Text;
            context.Artists.Add(artist);
            context.SaveChanges();
            bindGrid(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
 Artist artist = context.Artists.Find(id);
            artist.Name = textBox1.Text;
            context.Artists.Update(artist);
            context.SaveChanges();
            bindGrid(false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void buttondelete_Click(object sender, EventArgs e)
        {
            try
            {

Artist artist = context.Artists.Find(id);
            context.Artists.Remove(artist);
            context.SaveChanges();
            bindGrid(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = (int)dataGridView1.Rows[e.RowIndex].Cells["ArtistId"].Value;
            textBox1.Text= dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            bindGrid(true);
        }
    }

}
