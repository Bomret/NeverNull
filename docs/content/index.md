## Documentation
A Option type that prevents using null or "magic values" (NullObject, exit code -1, index out of range, etc.) in your code.
Licensed under the [MIT License][mit].


The NeverNull library can be [installed from NuGet][nuget]:
```powershell
PM> Install-Package NeverNull
```

## Example
Reading the content type of a url as string and printing it to the console. If the safe cast to `HttpWebRequest` would return `null` the subsequent calls to `Select` and `Where` would not execute and *"No matching result"* would be printed to the console. The same would happen if any of the calls to `Select` would return null or the predicate `contentType.StartsWith("text")` would not hold in the Where predicate.

```csharp
Option.From(WebRequest.Create(Url) as HttpWebRequest)
    .Select(request => request.GetResponse()?.ContentType)
    .Where(contentType => contentType.StartsWith("text"))
    .Match(
        Some: contentType => Console.WriteLine($"Content-Type: {contentType}"),
        None: () => Console.WriteLine("No matching result."));
```

The same can be written using LINQ syntax:

```csharp
var maybeText =
    from req in Option.From(WebRequest.Create(Url) as HttpWebRequest)
    let contentType = req.GetResponse()?.ContentType
    where contentType.StartsWith("text")
    select contentType;
        
maybeText.Match(
    Some: contentType => Console.WriteLine($"Content-Type: {contentType}"),
    None: () => Console.WriteLine("No matching result."));
```

## Contributing and copyright
The project is hosted on [GitHub][gh] where you can [report issues][issues], fork 
the project and submit pull requests. If you're adding a new public API, please also 
consider adding [samples][content] that can be turned into a documentation. You might
also want to read the [library design notes][readme] to understand how it works.

The library is available under MIT license, which allows modification and 
redistribution for both commercial and non-commercial purposes. For more information see the 
[License file][license] in the GitHub repository.

Icon: like by Gregor Črešnar from the [Noun Project][nounproject].

  [mit]: http://opensource.org/licenses/MIT
  [nuget]: https://nuget.org/packages/NeverNull
  [content]: https://github.com/bomret/NeverNull/tree/master/docs/content
  [gh]: https://github.com/bomret/NeverNull
  [issues]: https://github.com/bomret/NeverNull/issues
  [readme]: https://github.com/bomret/NeverNull/blob/master/README.md
  [license]: https://github.com/bomret/NeverNull/blob/master/LICENSE.txt
  [nounproject]: https://thenounproject.com/