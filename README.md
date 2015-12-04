# NeverNull
A Option type that prevents using null or "magic values" (NullObject, exit code -1, index out of range, etc.) in your code.
Licensed under the MIT License (http://opensource.org/licenses/MIT).

[![NuGet Status](http://img.shields.io/nuget/v/NeverNull.svg)](https://www.nuget.org/packages/NeverNull/)
[![Issue Stats](http://www.issuestats.com/github/bomret/nevernull/badge/pr?style=flat)](http://www.issuestats.com/github/bomret/nevernull)
[![Issue Stats](http://www.issuestats.com/github/bomret/nevernull/badge/issue?style=flat)](http://www.issuestats.com/github/bomret/nevernull)
[![Covertity Scan](https://img.shields.io/coverity/scan/7200.svg)](https://scan.coverity.com/projects/bomret-nevernull/)

## Build status
|  |  BuildScript | Status of last build |
| :------ | :------: | :------: |
| **Mono** | [build.sh](https://github.com/Bomret/NeverNull/blob/master/build.sh) | [![Travis build status](https://travis-ci.org/Bomret/NeverNull.svg)](https://travis-ci.org/Bomret/NeverNull) |
| **Windows** | [build.cmd](https://github.com/Bomret/NeverNull/blob/master/build.cmd) | [![AppVeyor Build status](http://img.shields.io/appveyor/ci/stefanreichel/nevernull.svg)](https://ci.appveyor.com/project/StefanReichel/nevernull) |

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

## Combinators
Since `Some` and `None` are simple data structures, a couple of extension methods are provided that make working with both types easier.

### IfSome
```csharp
Option.From(someString).IfSome(value => /* Do something with the value */);
```
`IfSome` is only executed if `someString` is not `null` and delegates the value of the `Some` to its enclosed callback.

### IfNone
```csharp
Option.From<string>(null).IfNone(() => _wasNull = true;);
```
`IfNone` is only executed if `null` was returned and then executes its enclosed callback.

### Match
```csharp
Option.From(2).Match(
    Some: i => _two = i,
   	None: () => _isNone = true);
```
`Match` allows to use a pattern matching like callback registration. The first function parameter is only executed in case of a `Some` and gets the value to work with. The second function parameter is registered to handle `None` and is only executed if the value is null.

```csharp
string result = Option.From(2).Match(
    Some: i => i.ToString(),
   	None: () => "");
```
This overload for `Match` produces a value. In the above example `result` would be the string `"2"`.

### ToNullable
```csharp
Option<DateTime> nowOption = Option.From(DateTime.Now)

DateTime? maybeNow = nowOption.ToNullable();
```
Converts an `Option<T>` (where `T` is a value type) to a `Nullable<T>`.

### Get
```csharp
int two = Option.From(2).Get();
```
`Get` is the most straight forward extension. It returns the value if the result is a `Some` or throws an `InvalidOperationException`, if `None`. In the above example `two` would be `2`.

### GetOrElse
```csharp
int two = Option.From(2).GetOrElse(-1);
```
`GetOrElse` either returns the value, if `From` returned a `Some` or the else value, if `From` returned `None`. In the above example `two` would be `2`. It would have been `-1` if `2` was `null`.

### GetOrDefault
```csharp
int two = Option.From(2).GetOrDefault();
```
`GetOrDefault` either returns the value, if `From` returned a `Some` or the value of `default(T)`, if `From` returned `None`. In the above example `two` would be `2`. It would have been `0` which is `default(int)` if `2` was `null`.

### OrElse
```csharp
Option<int> result = Option.From<int?>(null).OrElse(-1);
```
In the above examples `null` would have been returned and `result` would be `None`. The `OrElse` combinator makes it possible to return a different value in case of `None`. `result` would be a `Some<int>` with the Value `-1`.

### OrElseWith
```csharp
Option<int> result = Option.From<int?>(null).OrElseWith(Option.From(-1));
```
In the above examples `null` would have been returned and `result` would be `None`. The `OrElseWith` combinator makes it possible to return a different `Option` in case of `None`. `result` would be a `Some<int>` with the Value `-1`.

### Select
```csharp
Option<string> result = Option.From(2).Select(i => i.ToString());
```
`Select` allows to apply a function to the value of a `Some`. In the above example `result` would contain the string value `"5"`.

### SelectMany
```csharp
Option<string> result = Option.From(2).SelectMany(i => Option.From(i.ToString()));
```
`SelectMany` allows to apply a function to the value of a `Some` that returns another `Option` and avoid the nesting that would occur otherwise. In the above example `result` would be a `Some` with Value `"5"`. If `Some` would have been used, `result` would have been a `Option<Option<string>>`.

### Where
```csharp
var result = Option.From(5).Where(i => i == 5);
```
`Where` checks if a given predicate holds true for an `Option`. In the above example `result` would be a `Some` with Value `5`. If the predicate `i => i == 5` would not hold, `result` would have been `None`.

### Reject
```csharp
var result = Option.From(5).Reject(i => i == 5);
```
`Reject` does the exact opposite of `Where`. In the above example `result` would be `None`. If the predicate `i => i == 5` would not hold, `result` would have been a `Some` with value `5`.

### Zip
```csharp
Option<int> five = 5;
Option<int> four = 4;

Option<int> nine = five.Zip(four, (a, b) => a + b);
```
Takes another `Option` and allows to apply a `Func` to the values of both options. If one `Option` would be `None` the function would not be applied.
In the above example `nine` would be a `Some` containing `9`.

### ZipWith
```csharp
Option<int> five = 5;
Option<int> four = 4;

Option<int> nine = five.ZipWith(four, (a, b) => Option.From(a + b));
```
Takes another `Option` and allows to apply a `Func` to the values of both options. If one `Option` would be `None` the function would not be applied.
In the above example `nine` would be a `Some` containing `9`.

### Transform
```csharp
Option<string> result = Option.From(2).Transform(
    Some: i => i.ToString(),
    None: () => "");
```
Can be used to transform the value of an `Option`. The first function parameter transforms the resulting value if it is a `Some`, the second returns a value if it is `None`. In the above example `result` would be a `Some` with value `"5"`.

### Do
```csharp
Option<string> result = Option.From(2)
    .Do(() => Console.Write("Do executed"))
    .Select(i => i.ToString());
```
Can be used to execute side effecting behavior without modifying an `Option`. In the above example `result` would be a `Some` containing `"2"`. The given `Action` will be executed in _either case_, regardless if the incoming `Option` is a `Some` or `None`.

### Flatten
```csharp
Option<Option<int>> nestedOption = Option.From(Option.From(2));
Option<int> result = nestedOption.Flatten();
```
Flattens a nested `Option`.