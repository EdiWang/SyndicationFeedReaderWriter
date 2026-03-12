// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Edi.SyndicationFeed.ReaderWriter;

public class SyndicationContent : ISyndicationContent
{
    private ICollection<ISyndicationAttribute> _attributes;
    private ICollection<ISyndicationContent> _children;

    public SyndicationContent(string name, string value = null)
        : this(name, null, value)
    {
    }

    public SyndicationContent(string name, string ns, string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);


        Name = name;
        Value = value;
        Namespace = ns;

        _attributes = [];
        _children = [];
    }

    public SyndicationContent(ISyndicationContent content)
    {
        ArgumentNullException.ThrowIfNull(content);

        Name = content.Name;
        Namespace = content.Namespace;
        Value = content.Value;

        // Copy collections only if needed
        _attributes = content.Attributes as ICollection<ISyndicationAttribute> ?? content.Attributes.ToList();
        _children = content.Fields as ICollection<ISyndicationContent> ?? content.Fields.ToList();
    }

    public string Name { get; }

    public string Namespace { get; }

    public string Value { get; set; }

    public IEnumerable<ISyndicationAttribute> Attributes => _attributes;

    public IEnumerable<ISyndicationContent> Fields => _children;

    public void AddAttribute(ISyndicationAttribute attribute)
    {
        ArgumentNullException.ThrowIfNull(attribute);

        if (_attributes.IsReadOnly)
        {
            _attributes = _attributes.ToList();
        }

        _attributes.Add(attribute);
    }

    public void AddField(ISyndicationContent field)
    {
        ArgumentNullException.ThrowIfNull(field);

        if (_children.IsReadOnly)
        {
            _children = _children.ToList();
        }

        _children.Add(field);
    }
}