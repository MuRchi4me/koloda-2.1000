using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace koloda_2._1000
{
    public partial class Form1 : Form
    {
        public class Deck
        {
            private Card[] deck;
            private int currentCard;
            private const int NUMBER_OF_CARDS = 52;
            private Random ranNum;
            public Deck()
            {
                string[] faces = { "T", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
                string[] suits = { "♥", "♣", "♦", "♠" };
                deck = new Card[NUMBER_OF_CARDS];
                currentCard = 0;
                ranNum = new Random();
                for (int count = 0; count < deck.Length; count++)
                    deck[count] = new Card(faces[count % 13], suits[count / 13]);
            }
            public void Shuffle()
            {
                currentCard = 0;
                for (int first = 0; first < deck.Length; first++)
                {
                    int second = ranNum.Next(NUMBER_OF_CARDS);
                    Card temp = deck[first];
                    deck[first] = deck[second];
                    deck[second] = temp;
                }
            }
            public Card DealCard()
            {
                if (currentCard < deck.Length)
                    return deck[currentCard++];
                else
                    return null;
            }
            public void dealMyCart(string[] cardsLoad)
            {
                int kk = 0;
                foreach (string cardl in cardsLoad)
                {
                    for (int iii = 0; iii < deck.Length; iii++)
                    {
                        if (cardl[0] == deck[iii].getFace()[0] && cardl[1] == deck[iii].getSuit()[0])
                        {
                            Card item = deck[iii];
                            deck[iii] = deck[kk];
                            deck[kk] = item;
                        }
                    }
                    kk++;
                }
            }
        }
        Deck deck = new Deck();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            deck.Shuffle();
        }
        int y = 290, x = 34;
        int y2 = 290, x2 = 385;
        PictureBox[] Pic = new PictureBox[26];
        PictureBox[] Pic2 = new PictureBox[26];
        int MyScore; int EnemScore;
        string winner;
        int g = 1;  int k = 1;
        int You = 0; int Enem = 0; string MyDeck; string EnemyDeck;
        private void OpenFileMenuItem_Click(object sender, EventArgs e)
        {
            string text;
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Text = openFileDialog.FileName;
                string FileName = openFileDialog.FileName;
                using (StreamReader reader = new StreamReader(FileName))
                {
                    text = reader.ReadToEnd();
                }
                resetBoard();
                var lst = Console.ReadLine()?.Split().Select(int.Parse).ToList();
                string[] s_arr = text.Split('~');
                You = int.Parse(s_arr[1]);
                Enem = int.Parse(s_arr[2]);
                label7.Text = Convert.ToString(You);
                label8.Text = Convert.ToString(Enem);

                string[] mynewarr = s_arr[0].Split(' ').Take(s_arr[0].Split(' ').Count() - 1).ToArray();

                deck.dealMyCart(mynewarr);
                foreach (string s in mynewarr)
                    button1_Click(null, new EventArgs());
            }
        }
        static List<string> SplitString(string str)
        {
            List<string> list = new List<string>();
            int i = 0;
            while (i < str.Length - 1)
            {
                list.Add(str.Substring(i, 2));
                i += 2;
            }
            return list;
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                using (StreamWriter writer = new StreamWriter(FileName, false))
                {
                    writer.WriteLineAsync(MyDeck +"~"+ You + "~"+ Enem);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            label2.Visible = true;
            int z = 139;
            Card card = deck.DealCard();
            string fileName = card.getFace() + card.getSuit() + ".png";
            Image image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + fileName);
            Pic[k] = new PictureBox();
            Pic[k].Location = new Point(y, x);
            Pic[k].Name = "pic" + k;
            Pic[k].SizeMode = PictureBoxSizeMode.Zoom;
            Pic[k].BackColor = Color.SeaGreen;
            Pic[k].Size = new Size(140, 188);
            Pic[k].Image = image;
            this.Controls.Add(Pic[k]);
            this.Controls[this.Controls.Count - 1].BringToFront();
            label1.Text = card.ToString();
            y += z;
            k++;
            MyDeck += card.getFace() + card.getSuit() + " ";
            label11.Text = MyDeck;
            MyScore += card.value;
            label2.Text = Convert.ToString(MyScore);
            if (MyScore > 21)
            {
                button1.Enabled = false;
            }
        }
        private void resetBoard()
        {
            button2.Enabled = true;
            label1.Text = null;
            button1.Enabled = true;
            winner = null;
            MyScore = 0;
            EnemScore = 0;
            y2 = 290; x2 = 385;
            y = 290; x = 34;
            deck.Shuffle();
            for (int i = 1; i < k; i++)
            {
                Pic[i].Dispose();
            }
            k = 1;
            for (int i2 = 1; i2 < g; i2++)
            {
                Pic2[i2].Dispose();
            }
            g = 1;
            label11.Text = null;
            MyDeck = null;
            label12.Text = null;
            EnemyDeck = null;
            label3.Visible = false;
            label2.Visible = false;
            label7.Text = Convert.ToString(You);
            label8.Text = Convert.ToString(Enem);
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            label3.Visible = true;
            while (EnemScore <= 17 || (EnemScore < MyScore && MyScore <= 21))
            {
                int z = 134;
                Card card = deck.DealCard();
                string fileName = card.getFace() + card.getSuit() + ".png";
                Image image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + fileName);
                Pic2[g] = new PictureBox();
                Pic2[g].Location = new Point(y2, x2);
                Pic2[g].Name = "pic2" + g;
                Pic2[g].SizeMode = PictureBoxSizeMode.Zoom;
                Pic2[g].BackColor = Color.SeaGreen;
                Pic2[g].Size = new Size(140, 188);
                Pic2[g].Image = image;
                this.Controls.Add(Pic2[g]);
                this.Controls[this.Controls.Count - 1].BringToFront();
                label1.Text = card.ToString();
                y2 += z;
                g++;
                EnemScore += card.value;
                EnemyDeck += card.getFace() + card.getSuit() + " ";
                label12.Text = EnemyDeck;
                label3.Text = Convert.ToString(EnemScore);
                await Task.Delay(900);
                if (EnemScore > MyScore || MyScore > 21)
                {
                    break;
                }
            }
            if (MyScore > EnemScore && EnemScore <= 21 && MyScore <= 21)
            {
                winner = "Выйгрыш!!! \nУ вас больше очков";
                You++;
            }
            else if (MyScore <= 21 && EnemScore > 21)
            {
                winner = "Выйгрыш!!! \nУ компьютера перебор";
                You++;
            }
            else if (MyScore > 21 && EnemScore <= 21)
            {
                winner = "Проигрыш!!! \n У вас перебор";
                Enem++;
            }
            else if (MyScore < EnemScore && EnemScore <= 21 && MyScore <= 21)
            {
                winner = "Проигрыш!!! \n У компьютера больше очков";
                Enem++;
            }
            else if (MyScore > 21 && EnemScore > 21)
            {
                winner = "Ничья!!! \n У обоих перебор";
            }
            else if (MyScore == EnemScore && MyScore <= 21 && EnemScore <= 21)
            {
                winner = "Ничья!!! \n У обоих одинаковое количество очков";
            }
            if (!String.IsNullOrEmpty(winner))
            {
                DialogResult dialogResult = MessageBox.Show($" {winner}!", "Итог", MessageBoxButtons.OK);

                if (dialogResult == DialogResult.OK)
                {
                    resetBoard();
                }
            }
        }
    }
}