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
    public partial class CustomizeGenne : Form
    {
        MusicStoreContext context;
        int id;
        public CustomizeGenne()
        {
            context = new MusicStoreContext();
            InitializeComponent();
            bindGrid(false);
        }

         void bindGrid(bool check)
        {

            dataGridView1.Columns.Clear();
            if (check)
            {
                dataGridView1.DataSource = context.Genres
                 .Where(s => s.Name.Contains(textBoxSearch.Text))
                 .OrderByDescending(s => s.Name)
                 .ToList();
            }
            else
            {
                dataGridView1.DataSource = context.Genres.ToList();
            }
           
            dataGridView1.Columns["Albums"].Visible = false;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = (int)dataGridView1.Rows[e.RowIndex].Cells["GenreId"].Value;
            txbName.Text= dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
            txbDesc.Text = dataGridView1.Rows[e.RowIndex].Cells["Desctription"].Value.ToString();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
           
            try
            {
            
                Genre genre = new Genre();
            genre.Name = txbName.Text;
            genre.Desctription = txbDesc.Text;
            
            context.Genres.Add(genre);
            context.SaveChanges();
            bindGrid(false);
                MessageBox.Show("Added");
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

Genre genre = context.Genres.Find(id);
            genre.Name = txbName.Text;
            genre.Desctription = txbDesc.Text;

            context.Genres.Update(genre);
            context.SaveChanges();
            bindGrid(false);
                MessageBox.Show("Updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Genre genre = context.Genres.Find(id);
            context.Genres.Remove(genre);
            context.SaveChanges();
            bindGrid(false);
                MessageBox.Show("Deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
           
            

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            bindGrid(true);
        }
    }
}
