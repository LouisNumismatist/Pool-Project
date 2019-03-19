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
            HighScoresList = HighScoresSorting.GetHighScores(); //josh tm
            WriteLists(ref UsernameBox, ref BallsPottedBox, ref TimeBox);
            //HighScoresList = HighScoresSorting.IdentifyCommand(0, true);
        }

        public void WriteLists(ref ListBox listBox1, ref ListBox listBox2, ref ListBox listBox3)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

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

        private void UsernameBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BallsPottedBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TimeBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void UsernameASC_Click(object sender, EventArgs e) //Username ASC
        {
            //sort by the usernanme
            //use OrderByDescending for descending
            HighScoresList = Algorithms.MergeGeneric(HighScoresList, new HighScoreNameComparer()).ToList();
            WriteLists(ref UsernameBox, ref BallsPottedBox, ref TimeBox);
        }

        private void UsernameDESC_Click(object sender, EventArgs e) //Username DESC
        {
            HighScoresList = Algorithms.MergeGeneric(HighScoresList, new HighScoreNameComparer()).ToList();
            HighScoresList.Reverse();
            WriteLists(ref UsernameBox, ref BallsPottedBox, ref TimeBox);
        }

        private void BallsPottedASC_Click(object sender, EventArgs e) //Balls Potted ASC
        {
            HighScoresList = Algorithms.QuickGeneric(HighScoresList, new HighScoreBallsPottedComparer()).ToList();
            WriteLists(ref UsernameBox, ref BallsPottedBox, ref TimeBox);
        }

        private void BallsPottedDESC_Click(object sender, EventArgs e) //Balls Potted DESC
        {
            HighScoresList = Algorithms.QuickGeneric(HighScoresList, new HighScoreBallsPottedComparer()).ToList();
            HighScoresList.Reverse();
            WriteLists(ref UsernameBox, ref BallsPottedBox, ref TimeBox);
        }

        private void TimeASC_Click(object sender, EventArgs e) //Time Taken ASC
        {
            HighScoresList = Algorithms.CocktailShakerGeneric(HighScoresList, new HighScoreTimeComparer()).ToList();
            WriteLists(ref UsernameBox, ref BallsPottedBox, ref TimeBox);
        }        

        private void TimeDESC_Click(object sender, EventArgs e) //Time Taken DESC
        {
            HighScoresList = Algorithms.CocktailShakerGeneric(HighScoresList, new HighScoreTimeComparer()).ToList();
            HighScoresList.Reverse();
            WriteLists(ref UsernameBox, ref BallsPottedBox, ref TimeBox);
        }

    }
}
