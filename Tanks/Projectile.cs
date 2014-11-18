using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    // Снаряд
    class Projectile
    {
        private ProjectileImg projectileImg = new ProjectileImg();

        Image img;
        public Image Img
        {
            get { return img; }
        }
        int distance;
        #region Координаты

        int x = 0;
        int y = 0;

        int direct_x = 0;
        int direct_y = 0;

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

        #endregion

        public Projectile()
        {
            img = projectileImg.Up;
            DefaultSettings();
        }


        public void Run()
        {
            PutImage();
            if (Direct_y == 0 && Direct_x == 0)
                return;
            distance += 3;
            x += Direct_x * 4;
            y += Direct_y * 4;
            if (distance > 45 * 6)
                DefaultSettings();
        }

        public void DefaultSettings()
        {
            x = y = -10;
            Direct_x = Direct_y = 0;
            distance = 0;
        }

        private void PutImage()   // Изменение картинки в зависимости от направления движения
        {
            if (direct_x == 1)
                img = projectileImg.Right;
            if (direct_x == -1)
                img = projectileImg.Left;
            if (direct_y == 1)
                img = projectileImg.Down;
            if (direct_y == -1)
                img = projectileImg.Up;
        }
    }
}
