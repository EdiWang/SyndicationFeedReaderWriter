// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Edi.SyndicationFeed.ReaderWriter.Utils;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace Edi.SyndicationFeed.ReaderWriter;

public abstract class XmlFeedWriter(XmlWriter writer, ISyndicationFeedFormatter formatter) : ISyndicationFeedWriter
{
    private readonly XmlWriter _writer = writer ?? throw new ArgumentNullException(nameof(writer));

    public ISyndicationFeedFormatter Formatter { get; } = formatter ?? throw new ArgumentNullException(nameof(formatter));

    public virtual Task Write(ISyndicationContent content)
    {
        return WriteRaw(Formatter.Format(content ?? throw new ArgumentNullException(nameof(content))));
    }

    public virtual Task Write(ISyndicationCategory category)
    {
        return WriteRaw(Formatter.Format(category ?? throw new ArgumentNullException(nameof(category))));
    }

    public virtual Task Write(ISyndicationImage image)
    {
        return WriteRaw(Formatter.Format(image ?? throw new ArgumentNullException(nameof(image))));
    }

    public virtual Task Write(ISyndicationItem item)
    {
        return WriteRaw(Formatter.Format(item ?? throw new ArgumentNullException(nameof(item))));
    }

    public virtual Task Write(ISyndicationPerson person)
    {
        return WriteRaw(Formatter.Format(person ?? throw new ArgumentNullException(nameof(person))));
    }

    public virtual Task Write(ISyndicationLink link)
    {
        return WriteRaw(Formatter.Format(link ?? throw new ArgumentNullException(nameof(link))));
    }

    public virtual Task WriteValue<T>(string name, T value)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        string valueString = Formatter.FormatValue(value) ?? throw new FormatException(nameof(value));
        return WriteRaw(Formatter.Format(new SyndicationContent(name, valueString)));
    }

    public virtual Task WriteRaw(string content)
    {
        return XmlUtils.WriteRawAsync(_writer, content);
    }

    public Task Flush()
    {
        return XmlUtils.FlushAsync(_writer);
    }
}