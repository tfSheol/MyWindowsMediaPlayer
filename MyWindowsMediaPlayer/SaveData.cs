using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyWindowsMediaPlayer
{
    class SaveData
    {
        public static void Save<T>(String path, T data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream stream = new FileStream(path, FileMode.Create);
            serializer.Serialize(stream, data);
            stream.Close();
            /*
            int i = -1;
            XmlWriterSettings ws = new XmlWriterSettings();
            ws.Indent = true;
            XmlWriter writer = XmlWriter.Create(path, ws);

            writer.WriteStartDocument();
            writer.WriteStartElement("Playlist");
            while(++i < data.getList().Count())
            {
                writer.WriteStartElement("Track");
                writer.WriteElementString("path", data.getList().ElementAt(i));
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();*/
        }

        public static T Load<T>(String path)
        {
            T data;
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            data = (T)formatter.Deserialize(stream);
            stream.Close();
            return data;
            /*
            XmlReader reader = XmlReader.Create(path);
            T data = new Playlist();

            while (reader.Read())
            {
                data.addAMusic(reader.Value);
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals("Track"))
                            playlist.addAMusic(reader.Value);
                        break;
                }
            }
            reader.Close();
            return (data);*/
        }
    }
}
