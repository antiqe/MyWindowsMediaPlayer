using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyWindowsMediaPlayer
{
    class ManagePlaylist
    {
        public void WritePlaylist(string file, List<Tuple<string, string>> list)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(file, settings);
            writer.WriteStartDocument();
            writer.WriteComment("Playlist " + file);
            writer.WriteStartElement("Songs");

            foreach (Tuple<string, string> elem in list)
            {
                writer.WriteStartElement("Song");
                writer.WriteElementString("Name", elem.Item2);
                writer.WriteElementString("Path", elem.Item1);
                writer.WriteEndElement();

            }
            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();
        }

        public void ReadPlaylist(string file, List<Tuple<string, string>> list)
        {
            XmlReader reader = XmlReader.Create(file);
            string item1 = "";
            string item2 = "";

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Songs")
                {
                    while (reader.NodeType != XmlNodeType.EndElement)
                    {
                        reader.Read();
                        if (reader.Name == "Song")
                        {
                            while (reader.NodeType != XmlNodeType.EndElement)
                            {
                                reader.Read();
                                if (reader.Name == "Name")
                                {
                                    while (reader.NodeType != XmlNodeType.EndElement)
                                    {
                                        reader.Read();
                                        if (reader.NodeType == XmlNodeType.Text)
                                        {
                                            item1 = reader.Value;
                                        }
                                    }
                                    reader.Read();
                                }
                                if (reader.Name == "Path")
                                {
                                    while (reader.NodeType != XmlNodeType.EndElement)
                                    {
                                        reader.Read();
                                        if (reader.NodeType == XmlNodeType.Text)
                                        {
                                            item2 = reader.Value;
                                            list.Add(Tuple.Create<string, string>(item1, item2));
                                        }
                                    }
                                }
                            }
                            reader.Read();
                        }
                    }
                }
            }
        }
    }
}
