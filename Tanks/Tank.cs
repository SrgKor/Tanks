using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Tanks 
{
    // Вражеская сущность
    class Tank : IRun, ITurn, ITransparent, IPicture, IUTurn
    {
        public Tank(int sizeField, int xInit, int yInit)    // Конструктор
        {
            img = tankImg.ImgRight;
            r = new Random();

            this.sizeField = sizeField;
            this.X = xInit;
            this.Y = yInit;
            //this.anotherTanks = anotherTanks;
        }

        private TankImg tankImg = new TankImg();
        protected Image img;

        public Image Img
        {
            get { return img; }
        }

        //List<Tank> anotherTanks;
        protected static Random r;

        protected int sizeField;  // размер поля



        protected int x;  // Координаты танка
        protected int y;

        protected int direct_x;
        protected int direct_y;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public int Direct_x
        {
            get { return direct_x; }
            set
            {
                if (value == 0 || value == -1 || value == 1)
                    direct_x = value;
                else
                    direct_x = 0;
            }
        }
        public int Direct_y
        {
            get { return direct_y; }
            set
            {
                if (value == 0 || value == -1 || value == 1)
                    direct_y = value;
                else
                    direct_y = 0;
            }
        }

        private void PutImage()   // Изменение картинки в зависимости от направления движения
        {
            if (direct_x == 1)
                img = tankImg.ImgRight;
            if (direct_x == -1)
                img = tankImg.ImgLeft;
            if (direct_y == 1)
                img = tankImg.ImgDown;
            if (direct_y == -1)
                img = tankImg.ImgUp;
        }

        public void Run()
        {
            X += Direct_x;
            Y += Direct_y;
            Transparent();
            if ((Math.IEEERemainder(x, 90) == 0) && (Math.IEEERemainder(y, 90) == 0))
                Turn();
        }

        public void Turn()
        {
                // произвольное движение по y или по x
                do
                {
                    Direct_y = r.Next(-1, 2);
                    Direct_x = r.Next(-1, 2);
                    if ( (Direct_x == 0 && Direct_y != 0) || ((Direct_x != 0) && (Direct_y == 0) ))
                        break;
                }
                while (true);
                PutImage();

                /*
                if (r.Next(5000) < 2500)
                {
                    if (Direct_y == 0)
                    {
                        Direct_x = 0;
                        while (Direct_y == 0)
                            Direct_y = r.Next(-1, 2);
                    }
                }
                else
                {
                    if (Direct_x == 0)
                    {
                        Direct_y = 0;
                            while (Direct_x == 0)
                                Direct_x = r.Next(-1,2);
                    }

                }
               */
        }

        public void Transparent()
        {
            if (x == -1)
                x = sizeField - 46;
            if (x == sizeField - 44)
                x = 1;

            if (y == -1)
                y = sizeField - 46;
            if (y == sizeField - 44)
                y = 1;
        }

        

        public void UTurn() // Разворот на 180'
        {
            if (direct_x == 0 && direct_y != 0) // если движется по y
            {
                if (direct_y == 1)
                    direct_y = -1;
                else
                    direct_y = 1;
            }
            else if (direct_y == 0 && direct_x != 0)// если движется по x
            {
                if (direct_x == 1)
                    direct_x = -1;
                else
                    direct_x = 1;
            }
            /*
            Direct_x = Direct_x * -1;
            Direct_y = Direct_y * -1;*/
            PutImage();
        }


        internal void Die()
        {
            X = -50;
            Y = -50;
            Direct_x = 0;
            Direct_y = 0;
        }
    }
}
