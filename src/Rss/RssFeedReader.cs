// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Edi.SyndicationFeed.ReaderWriter.Utils;
using System.Threading.Tasks;
using System.Xml;

namespace Edi.SyndicationFeed.ReaderWriter.Rss;

public class RssFeedReader(XmlReader reader, ISyndicationFeedParser parser) : XmlFeedReader(reader, parser)
{
    private readonly XmlReader _reader = reader;
    private bool _knownFeed;

    public RssFeedReader(XmlReader reader)
        : this(reader, new RssParser())
    {
    }

    public override async Task<bool> Read()
    {
        if (!_knownFeed)
        {
            await InitRead();
            _knownFeed = true;
        }

        return await base.Read();
    }

    protected override SyndicationElementType MapElementType(string elementName)
    {
        if (_reader.NamespaceURI != RssConstants.Rss20Namespace)
        {
            return SyndicationElementType.Content;
        }

        return elementName switch
        {
            RssElementNames.Item => SyndicationElementType.Item,
            RssElementNames.Link => SyndicationElementType.Link,
            RssElementNames.Category => SyndicationElementType.Category,
            RssElementNames.Author or RssElementNames.ManagingEditor => SyndicationElementType.Person,
            RssElementNames.Image => SyndicationElementType.Image,
            _ => SyndicationElementType.Content,
        };
    }

    private async Task InitRead()
    {
        // Check <rss>
        bool knownFeed = _reader.IsStartElement(RssElementNames.Rss, RssConstants.Rss20Namespace) &&
                         _reader.GetAttribute(RssElementNames.Version).Equals(RssConstants.Version);

        if (knownFeed)
        {
            // Read<rss>
            await XmlUtils.ReadAsync(_reader);

            // Check <channel>
            knownFeed = _reader.IsStartElement(RssElementNames.Channel, RssConstants.Rss20Namespace);
        }

        if (!knownFeed)
        {
            throw new XmlException("Unknown Rss Feed");
        }

        // Read <channel>
        await XmlUtils.ReadAsync(_reader);
    }
}