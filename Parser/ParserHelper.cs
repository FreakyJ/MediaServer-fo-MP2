using System;
using System.Text;
using System.Xml;
using MediaPortal.Extensions.MediaServer.DIDL;
using Peg.Base;

namespace MediaPortal.Extensions.MediaServer.Parser
{
    public static class ParserHelper
    {

        public static string PegNodeToXml(PegNode pn, string text)
        {
            var message = new StringBuilder(10000);
            var xml = XmlWriter.Create(new StringWriterWithEncoding(message, Encoding.UTF8),
                                    DEFAULT_XML_WRITER_SETTINGS);
            xml.WriteStartDocument();
            PegNodeToXmlRecurse(pn, text, xml);
            xml.WriteEndDocument();
            xml.Close();
            return message.ToString();
        }

        public static void PegNodeToXmlRecurse(PegNode pn, string text, XmlWriter xml)
        {
            var name = pn.id_ > 0 ? Enum.GetName(typeof (EUPnPContentDirectorySearch), pn.id_) : "Node";
            xml.WriteStartElement(name);
            xml.WriteAttributeString("match", pn.GetAsString(text));
            if (pn.child_ != null)
            {
                PegNodeToXmlRecurse(pn.child_, text, xml);
            }
            xml.WriteEndElement();
            if (pn.next_ != null)
            {
                PegNodeToXmlRecurse(pn.next_, text, xml);
            }
        }


        public static XmlWriterSettings DEFAULT_XML_WRITER_SETTINGS = new XmlWriterSettings
        {
            CheckCharacters = false,
            Encoding = Encoding.UTF8,
            Indent = false
        };

    }
}
