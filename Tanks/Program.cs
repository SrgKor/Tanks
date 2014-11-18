using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Tanks
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        // Предусмотрена возможность вызова приложения со следующими параметрами (кол-во - [0, 4]; тип - int): 
        // 1) размер игрового поля
        // 2) кол-во танков
        // 3) кол-во яблок
        // 4) скорость игры
        static void Main(string[] args)
        {
            Controller_MainForm controller; // объектная ссылка на главную форму (реализующую логику "Controller" модели MVC)

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // В зависимости от количества переданных приложению параметров, создается объект Controller_MainForm
            // с использованием соответствующего конструктора.
            switch (args.Length)
            {
                case 0 : controller = new Controller_MainForm(); break;
                case 1: controller = new Controller_MainForm(Convert.ToInt32(args[0])); break;
                case 2: controller = new Controller_MainForm(Convert.ToInt32(args[0]), Convert.ToInt32(args[1])); break;
                case 3: controller = new Controller_MainForm(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]),Convert.ToInt32(args[2])); break;
                case 4: controller = new Controller_MainForm(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]),Convert.ToInt32(args[2]),Convert.ToInt32(args[3])); break;
                default: controller = new Controller_MainForm(); break; // по дефолту используется конструктор без параметров
            }
            
            Application.Run(controller);
        }
    }
}
