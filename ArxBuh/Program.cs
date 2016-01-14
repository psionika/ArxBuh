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

            if (!SingleInstance.IsFirstRun)
            {
                SingleInstance.ShowWindow("ArxBuh");
                return;
            }

            using (var form_Main = new Form_Main())
            {
                Application.Run(form_Main);
            }
        }
    }
}
