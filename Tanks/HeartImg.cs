using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    class HeartImg  // Изображение сердечка на игровом поле
    {
        Image img = Properties.Resources.apple;
           
        public Image Img
        {
            get { return img; }
           
        }
    }
}
