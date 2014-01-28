using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Soap;

namespace buh_02
{
    static class ArxBuhSettings
    {
        public static Boolean BackupEnable = true;
        public static String BackupDir = @"Backup";
        public static Decimal BackupCounter = 10;

        public static Boolean EncryptEnable = false;
        public static String EncryptPassword = "";

        public static String UpdatePath = @"http://arxbuh.itchita.ru/arxbuh.xml";
        public static Boolean UpdateEnabled = true;
    }

    static class ArxBuhSettingAction
    {
        public static void WriteXml()
        {
            var static_class = typeof(ArxBuhSettings);
            const string filename = "settings.xml";

            try
            {
                var fields = static_class.GetFields(BindingFlags.Static | BindingFlags.Public);

                object[,] a = new object[fields.Length, 2];
                var i = 0;
                foreach (var field in fields)
                {
                    a[i, 0] = field.Name;
                    a[i, 1] = field.GetValue(null);
                    i++;
                }
                Stream f = File.Open(filename, FileMode.Create);
                SoapFormatter formatter = new SoapFormatter();
                formatter.Serialize(f, a);
                f.Close();
            }
            catch
            {
            }
        }

        public static void ReadXml()
        {
            var static_class = typeof(ArxBuhSettings);
            const string filename = "settings.xml";

            try
            {
                var fields = static_class.GetFields(BindingFlags.Static | BindingFlags.Public);
                Stream f = File.Open(filename, FileMode.Open);
                SoapFormatter formatter = new SoapFormatter();
                object[,] a = formatter.Deserialize(f) as object[,];
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
            catch
            {
            }
        }
    }
}
