using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace TestMysqlCDRAuto
{
    public class GetCdrThumb
    {
        public BitmapImage GetThumb(string filePath)
        {
            using (ZipArchive zipFile = new ZipArchive((new FileInfo(filePath)).Open(FileMode.Open)))
            {
                string xml = GetStringFromEntry("META-INF/container.xml", zipFile);
                int numPage = 1;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlNodeList nodes = doc.LastChild.FirstChild.ChildNodes;
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].Attributes["crl:file-kind"].InnerText == "page")
                    {
                        string pageName = nodes[i].Attributes["crl:caption"].InnerText;
                        pageName = pageName.Substring(pageName.IndexOf('[') + 1, pageName.IndexOf(']') - 2);
                        BitmapImage preview = GetBitmapFromEntry(string.Format("previews/page{0}.png", numPage), zipFile);
                        return preview;

                    }
                }
                return null;
            }
        }
        private BitmapImage GetBitmapFromEntry(string entry, ZipArchive zipFile)
        {
            try
            {
                ZipArchiveEntry thumbEntry = zipFile.GetEntry(entry);
                //Bitmap bitmap;
                using (Stream thumbStream = thumbEntry.Open())
                {
                    MemoryStream ms = new MemoryStream();
                    thumbStream.CopyTo(ms);
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    ms.Seek(0, SeekOrigin.Begin);
                    image.StreamSource = ms;
                    image.EndInit();

                    return image;

                }
                return null;
            }
            catch { return null; }
        }
        private string GetStringFromEntry(string entry, ZipArchive zipFile)
        {
            string text = "";
            ZipArchiveEntry textEntry = zipFile.GetEntry(entry);
            using (Stream stEntry = textEntry.Open())
            {
                using (StreamReader sr = new StreamReader(stEntry))
                {
                    text = sr.ReadToEnd();
                }
            }
            return text;
        }

    }
}
