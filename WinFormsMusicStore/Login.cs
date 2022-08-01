using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsMusicStore.Model;

namespace WinFormsMusicStore
{
    public partial class Login : Form
    {
        MusicStoreContext context = new MusicStoreContext();
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string nameAdmin, passAdmin,nameUser, passUser;
            var conf = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            nameAdmin = conf["User:Name"];
            passAdmin = conf["User:Password"];
            
            nameUser = conf["User1:Name"];
            passUser = conf["User1:Password"];
            //MessageBox.Show($"Name = {name}, Pass = {pass}");

            if (txbName.Text == nameAdmin &&   txbPassword.Text == passAdmin)
            {//Admin
                MessageBox.Show("You are logged in as administrator");
                Settings.UserName = nameAdmin;
                MainAdmin m = new MainAdmin();
                DialogResult dr = m.ShowDialog();

            }else if (txbName.Text == nameUser && txbPassword.Text == passUser)//User
            {
                MessageBox.Show($"You are logged in as {nameUser}");
                Settings.UserName = nameUser;
                Orders o = new Orders(context);
                DialogResult dr = o.ShowDialog();
            }
            else
                MessageBox.Show("Don't have that user");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
