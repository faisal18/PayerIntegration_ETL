using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml.Linq;
using System.Xml;
using System.Data;
using System.Xml.Serialization;

namespace PayerIntegration_ETL.Helpers
{
    class XMLHelper
    {
        public static string split_xml(string filename)
        {
            string message;
            try
            {
                var xdoc = XDocument.Load(filename);
                var xmls = xdoc.Root.Elements().ToArray();

                for (int i = 0; i < xmls.Length; i++)
                {
                    using (var file = File.CreateText(string.Format(@"C:\tmp\PR\Error\xml{0}.xml", i + 1)))
                    {
                        //File.WriteAllText(@"C:\Users\faisal\Desktop\{0}.xml",xmls[i].ToString());
                        file.Write(xmls[i].ToString());
                    }
                }
                message = "Success";
                return message;
            }

            catch (Exception ex)
            {
                Logger.Error("   Split XML ---" + ex.Message.ToString());
                return null;
            }
        }

        public static string XmlTextReader (string filename)
        {
            try
            {
                StringBuilder XMLData = new StringBuilder();
                
                using (XmlTextReader xmlReader = new XmlTextReader(filename))
                {
                    while (xmlReader.Read())
                    {

                        switch (xmlReader.NodeType)
                        {
                           
                            case XmlNodeType.Element: // The node is an element.
                                XMLData.Append("<" + xmlReader.Name);
                                XMLData.Append(">");
                                break;
                            case XmlNodeType.Text: //Display the text in each element.
                                XMLData.Append(xmlReader.Value);



                                break;
                            case XmlNodeType.EndElement: //Display the end of the element.
                                XMLData.Append("</" + xmlReader.Name);
                                XMLData.Append(">");
                                break;
                        }

                       

                    }
                    Logger.Info(XMLData);
                }
                return XMLData.ToString();
            }
            catch (Exception ex)
            {

                Logger.Error(" XML Loader" + ex);
                return null;
            }

        }

        public static void DataSettoXmlTextWriter(System.Data.DataSet  DStoEx)
        {
            try
            {
                string yo =  ToXml(DStoEx);
                DStoEx.WriteXml(@"C:\tmp\pr\string" + DateTime.Now.ToString("yyyyMMddhhmmss") +".xml");
            }
            catch (Exception ex)
            {

                Logger.Error(" XML Loader" + ex);
               /* return null*/;
            }

        }
        public static string ToXml(DataSet ds)
        {

            return "";
        }

    }
}
