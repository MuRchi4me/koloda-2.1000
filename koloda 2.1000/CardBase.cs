using System.Drawing;

namespace koloda_2._1000
{
    public class CardBase
    {
        public int value;
        private string face;
        private string suit;
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
        public override string ToString()
        {
            return face + suit;
        }
    }
}