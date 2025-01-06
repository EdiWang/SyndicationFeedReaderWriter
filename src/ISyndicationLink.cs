// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Edi.SyndicationFeed.ReaderWriter;

public interface ISyndicationLink
{
    Uri Uri { get; }

    string Title { get; }

    string MediaType { get; }

    string RelationshipType { get; }

    long Length { get; }

    string Hreflang { get; set; }

    DateTimeOffset LastUpdated { get; }
}