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

namespace ReservationApplication
{
    public partial class AMES : Form
    {
        AirbnbDBEntities1 db = new AirbnbDBEntities1();
        int rowIndex;
        public AMES()
        {
            InitializeComponent();
            LoadLocations();
            LocationsDataGrid.Columns[1].HeaderText = "City";
            LocationsDataGrid.Columns[2].HeaderText = "District";
        }

        public void LoadLocations()
        {

            LocationsDataGrid.DataSource = db.Locations
                .Select(a => new {
                    a.City,
                    a.LocationID,
                    a.Location_Name,
                    a.Location_Address,
                    a.Location_Price,
                    a.Location_IS_Reserved,
                    a.imgNumber
                }).ToList();
            LocationsDataGrid.Columns[1].Visible = false;
            LocationsDataGrid.Columns[5].Visible = false;
            LocationsDataGrid.Columns[3].Visible = false;
            LocationsDataGrid.Columns[4].Visible = false;
            //Setting Values to Text boxes.
            City.Text = LocationsDataGrid.Rows[0].Cells[0].Value.ToString();
            textBox3.Text = LocationsDataGrid.Rows[0].Cells[1].Value.ToString();
            Address.Text = LocationsDataGrid.Rows[0].Cells[3].Value.ToString() + " " +
                LocationsDataGrid.Rows[0].Cells[2].Value.ToString();

            price.Text = LocationsDataGrid.Rows[0].Cells[4].Value.ToString();
            textBox2.Text = LocationsDataGrid.Rows[0].Cells[5].Value.ToString();
            int ResDays = int.Parse(textBox2.Text);
            if (ResDays > 0)
            {
                textBox1.Text = "Not Avaliable till " + DateTime.Today.AddDays(ResDays).ToString("MMMM dd, yyyy");
                ReserveBTN.Enabled = false;
                numericUpDown1.Enabled = false;
            }
            else
            { textBox1.Text = "Avaliable "; }
            string wordsToBeSearched = @"\bin\Debug";
            int wordIndex = Application.StartupPath.IndexOf(wordsToBeSearched);
            if (wordIndex != -1)
            {
                string NewAppDir = Application.StartupPath.Substring(0, wordIndex);
            pictureBox1.ImageLocation = NewAppDir +
               @"\Images\" + LocationsDataGrid.Rows[0].Cells[6].Value + ".jpg";
            }
            LocationsDataGrid.Columns[1].Width = 150;
            LocationsDataGrid.Columns[2].Width = 150;

        }

        private void LocationsDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = e.RowIndex;

            if (rowIndex >= 0)
            {
                DataGridViewRow Row = LocationsDataGrid.Rows[rowIndex];
                Address.Text = Row.Cells[3].Value.ToString() + " " + Row.Cells[2].Value.ToString();
                string wordsToBeSearched = @"\bin\Debug";
                int wordIndex = Application.StartupPath.IndexOf(wordsToBeSearched);
                if (wordIndex != -1)
                {
                    string NewAppDir = Application.StartupPath.Substring(0, wordIndex);
                    pictureBox1.ImageLocation = NewAppDir +
                       @"\Images\" + Row.Cells[6].Value + ".jpg";
                }
                textBox3.Text = Row.Cells[1].Value.ToString();
                textBox1.Text = Row.Cells[5].Value.ToString();
                City.Text = Row.Cells[0].Value.ToString();
                int ResDays = int.Parse(textBox1.Text);


                if (ResDays > 0)
                {
                    textBox1.Text = "Not Avaliable till " + DateTime.Today.AddDays(ResDays).ToString("MMMM dd, yyyy");

                    ReserveBTN.Enabled = false;
                    numericUpDown1.Enabled = false;
                }
                else
                {
                    textBox1.Text = "Avaliable ";
                    ReserveBTN.Enabled = true;
                    numericUpDown1.Enabled = true;
                }
            }
        }

        private void ReserveBTN_Click(object sender, EventArgs e)
        {
            int check = int.Parse(numericUpDown1.Value.ToString());
            DialogResult dialogResult = MessageBox.Show(" You Want to Reserve  " + check.ToString() + " Days", "Last Check !", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int res = int.Parse(textBox3.Text.ToString());
                var m = (from r in db.Locations
                         where r.LocationID == res
                         select r).First();
                m.Location_IS_Reserved = int.Parse(numericUpDown1.Value.ToString());

                db.SaveChanges();
                LoadLocations();
                numericUpDown1.Value = 0;

            }
            else if (dialogResult == DialogResult.No)
            {
                LoadLocations();
                numericUpDown1.Value = 0;
            }
        }

        private void NewHostBtn_Click(object sender, EventArgs e)
        {

            NewHost newHost = new NewHost(this);
            newHost.ShowDialog();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Close();
            f.ResetText();

        }

        private void AMES_Load(object sender, EventArgs e)
        {
        }
        private void AMES_Load_1(object sender, EventArgs e)
        {

                AirbnbDBEntities1 db = new AirbnbDBEntities1();
                Guest g = new Guest();
                var m = (from r in db.Guests
                         where r.Guest_ID == 2
                         select r).First();

                if (m.Guest_Name == "1")
                {
                    NewHostBtn.Enabled = false;
                    NewHostBtn.Visible = false;
                }
                else
                {
                    NewHostBtn.Enabled = true;
                    NewHostBtn.Visible = true;
                }
                m.Guest_Name = "2";
                db.SaveChanges();
            }
        }
}
