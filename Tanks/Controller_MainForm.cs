using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace Tanks
{
    delegate void CrossThreadWorkDelegate();  // делегат для кросспоточной работы 

    // Контроллер. Главная форма.
    public partial class Controller_MainForm : Form
    {
        bool isSound = true;
        
        View view;
        Model model;

        Thread modelPlayThread;
        
        // По спецификации приложение должно иметь возможность запуска с входными параметрами.
        // Конструктор должен вызываться с любым кол-вом параметров.
        public Controller_MainForm() : this (585) { }
        public Controller_MainForm(int sizeField) : this(sizeField, 5) { }
        public Controller_MainForm(int sizeField, int amountTanks) : this(sizeField, amountTanks, 5) { }
        public Controller_MainForm(int sizeField, int amountTanks, int amountApples) : this(sizeField, amountTanks, amountApples, 40) { }
        public Controller_MainForm(int sizeField, int amountTanks, int amountApples, int speedGame)
        {
            InitializeComponent();

            model = new Model(sizeField, amountTanks, amountApples, speedGame);
            view = new View(model);
            this.Controls.Add(view);

            model.StatusChangeEvent += new Del(StatusStripLabelChanger);

            sp = new SoundPlayer(Properties.Resources.Kalimba);
        }

        private void startStop_btn_Click(object sender, EventArgs e)
        {
            if (model.gameStatus == GameStatus.play)
            {
                StartStop.Image = Properties.Resources.PlayButton;
                modelPlayThread.Abort();
                model.gameStatus = GameStatus.stop; StatusStripLabelChanger();
            }
            else
            {
                StartStop.Focus();
                modelPlayThread = new Thread(model.Play);
                modelPlayThread.Start();
                model.gameStatus = GameStatus.play; StatusStripLabelChanger();

                StartStop.Image = Properties.Resources.PauseButton;
                view.Invalidate();
            }
        }

        private void Controller_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (modelPlayThread != null)
            {
                model.gameStatus = GameStatus.stop;
                modelPlayThread.Abort();
            }
            DialogResult dialogResult;
            dialogResult = MessageBox.Show("                   Завершить игру?", "PackMan",MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void ManipulatePackMan(object sender, KeyPressEventArgs e)
        {
            
            switch (e.KeyChar)
            {
                case 'I' : // хотим вверх
                    {
                        model.PackMan.NextDirect_y = -1;
                        model.PackMan.NextDirect_x = 0;
                    }
                    break;

                case 'K':   // вниз
                    {
                        model.PackMan.NextDirect_y = 1;
                        model.PackMan.NextDirect_x = 0;
                    }
                    break;

                case 'J':   // влево
                    {
                        
                        model.PackMan.NextDirect_y = 0;
                        model.PackMan.NextDirect_x = -1;
                    }
                    break;

                case 'L':  // вправо
                    
                    {
                        model.PackMan.NextDirect_y = 0;
                        model.PackMan.NextDirect_x = 1;
                    }
                    break;

                default:
                    {
                        model.Projectile.X = model.PackMan.X + 23;
                        model.Projectile.Y = model.PackMan.Y + 23;
                        model.Projectile.Direct_x = model.PackMan.Direct_x;
                        model.Projectile.Direct_y = model.PackMan.Direct_y;
                    }
                    break;

            }
            
        }

        private void StartStop_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData.ToString())
            {
                case "I": // хотим вверх
                    {
                        model.PackMan.NextDirect_y = -1;
                        model.PackMan.NextDirect_x = 0;
                    }
                    break;

                case "K":   // вниз
                    {
                        model.PackMan.NextDirect_y = 1;
                        model.PackMan.NextDirect_x = 0;
                    }
                    break;

                case "J":   // влево
                    {

                        model.PackMan.NextDirect_y = 0;
                        model.PackMan.NextDirect_x = -1;
                    }
                    break;

                case "L":  // вправо
                    {
                        model.PackMan.NextDirect_y = 0;
                        model.PackMan.NextDirect_x = 1;
                    }
                    break;

                default:
                    {
                        model.Projectile.X = model.PackMan.X + 23;
                        model.Projectile.Y = model.PackMan.Y + 23;
                        model.Projectile.Direct_x = model.PackMan.Direct_x;
                        model.Projectile.Direct_y = model.PackMan.Direct_y;
                    }
                    break;
            }

        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.NewGame();
            StartStop.Image = Properties.Resources.PlayButton;
            view.Refresh();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пакмен 1.0 \nРазработчик Сергей Королев\n JKLI - управление\n любая клавиша - выстрел", "PackMan");
        }

        private void soundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isSound = !isSound;
            if (isSound)
            {
                soundToolStripMenuItem.Image = global::Tanks.Properties.Resources.SoundOn;
               
            }
            else
            {
                soundToolStripMenuItem.Image = global::Tanks.Properties.Resources.NoSound;
              
            }
        }

        void StatusStripLabelChanger()  // Изменение панели статуса - выводит состояние игры.
        {
            Invoke(new CrossThreadWorkDelegate(SetValueToStatusStripLabel) );
        }

        private void SetValueToStatusStripLabel()
        {
            toolStripStatusLabel1.Text = model.gameStatus.ToString();
            if (isSound)
            {
                if (model.gameStatus == GameStatus.play)
                    sp.PlayLooping();
                else
                    sp.Stop();
            }
            
        }

        SoundPlayer sp;
    }
}
