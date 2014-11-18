using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    class Hunter : Tank
    {
        HunterImg hunterImg = new HunterImg();


        public Hunter(int sizeField, int xInit, int yInit ) 
            : base(sizeField, xInit, yInit )
        {
            PutImage();
        
        }

        public void Turn(int packManX, int packManY)
        {
            if (X < packManX)
                Direct_x = 1;
            else if (X > packManX)
                Direct_x = -1;
            else
                Direct_x = 0;

            if (Y < packManY)
                Direct_y = 1;
            else if (Y > packManY)
                Direct_y = -1;
            else
                Direct_y = 0;

            if (r.Next(5000) > 2500)
                Direct_x = 0;
            else
                Direct_y = 0;



            PutImage();
        }

        new public void UTurn()
        {
            Direct_x = Direct_x * -1;
            Direct_y = Direct_y * -1;
            PutImage();
        }

        public void Run(int packManX, int packManY)
        {

            X += Direct_x;
            Y += Direct_y;
            Transparent();
            if ((Math.IEEERemainder(x, 90) == 0) && (Math.IEEERemainder(y, 90) == 0))
                Turn(packManX, packManY);
        }

        private void PutImage()   // Изменение картинки в зависимости от направления движения
        {
            img = hunterImg.Img;
        }
    }
}
