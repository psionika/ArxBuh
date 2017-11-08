using System;
using System.Windows.Forms;

namespace ArxBuh
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!SingleInstance.IsFirstRun)
            {
                SingleInstance.ShowWindow("ArxBuh");
                return;
            }

            using (var formMain = new FormMain())
            {
                Application.Run(formMain);
            }
        }
    }
}
