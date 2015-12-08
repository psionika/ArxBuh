using System;
using System.Windows.Forms;

using NLog;

namespace ArxBuh
{
    static class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
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
