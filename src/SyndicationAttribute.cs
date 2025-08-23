// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Edi.SyndicationFeed.ReaderWriter;

public sealed class SyndicationAttribute(string name, string ns, string value) : ISyndicationAttribute
{
    public SyndicationAttribute(string name, string value) :
        this(name, null, value)
    {
    }

    public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));
    public string Namespace { get; } = ns;
    public string Value { get; } = value ?? throw new ArgumentNullException(nameof(value));
}