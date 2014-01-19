using System;
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

    public class PropsFields
    {
        public System.Drawing.Point Location = new System.Drawing.Point(50, 50);
        public System.Drawing.Size FormSize = new System.Drawing.Size(900, 550);

        public Boolean BackupEnable = true;
        public String BackupDir = @"Backup";
        public Decimal BackupCounter = 10;

        public Boolean EncryptEnable = false;

        public String UpdatePath = @"http://arxbuh.itchita.ru/arxbuh.xml";
        public Boolean UpdateEnabled = true;
    }

    public class Props
    {
        public PropsFields Fields;

        public Props()
        {
            Fields = new PropsFields();
        }

        //Запист настроек в файл
        public void WriteXml()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
            TextWriter writer = new StreamWriter(System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("ArxBuhUpdater.exe", "") + "settings.xml");
            ser.Serialize(writer, Fields);
            writer.Close();
        }

        //Чтение настроек из файла
        public void ReadXml()
        {
            if (File.Exists(System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("ArxBuhUpdater.exe", "") + "ArxBuhUpdater.xml"))
            {
                XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
                TextReader reader = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("ArxBuhUpdater.exe", "") + "settings.xml");
                Fields = ser.Deserialize(reader) as PropsFields;
                reader.Close();
            }
            else { }
        }
    }

}
