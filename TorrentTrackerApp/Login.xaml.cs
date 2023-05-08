using DateBase.Models;
using DateBase.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TorrentTrackerApp
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {        
        
        public Login()
        {
            //init iniy
            InitializeComponent();
        }

        private void button_login_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(login_Box.Text);

            if (password_Box.Text.Equals("") && login_Box.Text.Equals(""))
            {
                MessageBox.Show($"Fields login password and  are empty");
            }
            else if (login_Box.Text=="")
            {
                MessageBox.Show($"Not information in login");
            }
            else if (password_Box.Text == "")                
            {
                MessageBox.Show($"Not information in password");
            }           
            else if (password_Box.Text!= "" && login_Box.Text != "" || password_Box.Text != "" || login_Box.Text != "")
            {

                MainWindow main = new MainWindow();
                
                User check = null;

                using (StoreUserContext db = new StoreUserContext())
                {
                    check = db.Users.Where(x=>x.LoginUser==login_Box.Text && x.PaswwordUser==password_Box.Text).FirstOrDefault();
                }

                if (check != null)
                {
                    MessageBox.Show($"SUCCESS! Opening TorrentTracker", "Login");
                    this.Close();
                    main.Show();
                }
                else
                {
                    MessageBox.Show($"You need autorized! Use the SING UP", "Eror!");
                }
            }
        }

        private void button_regist_Click(object sender, RoutedEventArgs e)
        {
            if (login_Box.Text!= "" && password_Box.Text!= "" || password_Box.Text != "" || login_Box.Text != "")
            {
                using (StoreUserContext db = new StoreUserContext())
                {
                    var check = db.Users.FirstOrDefault();

                    if (check == null)
                    {
                        User newUser = new User() { LoginUser = login_Box.Text, PaswwordUser = password_Box.Text };
                        db.Add(newUser);
                        db.SaveChanges();

                        MessageBox.Show($"You autorized");                       
                    }
                }
            }
            else
            {
                MessageBox.Show($"Fields login password and  are empty");
            }                      
        }
    }
}
