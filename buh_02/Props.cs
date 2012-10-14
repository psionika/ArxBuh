using System;
using System.Xml.Serialization;
using System.IO;

namespace buh_02
{
    //Класс определяющий какие настройки есть в программе
    public class PropsFields
    {
        //Путь до файла настроек
        public String XMLFileName = Environment.CurrentDirectory + "\\settings.xml";

        //Чтобы добавить настройку в программу просто добавьте суда строку вида - 
        //public ТИП ИМЯ_ПЕРЕМЕННОЙ = значение_переменной_по_умолчанию;
        public String TextValue = @"Testing XML File Settings";

        public System.Drawing.Point Location = new System.Drawing.Point(50, 50);
        public System.Drawing.Size FormSize = new System.Drawing.Size(900, 550);
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
            TextWriter writer = new StreamWriter(Fields.XMLFileName);
            ser.Serialize(writer, Fields);
            writer.Close();
        }

        //Чтение настроек из файла
        public void ReadXml()
        {
            if (File.Exists(Fields.XMLFileName))
            {
                XmlSerializer ser = new XmlSerializer(typeof(PropsFields));
                TextReader reader = new StreamReader(Fields.XMLFileName);
                Fields = ser.Deserialize(reader) as PropsFields;
                reader.Close();
            }
            else { }
        }
    }
}
