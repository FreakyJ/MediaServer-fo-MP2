﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using MediaPortal.Extensions.MediaServer.Objects;

namespace MediaPortal.Extensions.MediaServer.DIDL
{
    public class ObjectRenderer
    {
        private object DirObject { get; set; }

        public ObjectRenderer(object obj)
        {
            DirObject = obj;
        }

        public class DirectoryPropertyComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                // Ensure that the xml text part is sorted to the bottom.
                if (x == y && x == string.Empty) return 0;
                if (x == string.Empty) return 1;
                if (y == string.Empty) return -1;

                return x.CompareTo(y);
            }
        }

        public void Render(PropertyFilter filter, XmlWriter xml)
        {
            // Retrieve a list of properties that have a DirectoryProperty Attribute.
            var properties = from p in
                                    DirObject.GetType().GetInterfaces()
                                    .SelectMany(i => i.GetProperties()).Distinct()
                                let attrs = p.GetCustomAttributes(typeof(DirectoryPropertyAttribute), false)
                                where attrs.Count() > 0
                                select new { Attribute = ((DirectoryPropertyAttribute)attrs[0]), Property = p };
            properties = properties.OrderBy(x => x.Attribute.XmlPart, new DirectoryPropertyComparer());

            var curElement = string.Empty;
            foreach (var p in properties)
            {
                if (!(p.Attribute.Required || filter.IsAllowed(p.Attribute.XmlPart))) continue;

                var match = Regex.Match(p.Attribute.XmlPart, @"(\w*):?(\w*)@?(\w*):?(\w*)");

                var propNamespace = match.Groups[1].Value;
                var propElement = match.Groups[2].Value;
                var propAttributeNamespace = match.Groups[3].Value;
                var propAttribute = match.Groups[4].Value;

                if (propElement == string.Empty && propNamespace != string.Empty)
                {
                    propElement = propNamespace;
                    propNamespace = string.Empty;
                }

                if (propAttribute == string.Empty && propAttributeNamespace != string.Empty)
                {
                    propAttribute = propAttributeNamespace;
                    propAttributeNamespace = string.Empty;
                }

                Console.WriteLine(propNamespace + ":" + propElement + "@" + propAttributeNamespace + ":" + propAttribute);

                var valueObj = p.Property.GetGetMethod().Invoke(DirObject, null);

                var propType = p.Property.PropertyType;
                IList<object> values = new List<object>();

                if (propType.IsGenericType)
                {
                    var enumerable = valueObj as IEnumerable;
                    if (enumerable != null)
                    {
                        foreach (var o in enumerable)
                        {
                            values.Add(o);
                        }
                    }
                }
                else
                {
                    if (valueObj != null)
                    {
                        if (valueObj is bool)
                        {
                            values.Add(valueObj.ToString().ToLower());
                        }
                        else
                        {
                            values.Add(valueObj.ToString());                            
                        }
                    }
                    else if (p.Attribute.Required)
                    {
                        // In the case where we have an attribute which is required 
                        // We must output it, but with a null value.
                        values.Add("null");
                    }
                }

                foreach (var value in values)
                {
                    if (propElement != string.Empty)
                    {
                        if (propAttribute == String.Empty)
                        {
                            // we need to write an element string
                            if (value is string)
                            {
                                xml.WriteElementString(propNamespace, propElement, null, value.ToString());
                            }
                            else
                            {
                                xml.WriteStartElement(propNamespace, propElement, null);
                                Render(filter.CloneWithElementBase(propNamespace, propElement), value, xml);
                                xml.WriteEndElement();
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException(string.Format("Unable to render property {0} with xml type {1}" + p.Property.Name, p.Attribute.XmlPart));
                        }
                    }

                    if (propAttribute != String.Empty)
                    {
                        xml.WriteAttributeString(propAttributeNamespace, propAttribute, null, value.ToString());
                    }
                    else if (propElement == string.Empty)
                    {
                        xml.WriteString(value.ToString());
                    }
                }
            }
        }


        public static void Render(PropertyFilter filter, object obj, XmlWriter xml)
        {
            var or = new ObjectRenderer(obj);
            or.Render(filter, xml);
        }

        private class ObjectComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                return x.CompareTo(y);
            }
        }

    }
}
