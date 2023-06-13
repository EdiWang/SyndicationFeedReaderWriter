# Edi.SyndicationFeed.ReaderWriter

[![.NET Build and Pack](https://github.com/EdiWang/SyndicationFeedReaderWriter/actions/workflows/dotnet.yml/badge.svg)](https://github.com/EdiWang/SyndicationFeedReaderWriter/actions/workflows/dotnet.yml)

> The original repo https://github.com/dotnet/SyndicationFeedReaderWriter was archived long time ago. I created this repo to continue maintain the project.

Edi.SyndicationFeed.ReaderWriter (original Microsoft.SyndicationFeed.ReaderWriter) provides lightweight forward-only read/write APIs (similar to .NET XmlReader) to simplify operations with RSS 2.0 ([spec](http://cyber.harvard.edu/rss/rss.html)) and Atom ([spec](https://tools.ietf.org/html/rfc4287)) syndication feeds. It offers extensiblity to support custom feed elements and formatting. The workflow is async on demand, which enables this library to be used on syndication feeds of arbitrary size or stream latency.

### Requirements:
* [Visual Studio 2022](https://www.visualstudio.com/vs/whatsnew/)

### Supports:
* .NET 6.0

### Building:
* The solution will build in Visual Studio 2022 after cloning.

### Running Tests:
* Open the solution in Visual Studio 2022.
* Build the Tests project.
* Open the Test Explorer and click "Run All" or run each test individually.

# Examples
Examples can be found [here](examples).

### Create an RssReader and Read a Feed ###
```cs
using (var xmlReader = XmlReader.Create(filePath, new XmlReaderSettings() { Async = true }))
{
    var feedReader = new RssFeedReader(xmlReader);

    while(await feedReader.Read())
    {
        switch (feedReader.ElementType)
        {
            // Read category
            case SyndicationElementType.Category:
                ISyndicationCategory category = await feedReader.ReadCategory();
                break;

            // Read Image
            case SyndicationElementType.Image:
                ISyndicationImage image = await feedReader.ReadImage();
                break;

            // Read Item
            case SyndicationElementType.Item:
                ISyndicationItem item = await feedReader.ReadItem();
                break;

            // Read link
            case SyndicationElementType.Link:
                ISyndicationLink link = await feedReader.ReadLink();
                break;

            // Read Person
            case SyndicationElementType.Person:
                ISyndicationPerson person = await feedReader.ReadPerson();
                break;

            // Read content
            default:
                ISyndicationContent content = await feedReader.ReadContent();
                break;
        }
    }
}
```

### Create an RssWriter and Write an Rss Item ###
```cs
var sw = new StringWriterWithEncoding(Encoding.UTF8);

using (XmlWriter xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { Async = true, Indent = true }))
{
    var writer = new RssFeedWriter(xmlWriter);
      
    // Create item
    var item = new SyndicationItem()
    {
        Title = "Rss Writer Avaliable",
        Description = "The new Rss Writer is now available as a NuGet Package!",
        Id = "https://www.nuget.org/packages/Microsoft.SyndicationFeed.ReaderWriter",
        Published = DateTimeOffset.UtcNow
    };

    item.AddCategory(new SyndicationCategory("Technology"));
    item.AddContributor(new SyndicationPerson("test", "test@mail.com"));

    await writer.Write(item);
    xmlWriter.Flush();
}

class StringWriterWithEncoding : StringWriter
{
    private readonly Encoding _encoding;

    public StringWriterWithEncoding(Encoding encoding)
    {
        this._encoding = encoding;
    }

    public override Encoding Encoding {
        get { return _encoding; }
    }
}
```
