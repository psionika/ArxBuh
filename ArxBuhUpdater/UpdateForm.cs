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
        Props props = new Props();

        public UpdateForm()
        {
            props.ReadXml();
            if (!props.Fields.UpdateEnabled)
            {
                StartExe();
            }

            GetUpdateXML();

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
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

        private string GetChangeLogHTML()
        {
            try
            {
                WebRequest req = WebRequest.Create(new Uri(Updater.ChangeLogURL));
                WebResponse resp = req.GetResponse();

                StreamReader str = new StreamReader(resp.GetResponseStream());
                string s = str.ReadToEnd();
                resp.Close();
                return s;
            }
            catch
            {
                return "";
            }
        }

        private void GetUpdateXML()
        {
            string AppCastURL = props.Fields.UpdatePath;
            WebRequest webRequest = WebRequest.Create(AppCastURL);
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

            Stream appCastStream = webResponse.GetResponseStream();

            var receivedAppCastDocument = new XmlDocument();

            if (appCastStream != null)
            {
                receivedAppCastDocument.Load(appCastStream);
            }

            XmlNodeList appCastItems = receivedAppCastDocument.SelectNodes("item");

            if (appCastItems != null)
                foreach (XmlNode item in appCastItems)
                {
                    XmlNode appCastVersion = item.SelectSingleNode("version");
                    if (appCastVersion != null)
                    {
                        String appVersion = appCastVersion.InnerText;
                        Updater.NewVersion = new Version(appVersion);
                    }
                    else
                        continue;

                    XmlNode appCastChangeLog = item.SelectSingleNode("changelog");

                    Updater.ChangeLogURL = appCastChangeLog != null ? appCastChangeLog.InnerText : "";

                    XmlNode appCastUrl = item.SelectSingleNode("url");

                    Updater.DownloadURL = appCastUrl != null ? appCastUrl.InnerText : "";
                }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            StartExe();
        }

        private void StartExe()
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

        private void button4_Click(object sender, EventArgs e)
        {
            props.Fields.UpdateEnabled = false;
            props.WriteXml();
            StartExe();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {   
            StartExe();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var downloadDialog = new FormDownloadUpdate(Updater.DownloadURL);

            try
            {
                downloadDialog.ShowDialog();
            }
            catch (System.Reflection.TargetInvocationException)
            {
            }
        }
    }
}
