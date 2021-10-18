using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReservationApplication
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            LoginUser.Text = LoginPass.Text = string.Empty;
        }

        private void Registerbtn_Click(object sender, EventArgs e)
        {
            Register DBox = new Register();
            DBox.ShowDialog();

        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {

            AirbnbDBEntities1 Ent = new AirbnbDBEntities1();


            AMES am = new AMES();
          
            var users = (from em in Ent.Users select em).ToList();

            bool flag = true;
            foreach (var user in users)
            {

                if (user.User_Name == LoginUser.Text && user.User_Password == LoginPass.Text)
                {

                    MessageBox.Show("Welcome " + LoginUser.Text);
                    am.ShowDialog();
                  
                    flag = false;
                    
                }
            }
            if (flag)
            {
                MessageBox.Show("User does not exist !!");
                LoginUser.Text = LoginPass.Text = string.Empty;
            }

            LoginUser.Text = LoginPass.Text = string.Empty;


        }

        private void GuestBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void GuestBtn_Click_1(object sender, EventArgs e)
        {
            AirbnbDBEntities1 db = new AirbnbDBEntities1();
            Guest g = new Guest();
            var m = (from r in db.Guests
                     where r.Guest_ID == 2
                     select r).First();
            m.Guest_Name = "1";
            db.SaveChanges();
            AMES f = new AMES();
            f.ShowDialog();

        }
    }

}
