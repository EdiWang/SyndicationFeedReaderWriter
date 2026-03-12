// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Edi.SyndicationFeed.ReaderWriter;

public class SyndicationItem : ISyndicationItem
{
    private ICollection<ISyndicationCategory> _categories;
    private ICollection<ISyndicationPerson> _contributors;
    private ICollection<ISyndicationLink> _links;

    public SyndicationItem()
    {
        _categories = [];
        _contributors = [];
        _links = [];
    }

    public SyndicationItem(ISyndicationItem item)
    {
        ArgumentNullException.ThrowIfNull(item);

        Id = item.Id;
        Title = item.Title;
        Description = item.Description;
        LastUpdated = item.LastUpdated;
        Published = item.Published;

        // Copy collections only if needed
        _categories = item.Categories as ICollection<ISyndicationCategory> ?? item.Categories.ToList();
        _contributors = item.Contributors as ICollection<ISyndicationPerson> ?? item.Contributors.ToList();
        _links = item.Links as ICollection<ISyndicationLink> ?? item.Links.ToList();
    }

    public string Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public IEnumerable<ISyndicationCategory> Categories => _categories;

    public IEnumerable<ISyndicationPerson> Contributors => _contributors;

    public IEnumerable<ISyndicationLink> Links => _links;

    public DateTimeOffset LastUpdated { get; set; }

    public DateTimeOffset Published { get; set; }

    public void AddCategory(ISyndicationCategory category)
    {
        ArgumentNullException.ThrowIfNull(category);

        if (_categories.IsReadOnly)
        {
            _categories = _categories.ToList();
        }

        _categories.Add(category);
    }

    public void AddContributor(ISyndicationPerson person)
    {
        ArgumentNullException.ThrowIfNull(person);

        if (_contributors.IsReadOnly)
        {
            _contributors = _contributors.ToList();
        }

        _contributors.Add(person);
    }

    public void AddLink(ISyndicationLink link)
    {
        ArgumentNullException.ThrowIfNull(link);

        if (_links.IsReadOnly)
        {
            _links = _links.ToList();
        }

        _links.Add(link);
    }
}