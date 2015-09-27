using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;

namespace ArxBuhUpdater
{
    public partial class UpdateForm : Form
    {

        public UpdateForm()
        {
            ArxBuhSettingAction.ReadXml();

            if (!ArxBuhSettings.UpdateEnabled)
            {
                StartExe();
            }

            GetUpdateXML();

            InitializeComponent();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            Updater.InstalledVersion = AssemblyName.GetAssemblyName("ArxBuh.exe").Version;
            Updater.IsWinFormsApplication = Application.MessageLoop;

            if (Updater.NewVersion.CompareTo(Updater.InstalledVersion) == 1)
            {
                webBrowser1.DocumentText = GetChangeLogHTML();

                label7.Text = string.Format(label7.Text, Updater.NewVersion, Updater.InstalledVersion);
            }
            else
            {
                StartExe();
            }
        }

        string GetChangeLogHTML()
        {
            try
            {
                var req = WebRequest.Create(new Uri(Updater.ChangeLogURL));
                var resp = req.GetResponse();

                var str = new StreamReader(resp.GetResponseStream());
                var s = str.ReadToEnd();
                resp.Close();
                return s;
            }
            catch
            {
                return "";
            }
        }

        void GetUpdateXML()
        {
            var AppCastURL = ArxBuhSettings.UpdatePath;
            var webRequest = WebRequest.Create(AppCastURL);
            webRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

            WebResponse webResponse;

            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch (Exception)
            {
                StartExe();
                return;
            }

            var appCastStream = webResponse.GetResponseStream();

            var receivedAppCastDocument = new XmlDocument();

            if (appCastStream != null)
            {
                receivedAppCastDocument.Load(appCastStream);
            }

            var appCastItems = receivedAppCastDocument.SelectNodes("item");

            if (appCastItems != null)
                foreach (XmlNode item in appCastItems)
                {
                    var appCastVersion = item.SelectSingleNode("version");
                    if (appCastVersion != null)
                    {
                        var appVersion = appCastVersion.InnerText;
                        Updater.NewVersion = new Version(appVersion);
                    }
                    else
                        continue;

                    var appCastChangeLog = item.SelectSingleNode("changelog");

                    Updater.ChangeLogURL = appCastChangeLog != null ? appCastChangeLog.InnerText : "";

                    var appCastUrl = item.SelectSingleNode("url");

                    Updater.DownloadURL = appCastUrl != null ? appCastUrl.InnerText : "";
                }
        }

        void button3_Click(object sender, EventArgs e)
        {
            StartExe();
        }

        static void StartExe()
        {
            if (Updater.StartExe)
            {
                var processStartInfo = new ProcessStartInfo { FileName = "ArxBuh.exe", UseShellExecute = true };
                Process.Start(processStartInfo);
                if (Updater.IsWinFormsApplication)
                {
                    Updater.StartExe = false;
                    Application.Exit();
                }
                else
                {
                    Updater.StartExe = false;
                    Environment.Exit(0);
                }
            }

        }

        void button4_Click(object sender, EventArgs e)
        {
            ArxBuhSettings.UpdateEnabled = false;
            ArxBuhSettingAction.WriteXml();
            StartExe();
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StartExe();
        }

        void button2_Click(object sender, EventArgs e)
        {
            using (var downloadDialog = new FormDownloadUpdate(Updater.DownloadURL))
            {
                {
                    downloadDialog.ShowDialog();
                }
            }
        }
    }
}
