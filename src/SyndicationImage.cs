// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.using System;

using System;

namespace Edi.SyndicationFeed.ReaderWriter;

public sealed class SyndicationImage(Uri url, string relationshipType = null) : ISyndicationImage
{
    public string Title { get; set; }

    public Uri Url { get; } = url ?? throw new ArgumentNullException(nameof(url));

    public ISyndicationLink Link { get; set; }

    public string RelationshipType { get; set; } = relationshipType;

    public string Description { get; set; }
}