// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Edi.SyndicationFeed.ReaderWriter;

public sealed class SyndicationCategory : ISyndicationCategory
{
    public SyndicationCategory(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public string Name { get; }

    public string Label { get; set; }

    public string Scheme { get; set; }
}