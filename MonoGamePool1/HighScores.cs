using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonoGamePool1
{
    public partial class HighScores : Form
    {
        public static int lastSort = 0;
        public static bool asc = true;

        public static List<HighScore> HighScoresList = new List<HighScore>();

        public HighScores()
        {
            InitializeComponent();
            
        }

        private void HighScores_Load(object sender, EventArgs e)
        {
            HighScoresSorting.GetHighScores();
            WriteLists(ref listBox1, ref listBox2, ref listBox3);
            //HighScoresList = HighScoresSorting.IdentifyCommand(0, true);
        }

        public void WriteLists(ref ListBox listBox1, ref ListBox listBox2, ref ListBox listBox3)
        {
            listBox1.ClearSelected();
            listBox2.ClearSelected();
            listBox3.ClearSelected();

            foreach(HighScore hs in HighScoresList)
            {
                listBox1.Items.Add(hs.UserName);
                listBox2.Items.Add(hs.BallsPotted);
                listBox3.Items.Add(hs.Time);
            }
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FlowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        /*private void Button2_Click_1(object sender, EventArgs e)
        {

        }*/

        /*private void Button2_Click_2(object sender, EventArgs e) //Username DESC
        {

        }*/

        private void FlowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e) //Username ASC
        {
            WriteLists(ref listBox1, ref listBox2, ref listBox3);
            if (lastSort == 0)
            {
                if (!asc)
                {
                    HighScoresSorting.ReverseList(ref HighScoresList);
                }
            }
            else
            {
                //Sort HighScores by Username
            }
            HighScoresSorting.IdentifyCommand(0, true, HighScoresList);
        }

        private void Button2_Click(object sender, EventArgs e) //Username DESC
        {
            if (lastSort == 0)
            {
                if (asc)
                {
                    HighScoresSorting.ReverseList(ref HighScoresList);
                }
            }
            else
            {
                //Sort HighScores by Username
                HighScoresSorting.ReverseList(ref HighScoresList);
            }
            HighScoresSorting.IdentifyCommand(0, false, HighScoresList);
        }

        private void Button3_Click(object sender, EventArgs e) //Balls Potted ASC
        {
            if (lastSort == 1)
            {
                if (!asc)
                {
                    HighScoresSorting.ReverseList(ref HighScoresList);
                }
            }
            else
            {
                //Sort HighScores by BallsPotted
            }
            HighScoresSorting.IdentifyCommand(1, true, HighScoresList);
        }

        private void Button5_Click(object sender, EventArgs e) //Balls Potted DESC
        {
            if (lastSort == 1)
            {
                if (asc)
                {
                    HighScoresSorting.ReverseList(ref HighScoresList);
                }
            }
            else
            {
                //Sort HighScores by BallsPotted
                HighScoresSorting.ReverseList(ref HighScoresList);
            }
            HighScoresSorting.IdentifyCommand(2, true, HighScoresList);
        }

        private void Button4_Click(object sender, EventArgs e) //Time Taken ASC
        {
            
            HighScoresSorting.IdentifyCommand(1, false, HighScoresList);
        }

        

        private void Button6_Click(object sender, EventArgs e) //Time Taken DESC
        {
            HighScoresSorting.IdentifyCommand(2, false, HighScoresList);
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
