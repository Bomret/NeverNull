# NeverNull
A Option type that prevents using null or "magic values" (NullObject, exit code -1, index out of range, etc.) in your code.
Licensed under the MIT License (http://opensource.org/licenses/MIT).

[![NuGet Status](http://img.shields.io/nuget/v/NeverNull.svg)](https://www.nuget.org/packages/NeverNull/)
[![Issue Stats](http://www.issuestats.com/github/bomret/nevernull/badge/pr?style=flat)](http://www.issuestats.com/github/bomret/nevernull)
[![Issue Stats](http://www.issuestats.com/github/bomret/nevernull/badge/issue?style=flat)](http://www.issuestats.com/github/bomret/nevernull)
[![Stories in Ready](https://badge.waffle.io/Bomret/NeverNull.svg?label=ready&title=Ready)](http://waffle.io/Bomret/NeverNull)

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

## License
The [MIT License](http://opensource.org/licenses/MIT)

## Contributing
* Read the [Code of Conduct](https://github.com/Bomret/NeverNull/blob/master/CODE_OF_CONDUCT.md)
* Fork and clone.
* Build the project running `build.cmd` on Windows or `build.sh` on Mac OSX/Linux.
* Write your code and don't forget to add tests.
* Add xml docs in your code and describe your feature in the README.
* Create a pull request.

## Deprecation of features
If a feature becomes deprecated it is marked with the `Obsolete` attribute and will not throw a compiler error. In the next release it will throw a compiler error and in the release after that it will be removed.

### Example
* 3.2.0
```csharp
[Obsolete("This method is deprecated and will be removed in 2 releases.")]
public bool TryGet(out value) => // ...
```

* 3.2.1
```csharp
[Obsolete("This method is deprecated and will be removed in the next release.", true)]
public bool TryGet(out value) => // ...
```

* 3.2.2
```csharp
// TryGet removed
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
`IfNone` is only executed if `None` was returned and then executes its enclosed callback.

### Contains
```csharp
bool containsFive = Option.From(5).Contains(5);
```
Returns `true` if the `Option` it is applied to is a `Some` containing the desired value otherwise `false`.

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

### Normalize
```csharp
Option<int?> option = Option.From<int?>(5);
Option<int> normalized = option.Normalize();
```
Normalizes an `Option<T?>` into its `Option<T>` representation.

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
// or lazily with a func
int two = Option.From(2).GetOrElse(() => -1);
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
// or lazily with a func
Option<int> result = Option.From<int?>(null).OrElse(() => -1);
```
In the above examples `null` would have been returned and `result` would be `None`. The `OrElse` combinator makes it possible to return a different value in case of `None`. `result` would be a `Some<int>` with the Value `-1`.

### OrElseWith
```csharp
Option<int> result = Option.From<int?>(null).OrElseWith(Option.From(-1));
// or lazily with a func
Option<int> result = Option.From<int?>(null).OrElseWith(() => Option.From(-1));
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
    .Do(i => Console.Write($"Do executed on {i}"))
    .Select(i => i.ToString());
```
Can be used to execute side effecting behavior without modifying an `Option`. In the above example `result` would be a `Some` containing `"2"`. The given `Action` will be executed in _either case_, regardless if the incoming `Option` is a `Some` or `None`.

### Flatten
```csharp
Option<Option<int>> nestedOption = Option.From(Option.From(2));
Option<int> result = nestedOption.Flatten();
```
Flattens a nested `Option`.

### Switch
```csharp
Option<int?> two = Option.From(2);
Option<int?> nil = Option.From<int?>(null);
Option<int?> three = Option.From(3);

Option<string> result = nil.Switch(three, two);
```
Returns the first `Option` that contains a value or `None` if all are `None`.

## Combinators for `IEnumerable<T>`
NeverNull contains several extensions that integrate `Option<T>` with `IEnumerable<T>`.

### Exchange
```csharp
IEnumerable<int?> ints = new [] { 1, 2, default(int?), 4 };
IEnumerable<Option<int>> optionalInts = Option.From(ints).Exchange();
```
If the given `Option<IEnumerable<T>>` is a `Some`, `Option.From` is applied to all values of the enumerable. If it is `None` instead, an empty enumerable is returned.

### SelectValues
```csharp
IEnumerable<Option<int?>> ints = new[] {1, 2, default(int?), 4};
IEnumerable<int> values = ints.SelectValues();
```
Select only the values contained in the options of the given enumerable. If it only contains `None`, an empty enumerable is returned.

### AggregateOptional
```csharp
IEnumerable<string> strings = new[] {"a", "b", default(string), "d"};
Option<string> abd = strings.AggregateOptional((a, c) => a + c);
```
Like the standard Linq `Aggregate` but automatically handles null elements and results in an option.

### AggregateOptionalNullable
```csharp
IEnumerable<int?> ints = new[] {1, 2, default(int?), 4};
Option<int> seven = ints.AggregateOptional((a, c) => a + c);
```
Like `AggregateOptional` but for `Nullable<T>` elements.

### AllOrNone
```csharp
IEnumerable<Option<string>> strings = new[] {"a", "b", default(string), "d"};
Option<IEnumerable<string>> abd = strings.AllOrNone();
```
Returns an option containing all values or `None`, if any of the options in the enumerable does not contain a value or the enumerable is empty.

### FirstOptional
```csharp
IEnumerable<string> strings = new[] {"a", "b", default(string), "d"};
Option<string> a = strings.FirstOptional();
```
Returns a `Some` containing the first value if the enumerable contains at least one value, else `None`.

### LastOptional
```csharp
IEnumerable<string> strings = new[] {"a", "b", default(string), "d"};
Option<string> d = strings.LastOptional();
```
Returns a `Some` containing the last value if the enumerable contains at least one value, else `None`.

### SingleOptional
```csharp
IEnumerable<string> strings = new[] {"a"};
Option<string> a = strings.SingleOptional();
```
Like `Single` but returns the only element in the enumerable wrapped in an option. If this enumerable is empty or the single element is NULL, None is returned. Throws an exception if this enumerable contains more than one element.

### SingleOptionalNullable
```csharp
IEnumerable<string> strings = new[] {"a"};
Option<string> a = strings.SingleOptional();
```
Like `SingleOptional` but for `Nullable<T>` elements.