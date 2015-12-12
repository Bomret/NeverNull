## Example
Reading the content type of a url as string and printing it to the console. If the safe cast to `HttpWebRequest` would return `null` the subsequent calls to `Select` and `Where` would not execute and *"No matching result"* would be printed to the console. The same would happen if any of the calls to `Select` would return null or the predicate `contentType.StartsWith("text")` would not hold in the Where predicate.

```csharp
Option.From(WebRequest.Create(Url) as HttpWebRequest)
    .Select(request => request.GetResponse()?.ContentType)
    .Where(contentType => contentType.StartsWith("text"))
    .Match(
        Some: contentType => Console.WriteLine("Content-Type: {0}", contentType),
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
    Some: contentType => Console.WriteLine("Content-Type: {0}", contentType),
    None: () => Console.WriteLine("No matching result."));
```
------

## Basics
`Option<T>` represents the absence or presence of a value. If a `Option<T>` contains a value, we'll call it `Some` from now on. If it is empty, we'll call it `None`.

```csharp
Option<int> result = Option.From(2);
```
The above example would evaluate `2` and - because that is a valid integer - return a `Some` and store it in the variable `result`. The result of the calculation is stored inside the `Some` and can be accessed using the `TryGet` method:

```csharp
int value;
if(result.TryGet(out value))
    // use value;
```
To find out if `result` represents a `Some` or `None` it provides the boolean property `HasValue`:

```csharp
if(result.HasValue) _two = i;
```

## Recommended usage
Every method that may not return a value in some circumstances should return an `Option<T>` instead of the result directly. That way, eventual `null` references or the usage of "magic values" can be avoided.

So instead of this:

```csharp
string DoSomethingThatCouldReturnNull(string a) { /* ... */ };
int DoSomethingThatCouldReturnAMagicValue(string a) { /* ... */ };
```
write this:

```csharp
Option<string> DoSomethingThatCouldReturnNull(string a) { /* ... */ };
Option<int> DoSomethingThatCouldReturnAMagicValue(string a) { /* ... */ };
```

## Creating an Option
There are several ways to create an `Option<T>`.

### From
```csharp
Option<int> option = Option.From(2);
// same as
Option<int> option = 2;
```
Evaluates a `T` synchronously and returns a `Some` if the value is not null or `None` otherwise. Always returns `Some`for non-nullable (value) types.

```csharp
DateTime? now = DateTime.Now;

Option<DateTime> option = Option.From(now);
```
Evaluates a `T?` synchronously and returns a `Some` if the value is not null or `None` otherwise.

### FromTryPattern
```csharp
Option<double> option = Option.FromTryPattern<string, double>(Double.TryParse, "2.6");
```
Evaluates the call to a given method that follows the TryParse pattern and arguments synchronously and returns a `Some` if the method succeeded or `None` otherwise.

Currently the method is overloaded with versions that take up to 16 args.

### None
```csharp
Option<int> none = Option.None;
// same as
Option<int> none = Option<int>.None;
```
Returns `None` that represents the absence of a value.

### ToOption
```csharp
Option<int> none = 3.ToOption();
```
Converts any value to an `Option<T>` by calling `Option.From` on it.