﻿using System;
using System.Xml.Serialization;
using System.IO;

namespace ArxBuhUpdater
{
    public static class Updater
    {
        internal static String ChangeLogURL;

        internal static String DownloadURL;

        internal static String AppTitle = "ArxBuh";

        internal static Version NewVersion;

        internal static Version InstalledVersion;

        internal static bool IsWinFormsApplication;

        internal static bool StartExe = true;
    }

    public class Props
    {
        public void WriteXml()
        {
            ArxBuhSettingAction.WriteXml();
        }

        public void ReadXml()
        {
            ArxBuhSettingAction.ReadXml();
        }
    }

}
