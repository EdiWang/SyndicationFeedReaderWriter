// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Edi.SyndicationFeed.ReaderWriter;

public sealed class SyndicationAttribute : ISyndicationAttribute
{
    public SyndicationAttribute(string name, string value) :
        this(name, null, value)
    {
    }

    public SyndicationAttribute(string name, string ns, string value)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Value = value ?? throw new ArgumentNullException(nameof(value));
        Namespace = ns;
    }

    public string Name { get; private set; }
    public string Namespace { get; private set; }
    public string Value { get; private set; }
}