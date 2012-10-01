using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using MediaPortal.Extensions.MediaServer.Objects;

namespace MediaPortal.Extensions.MediaServer.DIDL
{
    public class GenericDidlMessageBuilder
    {
        private readonly StringBuilder _message;
        private XmlWriter _xml;
        private bool _hasCompleted;
        private PropertyFilter _filter;

        public GenericDidlMessageBuilder()
        {
            _message = new StringBuilder(10000);
            _hasCompleted = false;
            StartMessage();
        }

        protected void StartMessage()
        {
            _xml = XmlWriter.Create(new StringWriterWithEncoding(_message, Encoding.UTF8),
                                    DEFAULT_XML_WRITER_SETTINGS);
            _xml.WriteStartDocument();
            _xml.WriteStartElement(string.Empty, "DIDL-Lite", "urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/");
            _xml.WriteAttributeString("xmlns", "dc", null, "http://purl.org/dc/elements/1.1/");
            _xml.WriteAttributeString("xmlns", "dlna", null, "urn:schemas-dlna-org:metadata-1-0/");
            _xml.WriteAttributeString("xmlns", "upnp", null, "urn:schemas-upnp-org:metadata-1-0/upnp/");
        }

        public void Build(string filter, object directoryPropertyObject)
        {
            _filter = new PropertyFilter(filter);
            BuildInternal(directoryPropertyObject);
            EndMessage();
        }

        protected void BuildInternal(object directoryPropertyObject)
        {
            if (!_hasCompleted)
            {
                if (directoryPropertyObject is IDirectoryContainer)
                {
                    _xml.WriteStartElement("container");
                }
                else if (directoryPropertyObject is IDirectoryItem)
                {
                    _xml.WriteStartElement("item");
                }
                else
                {
                    throw new ArgumentException(
                        "directoryPropertyObject isn't either an IDirectoryContainer or IDirectoryItem");
                }
                ObjectRenderer.Render(_filter, directoryPropertyObject, _xml);
                _xml.WriteEndElement();
            }
        }

        public void BuildAll(string filter, IEnumerable objs)
        {
            _filter = new PropertyFilter(filter);
            foreach (var obj in objs)
            {
                BuildInternal(obj);
            }
            EndMessage();
        }

        protected void EndMessage()
        {
            _xml.WriteEndElement();
            _xml.Close();
            _hasCompleted = true;
        }

        public new string ToString()
        {
            return _message.ToString();
        }

        /// <summary>
        /// Default settins to be used by XML writers.
        /// </summary>
        public static XmlWriterSettings DEFAULT_XML_WRITER_SETTINGS = new XmlWriterSettings
                                                                          {
                                                                              CheckCharacters = false,
                                                                              Encoding = Encoding.UTF8,
                                                                              Indent = false
                                                                          };
    }

    public class StringWriterWithEncoding : StringWriter
    {
        protected Encoding _encoding;

        public StringWriterWithEncoding(Encoding encoding)
        {
            _encoding = encoding;
        }

        public StringWriterWithEncoding(IFormatProvider formatProvider, Encoding encoding)
            : base(formatProvider)
        {
            _encoding = encoding;
        }

        public StringWriterWithEncoding(StringBuilder sb, Encoding encoding)
            : base(sb)
        {
            _encoding = encoding;
        }

        public StringWriterWithEncoding(StringBuilder sb, IFormatProvider formatProvider, Encoding encoding)
            : base(sb, formatProvider)
        {
            _encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return _encoding; }
        }
    }


}
