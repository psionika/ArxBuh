using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Soap;

namespace ArxBuhUpdater
{
    static class ArxBuhSettings
    {
        public static bool BackupEnable = true;
        public static string BackupDir = @"Backup";
        public static decimal BackupCounter = 10;

        public static bool EncryptEnable;
        public static string EncryptPassword = "";

        public static string UpdatePath = @"http://arxbuh.itchita.ru/arxbuh.xml";
        public static bool UpdateEnabled = true;
    }

    static class ArxBuhSettingAction
    {
        public static void WriteXml()
        {
            ArxBuhSettings.EncryptPassword = "";

            var static_class = typeof(ArxBuhSettings);
            var filename = "settings.xml";
            {
                var fields = static_class.GetFields(BindingFlags.Static | BindingFlags.Public);

                var a = new object[fields.Length, 2];
                var i = 0;
                foreach (var field in fields)
                {
                    a[i, 0] = field.Name;
                    a[i, 1] = field.GetValue(null);
                    i++;
                }
                Stream f = File.Open(filename, FileMode.Create);
                var formatter = new SoapFormatter();
                formatter.Serialize(f, a);
                f.Close();

            }
        }

        public static void ReadXml()
        {
            var static_class = typeof(ArxBuhSettings);
            var filename = "settings.xml";
            {
                var fields = static_class.GetFields(BindingFlags.Static | BindingFlags.Public);
                Stream f = File.Open(filename, FileMode.Open);
                var formatter = new SoapFormatter();
                var a = formatter.Deserialize(f) as object[,];
                f.Close();
                if (a != null && a.GetLength(0) != fields.Length) return;
                var i = 0;
                foreach (var field in fields)
                {
                    if (a != null && field.Name == (a[i, 0] as string))
                    {
                        if (a[i, 1] != null)
                            field.SetValue(null, a[i, 1]);
                    }
                    i++;
                }
            }
        }
    }

}
