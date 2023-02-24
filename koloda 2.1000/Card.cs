using System.Drawing;
namespace koloda_2._1000
{
    public partial class Form1
    {
        public class Card
        {
            private string face;
            private string suit;
            public int value; 
            public Card(string cardFace, string cardSuit)
            {
                face = cardFace;
                suit = cardSuit;
                bool success = int.TryParse(cardFace, out value);
                if (!success)
                {
                    switch (cardFace)
                    {
                        case "T":
                            value = 11;
                            break;
                        case "K":
                            value = 4;
                            break;
                        case "Q":
                            value = 3;
                            break;
                        case "J":
                            value = 2;
                            break;
                    }
                }
            }
            public override string ToString()
            {
                return face + suit;
            }
            public Image FromFile()
            {
                string fileName = face + suit + ".png";
                Image image = Image.FromFile(fileName);
                return image;
            }
            public string getFace()
            {
                return face;
            }
            public string getSuit()
            {
                return suit;
            }
        }
    }
}