﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Edi.SyndicationFeed.ReaderWriter.Utils;

static class XmlUtils
{
    public const string XmlNs = "http://www.w3.org/XML/1998/namespace";

    public static string GetValue(string xmlNode)
    {
        using (XmlReader reader = XmlReader.Create(new StringReader(xmlNode)))
        {
            reader.MoveToContent();
            return reader.ReadElementContentAsString();
        }
    }

    public static Task<string> ReadOuterXmlAsync(XmlReader reader)
    {
        if (reader.Settings.Async)
        {
            return reader.ReadOuterXmlAsync();
        }

        return Task.FromResult(reader.ReadOuterXml());
    }

    public static Task SkipAsync(XmlReader reader)
    {
        if (reader.Settings.Async)
        {
            return reader.SkipAsync();
        }

        reader.Skip();

        return Task.CompletedTask;
    }

    public static Task<bool> ReadAsync(XmlReader reader)
    {
        if (reader.Settings.Async)
        {
            return reader.ReadAsync();
        }

        return Task.FromResult(reader.Read());
    }

    public static XmlReader CreateXmlReader(string value)
    {
        return XmlReader.Create(new StringReader(value),
            new XmlReaderSettings
            {
                ConformanceLevel = ConformanceLevel.Fragment,
                DtdProcessing = DtdProcessing.Ignore,
                IgnoreComments = true,
                IgnoreWhitespace = true
            });
    }

    public static XmlWriter CreateXmlWriter(XmlWriterSettings settings, IEnumerable<ISyndicationAttribute> attributes, StringBuilder buffer)
    {
        settings.Async = false;
        settings.OmitXmlDeclaration = true;
        settings.ConformanceLevel = ConformanceLevel.Fragment;

        XmlWriter writer = XmlWriter.Create(buffer, settings);

        //
        // Apply attributes
        if (attributes != null && attributes.Count() > 0)
        {
            //
            // Create element wrapper
            ISyndicationAttribute xmlns = attributes.FirstOrDefault(a => a.Name == "xmlns");

            if (xmlns != null)
            {
                writer.WriteStartElement("w", xmlns.Value);
            }
            else
            {
                writer.WriteStartElement("w");
            }

            //
            // Write attributes
            foreach (var a in attributes)
            {
                if (a != xmlns)
                {
                    writer.WriteSyndicationAttribute(a);
                }
            }

            writer.WriteStartElement("y");
            writer.WriteEndElement();

            writer.Flush();
            buffer.Clear();
        }

        return writer;
    }

    public static Task WriteRawAsync(XmlWriter writer, string content)
    {
        if (writer.Settings.Async)
        {
            return writer.WriteRawAsync(content);
        }

        writer.WriteRaw(content);

        return Task.CompletedTask;
    }

    public static Task FlushAsync(XmlWriter writer)
    {
        if (writer.Settings.Async)
        {
            return writer.FlushAsync();
        }

        writer.Flush();

        return Task.CompletedTask;
    }

    public static void SplitName(string name, out string prefix, out string localName)
    {
        int i = name.IndexOf(':');
        if (i > 0)
        {
            prefix = name.Substring(0, i);
            localName = name.Substring(i + 1);
        }
        else
        {
            prefix = null;
            localName = name;
        }
    }

    public static bool IsXmlns(string name, string ns)
    {
        return name == "xmlns" || ns == "http://www.w3.org/2000/xmlns/";
    }

    public static bool IsXmlSchemaType(string name, string ns)
    {
        return name == "type" && ns == "http://www.w3.org/2001/XMLSchema-instance";
    }

    public static bool IsXmlMediaType(string value)
    {
        return value != null && (value == "xml" || value.EndsWith("/xml") || value.EndsWith("+xml"));
    }

    public static bool IsXhtmlMediaType(string value)
    {
        return value == "xhtml";
    }

    public static bool NeedXmlEscape(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        for (int i = 0; i < value.Length; ++i)
        {
            char ch = value[i];

            if (ch == '<' || ch == '>' || ch == '&' || char.IsSurrogate(ch))
            {
                return true;
            }
        }

        return false;
    }
}