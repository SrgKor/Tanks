using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    class TankImg
    {
        Image imgDown = Properties.Resources.monsterDown;
        Image imgUp = Properties.Resources.monsterUp;
        Image imgLeft = Properties.Resources.monsterLeft;
        Image imgRight = Properties.Resources.monsterRight;
        
        public Image ImgDown
        {
            get { return imgDown; }
            
        }
        public Image ImgUp
        {
            get { return imgUp; }
            
        }
        public Image ImgLeft
        {
            get { return imgLeft; }
            
        }
        public Image ImgRight
        {
            get { return imgRight; }
            
        }
    }
}
