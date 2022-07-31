using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace PublishXML.XmlCRUD
{
    public class XmlEdit
    {

        public string xml { get; set; }

        /// <summary>
        /// Metodun amaci yayinlanan xml url sini alip ozel barkod tanimlamasini saglamak icin kullanilir.
        /// </summary>
        /// <param name="urlAdress">duzenlenmesi icin cekilecek xml urlsi</param> 
        /// <param name="fileName">kaydedilecek dosya ismi dosya wwwroot/XMLFiles/dosyaismi. xml olarak kaydedilecek</param>
        public string SaveXml(string urlAdress,string fileName)
        {
            using (WebClient wc = new WebClient())
            {
                // xml = wc.DownloadString("http://convert.stockmount.com/xml/publish/28094/xmloutlet");
                xml = wc.DownloadString(urlAdress);
            }

            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            var basePath = Path.Combine(Environment.CurrentDirectory, @"wwwroot\XMLFiles\");
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            //var newFileName = string.Format("duzenlenmisVeri.xml");
            var newFileName = string.Format(fileName);

            doc.Save(basePath + newFileName + ".xml");


            XDocument docx = XDocument.Load(basePath + newFileName + ".xml");
            

            foreach (XElement product in docx.Element("Products").Elements("Product"))
            {
                product.Add(new XElement("uniqBarcode", product.Element("SmProductId").Value + "-2022"));
            }

            docx.Save(basePath + newFileName + ".xml");

            string xmlString = File.ReadAllText(basePath + newFileName + ".xml");
            return xmlString;
        }
    }
}
