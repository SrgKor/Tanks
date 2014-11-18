using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    class PackManImg
    {
        Image[] up = new Image[] 
        {
            Properties.Resources.PackManUp0,
            Properties.Resources.PackManUp1,
            Properties.Resources.PackManUp2,
            Properties.Resources.PackManUp3,
            Properties.Resources.PackmanUp4
        };

        Image[] down = new Image[] 
        {
            Properties.Resources.PackManDown0,
            Properties.Resources.PackManDown1,
            Properties.Resources.PackManDown2,
            Properties.Resources.PackManDown3,
            Properties.Resources.PackmanDown4
        };

        Image[] left = new Image[] 
        {
            Properties.Resources.PackManLeft0,
            Properties.Resources.PackManLeft1,
            Properties.Resources.PackManLeft2,
            Properties.Resources.PackManLeft3,
            Properties.Resources.PackmanLeft4
        };

        Image[] right = new Image[] 
        {
            Properties.Resources.PackManRight0,
            Properties.Resources.PackManRight1,
            Properties.Resources.PackManRight2,
            Properties.Resources.PackManRight3,
            Properties.Resources.PackmanRight4
        };

        public Image[] Up
        {
            get { return up; }
        }
        public Image[] Down
        {
            get { return down; }
        }
        public Image[] Left
        {
            get { return left; }
        }
        public Image[] Right
        {
            get { return right; }
        }

    }
}
