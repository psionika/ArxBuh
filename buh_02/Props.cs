using System;
using System.Xml.Serialization;
using System.IO;


namespace buh_02
{
    //Класс определяющий какие настройки есть в программе
    public class PropsFields
    {
        //Путь до файла настроек
       

        //Чтобы добавить настройку в программу просто добавьте суда строку вида - 
        //public ТИП ИМЯ_ПЕРЕМЕННОЙ = значение_переменной_по_умолчанию;
      
        public System.Drawing.Point Location = new System.Drawing.Point(50, 50);
        public System.Drawing.Size FormSize = new System.Drawing.Size(900, 550);

        public Boolean BackupEnable = true;
        public String BackupDir = @"Backup";
        public Decimal BackupCounter = 10;

        public Boolean EncryptEnable = false;
    }

    //Класс работы с настройками
    public class Props
    {
        public PropsFields Fields;

        public Props()
        {
            Fields = new PropsFields();
        }

        //Запись настроек в файл
        public void WriteXml()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
            TextWriter writer = new StreamWriter(System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("ArxBuh.exe", "settings.xml"));
            ser.Serialize(writer, Fields);
            writer.Close();
        }

        //Чтение настроек из файла
        public void ReadXml()
        {
            if (File.Exists(System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("ArxBuh.exe", "settings.xml")))
            {
                XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
                TextReader reader = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("ArxBuh.exe", "settings.xml"));
                Fields = ser.Deserialize(reader) as PropsFields;
                reader.Close();
            }
            else { }
        }
    }
}
