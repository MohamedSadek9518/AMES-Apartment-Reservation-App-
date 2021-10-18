using System;
using System.Activities;
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
    public partial class NewHost : Form
    {

        Location location = new Location();
        string ImagePath;
        Random rand = new Random();
        AMES ames;
        public NewHost()
        {
            InitializeComponent();
        }
        public NewHost(AMES am)
        {
            InitializeComponent();
            ames = am;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // open file dialog   
            openFileDialog1.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            // image filters  
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImagePath = openFileDialog1.FileName;
                // display image in picture box  
                pictureBox1.Image = new Bitmap(ImagePath);
                // image file path  
                ID.Text = rand.Next(1, 1000000000).ToString();
            }
        }
        private void Submit_Click(object sender, EventArgs e)
        {
            AirbnbDBEntities1 db1 = new AirbnbDBEntities1();
            //Save Image in folder
                     string SavedImgName = ImagePath.Replace(ImagePath, ID.Text + ".jpg");
            
            string wordsToBeSearched = @"\bin\Debug";
            int wordIndex = Application.StartupPath.IndexOf(wordsToBeSearched);
            if (wordIndex != -1)
            {
                string NewAppDir = Application.StartupPath.Substring(0, wordIndex);
                System.IO.Directory.CreateDirectory(NewAppDir + @"\Images\");
                 string SavedImgDir = NewAppDir + @"\Images\" + SavedImgName;
                   pictureBox1.Image.Save(SavedImgDir);
            }



            AirbnbDBEntities1 Ent = new AirbnbDBEntities1();

            //Save data to database
            location.imgNumber = int.Parse(ID.Text);
            location.City = City.Text;
            location.Location_Address = Address.Text;
            location.Location_Price = int.Parse(Price.Text);
            location.Location_Name = District.Text;
            location.Location_IS_Reserved = 0;



            db1.Locations.Add(location);
            db1.SaveChanges();
            this.DialogResult = DialogResult.OK;
            this.Close();
            ames.LoadLocations();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void NewHost_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}