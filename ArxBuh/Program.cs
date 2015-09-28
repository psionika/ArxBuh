using System;
using System.Windows.Forms;

namespace ArxBuh
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new Form_Main());

            if (!SingleInstance.IsFirstRun)
            {
                //windowName текст заголовка окна, типа Form1
                SingleInstance.ShowWindow("ArxBuh"); // разворачивает окно и выводит на первый план
                //SingleInstance.SendArgs("windowName", args); //отправляет параметры командной строки, если нужно
                return;
            }

            using (var form_Main = new Form_Main())
            {
                Application.Run(form_Main);
            }
        }
    }
}
