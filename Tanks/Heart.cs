using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    class Heart : IPicture
    {
        HeartImg heartImg = new HeartImg();
        Image img;
        public Image Img
        {
            get { return img; }
        }

        int x, y;
        public int X 
        { get {return x;} 
          set {x = value;} 
        }
        public int Y 
        { get {return y;}
          set { y = value; }
        }
        
        public Heart(int x, int y)
        {
            img = heartImg.Img;
            this.x = x;
            this.y = y;
        }
    }
}
