using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Tanks
{
    partial class View : UserControl
    {
        Model model;
        public View(Model model)
        {
            InitializeComponent();
            this.model = model;
        }

        void Draw(PaintEventArgs e)
        {
            DrawWall(e);
            DrawTanks(e);
            DrawHearts(e);
            DrawPackMan(e);
            DrawProjectile(e);

            if (model.gameStatus != GameStatus.play)
                return;

            Thread.Sleep(model.SpeedGame);
            Invalidate();
        }

        private void DrawProjectile(PaintEventArgs e)
        {
            e.Graphics.DrawImage(model.Projectile.Img, new Point(model.Projectile.X, model.Projectile.Y)); 
        }

        private void DrawPackMan(PaintEventArgs e)
        {
           e.Graphics.DrawImage(model.PackMan.CurrentImg, new Point(model.PackMan.X, model.PackMan.Y));
        }

        private void DrawHearts(PaintEventArgs e)
        {
            for(int i = 0; i < model.Hearts.Count; i++)
            {
                e.Graphics.DrawImage(model.Hearts[i].Img, new Point(model.Hearts[i].X, model.Hearts[i].Y));
            }
        }

        private void DrawTanks(PaintEventArgs e)
        {
            for(int i = 0; i < model.Tanks.Count; i++)
            {
                if (i == 0)
                {
                    e.Graphics.DrawImage(((Hunter)model.Tanks[i]).Img, new Point(model.Tanks[i].X, model.Tanks[i].Y));
                }
                else
                    e.Graphics.DrawImage(model.Tanks[i].Img, new Point(model.Tanks[i].X, model.Tanks[i].Y));
            }
            
        }

        private void DrawWall(PaintEventArgs e)
        {
           for (int x = 45; x < 45*12; x+=90)
                for (int y = 45; y < 45*12; y+=90)
                    e.Graphics.DrawImage(model.wall.Img, new Point(x, y));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Draw(e);
        }
    }
}
