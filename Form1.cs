using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1Lottery
{
    public partial class Form1 : Form
    {
        /*Klass som implementerar ett lotteri-program som styrs genom ett grafiskt gränssnitt.*/
        HashSet<int> userNums = new HashSet<int>();
        int numberOfDraws = 0;
        bool goodToGo = true;
        Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void PlayBtn_Click(object sender, EventArgs e)
        {
            /*Metod som anropas när användaren klickar på play.
             Den anropar i sin tur metoden validateInput() som kollar så att användarens
             input är giltig. Om den är giltig anropas runLottery(), som då kör
             själva lotterispelet.*/
            validateInput();
            if (goodToGo == false)
            {
                MessageBox.Show("No duplicate, missing or invalid numbers!");
                userNums.Clear();
                numberOfDraws = 0;
            }
            else runLottery();
        }
        private void validateInput()
        {
            /*Metod som anropas av PlayBtn_Click(). Den lägger till sju lotterisiffror
             i en hashset, lägger till ett nummer för antal dragning, och kollar så att
            de har giltiga värden (värda 1-35, är heltal, etc). Om allt är som det ska
            ges booleanen goodToGo värdet TRUE.*/
            goodToGo = true;
            try
            {
                userNums.Add(int.Parse(textBox1.Text));
                userNums.Add(int.Parse(textBox2.Text));
                userNums.Add(int.Parse(textBox3.Text));
                userNums.Add(int.Parse(textBox4.Text));
                userNums.Add(int.Parse(textBox5.Text));
                userNums.Add(int.Parse(textBox6.Text));
                userNums.Add(int.Parse(textBox7.Text));
                numberOfDraws = int.Parse(drawsTxtBox.Text);
            }
            catch (Exception)
            {
                goodToGo = false;
            }
            foreach (int i in userNums)
            {
                if (i < 1 || i > 35 || userNums.Count < 7 || numberOfDraws < 1)
                {
                    goodToGo = false;
                }
            }
        }
        private void runLottery()
        {
            /*Om boolean goodToGo är TRUE körs denna metod. Den drar sju slumpmässiga siffror,
             och någon av dessa siffror matchar någon av användarens siffror ökar värdet
             av variabeln match. I slutet av dragningen kalkyreas hur många matchar som skett.
             Detta visas sedan i GUIt.*/
            HashSet<int> lotteryNums = new HashSet<int>();
            int fivewin = 0;
            int sixwin = 0;
            int sevenwin = 0;
            fiveMatchTxtBox.Text = string.Empty;
            sixMatchTxtBox.Text = string.Empty;
            sevenMatchTxtBox.Text = string.Empty;
            for (int i = 0; i < numberOfDraws; i++)
            {
                
                int match = 0;
                while (lotteryNums.Count < 7) lotteryNums.Add(rand.Next(1, 35));
                foreach (int z in lotteryNums)
                {
                    foreach (int q in userNums) if (z == q) match++;
                }
                if (match == 5) fivewin++;
                else if (match == 6) sixwin++;
                else if (match == 7) sevenwin++;
                lotteryNums.Clear();
            }
            fiveMatchTxtBox.AppendText($"{fivewin}");
            sixMatchTxtBox.AppendText($"{sixwin}");
            sevenMatchTxtBox.AppendText($"{sevenwin}");
            userNums.Clear();
        }
    }
}
