using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    class PackMan : IRun, ITurn, ITransparent
    {
        Point testPosition = new Point(13 * 45, 12 * 45);       // стартовая позиция вне игрового поля для тестирования
        PackManImg packmanImg = new PackManImg();
        Image[] img;
        Image currentImg;
        public Image CurrentImg
        {
            get { return currentImg; }
        }

        

        int sizeField;  // размер поля

        int x;  // Координаты танка
        int y;

        int direct_x;
        int direct_y;

        int nextDirect_x;
        int nextDirect_y;

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

        public int NextDirect_x
        {
            get { return nextDirect_x; }
            set { nextDirect_x = value; }
        }
        public int NextDirect_y
        {
            get { return nextDirect_y; }
            set { nextDirect_y = value; }
        }

        public PackMan(int sizeField)                // Конструктор
        {
            this.sizeField = sizeField;
            this.x = 6*45;
            this.y = 12*45;
            //this.x = testPosition.X;
            //this.y = testPosition.Y;
            this.direct_x = 0;
            this.direct_y = -1;
            this.nextDirect_x = 0;
            this.NextDirect_y = -1;

            PutImage();
            PutCurrentImage();
        }

        public void Run()
        {
            x += Direct_x;
            y += Direct_y;

            if ((Math.IEEERemainder(x, 90) == 0) && (Math.IEEERemainder(y, 90) == 0))
                Turn();
           
            PutCurrentImage();
            Transparent();
        }

        int k = 0;
        void PutCurrentImage()
        {
            currentImg = img[k];
            k++;
            if (k == 5)
                k = 0;
        }

        public void Turn()
        {
            Direct_x = NextDirect_x;
            Direct_y = NextDirect_y;
            PutImage();
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

        void PutImage()   // Изменение картинки в зависимости от направления движения
        {
            if (direct_x == 1)
                img = packmanImg.Right;
            if (direct_x == -1)
                img = packmanImg.Left;
            if (direct_y == 1)
                img = packmanImg.Down;
            if (direct_y == -1)
                img = packmanImg.Up;
        }
    }
}
