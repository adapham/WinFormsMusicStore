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
    
    public partial class MainAdmin : Form
    {
        MusicStoreContext context;
        int id,idGenren,idArtist;
        List<int> listCart = new List<int>();
        public MainAdmin()
        {
            context = new MusicStoreContext();
            InitializeComponent();
            bindGrid(false);
        }


        private void MainAdmin_Load(object sender, EventArgs e)
        {

        }
         public void bindGrid(bool check)
        {
            dataGridView1.Columns.Clear();
            context = new MusicStoreContext();
            var dt = context.Genres.ToList();
            comboBoxGenre.DataSource = dt;
            comboBoxGenre.DisplayMember = "Name";
            comboBoxGenre.ValueMember = "GenreId";
            comboBoxGenre.SelectedValue = 1;
            var dt1 = context.Artists.ToList();
            comboBoxArtist.DataSource = dt1;
            comboBoxArtist.DisplayMember = "Name";
            comboBoxArtist.ValueMember = "ArtistId";
            comboBoxArtist.SelectedValue = 1;
            if (check)
            {
                dataGridView1.DataSource = context.Albums
                 .Where(s => s.Title.Contains(textBoxSearch.Text.Trim()))
                 .OrderByDescending(s => s.Title)
                 .ToList();

            }
            else
            {
                dataGridView1.DataSource = context.Albums.ToList();
            }
            dataGridView1.Columns["artis"].Visible = false;
            dataGridView1.Columns["genre"].Visible = false;
            dataGridView1.Columns["orderdetails"].Visible = false;
           
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
            private void MainAdmin_Activated(object sender, EventArgs e)
        {
            showToolStripMenuItem.Text = $"Logout ({Settings.UserName})";
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Settings.UserName = "";
            MessageBox.Show("You are logged out");
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            var dt = context.Genres.ToList();
            id = (int)dataGridView1.Rows[e.RowIndex].Cells["AlbumId"].Value;
            idArtist= (int)dataGridView1.Rows[e.RowIndex].Cells["ArtisId"].Value;
            idGenren= (int)dataGridView1.Rows[e.RowIndex].Cells["GenreId"].Value;
            txtTitle.Text = (string)dataGridView1.Rows[e.RowIndex].Cells["Title"].Value;
            txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells["Price"].Value.ToString();
            comboBoxGenre.DataSource = dt;
            
                comboBoxGenre.DisplayMember = "Name";
            comboBoxGenre.ValueMember = "GenreId";
            comboBoxGenre.SelectedValue = idGenren;
            var dt1 = context.Artists.ToList();
            comboBoxArtist.DataSource = dt1;
            comboBoxArtist.DisplayMember = "Name";
            comboBoxArtist.ValueMember = "ArtistId";
            
            comboBoxArtist.SelectedValue = idArtist;
            txtAlbumUrl.Text= (string)dataGridView1.Rows[e.RowIndex].Cells["AlbumUrl"].Value;
            //Check to ensure that the row CheckBox is clicked.

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void txtAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtAlbumUrl.Text))
            {
                MessageBox.Show("input not empty");
                return;
            }
            try
            {
                Album album = new Album();
                album.GenreId = (int)comboBoxGenre.SelectedValue;
                album.ArtisId = (int)comboBoxArtist.SelectedValue;
                album.Title = txtTitle.Text;
                album.Price = float.Parse(txtPrice.Text);
                album.AlbumUrl = txtAlbumUrl.Text;
                context.Albums.Add(album);
                context.SaveChanges();
                MessageBox.Show("Added");
                bindGrid(false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddGenre_Click(object sender, EventArgs e)
        {
            CustomizeGenne g = new CustomizeGenne();
            DialogResult dr = g.ShowDialog();
            if (dr == DialogResult.Cancel)
                bindGrid(false);

        }

        private void btnAddArtist_Click(object sender, EventArgs e)
        {
            CustomizeArtists a = new CustomizeArtists(context);
            DialogResult dialogResulta = a.ShowDialog();
            if (dialogResulta == DialogResult.Cancel)
                bindGrid(false);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bindGrid(true);
        }

        private void txtPrice_Validating(object sender, CancelEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtPrice.Text, "[^0-9]"))
            {
                string temp = txtPrice.Text;
                MessageBox.Show("Please enter only numbers.");

                txtPrice.Text = txtPrice.Text.Remove(txtPrice.Text.Length - 1);
                txtPrice.Text = temp;

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtAlbumUrl.Text))
            {
                MessageBox.Show("input not empty");
                return;
            }

            try
            {
                Album album = context.Albums.Find(id);
                album.GenreId = (int)comboBoxGenre.SelectedValue;
                album.ArtisId = (int)comboBoxArtist.SelectedValue;
                album.Title = txtTitle.Text;
                album.Price = float.Parse(txtPrice.Text);
                album.AlbumUrl = txtAlbumUrl.Text;
                context.Albums.Update(album);
                context.SaveChanges();
                MessageBox.Show("Updated");
                bindGrid(false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
 Album album = context.Albums.Find(id);
            context.Albums.Remove(album);
            context.SaveChanges();
                MessageBox.Show("Deleted");
                bindGrid(false);
            }
           
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
