using System;
using System.ComponentModel;
using System.Net.Cache;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace ArxBuhUpdater
{
    public partial class FormDownloadUpdate : Form
    {
        readonly string _downloadURL;

        string _tempPath;

        public FormDownloadUpdate(string downloadURL)
        {
            InitializeComponent();

            _downloadURL = downloadURL;
        }

        void FormDownloadUpdate_Load(object sender, EventArgs e)
        {
            var webClient = new WebClient();

            var uri = new Uri(_downloadURL);

            _tempPath = string.Format(@"{0}{1}", System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("ArxBuhUpdater.exe", ""), GetFileName(_downloadURL));

            webClient.DownloadProgressChanged += OnDownloadProgressChanged;

            webClient.DownloadFileCompleted += OnDownloadComplete;

            webClient.DownloadFileAsync(uri, _tempPath);
        }

        void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        void OnDownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            Application.Exit();
        }

        static string GetFileName(string url)
        {
            var fileName = string.Empty;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            httpWebRequest.Method = "HEAD";
            httpWebRequest.AllowAutoRedirect = false;
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            if (httpWebResponse.StatusCode.Equals(HttpStatusCode.Redirect) || httpWebResponse.StatusCode.Equals(HttpStatusCode.Moved) || httpWebResponse.StatusCode.Equals(HttpStatusCode.MovedPermanently))
            {
                if (httpWebResponse.Headers["Location"] != null)
                {
                    var location = httpWebResponse.Headers["Location"];
                    fileName = GetFileName(location);
                    return fileName;
                }
            }
            if (httpWebResponse.Headers["content-disposition"] != null)
            {
                var contentDisposition = httpWebResponse.Headers["content-disposition"];
                if (!string.IsNullOrEmpty(contentDisposition))
                {
                    const string lookForFileName = "filename=";
                    var index = contentDisposition.IndexOf(lookForFileName, StringComparison.CurrentCultureIgnoreCase);
                    if (index >= 0)
                        fileName = contentDisposition.Substring(index + lookForFileName.Length);
                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Substring(1, fileName.Length - 2);
                    }
                }
            }
            if (string.IsNullOrEmpty(fileName))
            {
                var uri = new Uri(url);

                fileName = Path.GetFileName(uri.LocalPath);
            }
            return fileName;
        }

    }
}
