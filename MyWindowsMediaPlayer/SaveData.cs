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
        public static void Save(String path, Playlist data)
        {
            int i = -1;
            XmlWriterSettings ws = new XmlWriterSettings();
            ws.Indent = true;
            XmlWriter writer = XmlWriter.Create(path, ws);

            writer.WriteStartDocument();
            while(++i < data.getList().Count())
            {
                writer.WriteStartElement("Track");
                writer.WriteElementString("path", data.getNextMusic());
                writer.WriteEndElement();
            }
            writer.WriteEndDocument();
            writer.Close();
        }

        public static Playlist Load(String path)
        {
            XmlReader reader = XmlReader.Create(path);
            Playlist playlist = new Playlist();

            while (reader.Read())
            {
                playlist.addAMusic(reader.Value);
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals("Track"))
                            playlist.addAMusic(reader.Value);
                        break;
                }
            }
            reader.Close();
            return (playlist);
        }
    }
}
