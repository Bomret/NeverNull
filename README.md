# NeverNull
A Option type for .net that prevents using null or "magic values" (NullObject, exit code -1, index out of range, etc.) in your code.
Licensed under the MIT License (http://opensource.org/licenses/MIT).

[![NuGet](https://img.shields.io/nuget/v/NeverNull.svg?style=flat-square)](https://www.nuget.org/packages/NeverNull/)
[![Travis branch](https://img.shields.io/travis/Bomret/NeverNull/master.svg?style=flat-square)](https://travis-ci.org/Bomret/NeverNull)
[![AppVeyor branch](https://img.shields.io/appveyor/ci/StefanReichel/nevernull/master.svg?style=flat-square)](https://ci.appveyor.com/project/StefanReichel/nevernull)
[![license](https://img.shields.io/github/license/bomret/NeverNull.svg?style=flat-square)](https://opensource.org/licenses/MIT)

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
------

## License
The [MIT License](http://opensource.org/licenses/MIT)

## Troubleshooting and support
Did you find a bug or have an idea for a new feature? Open a new [Issue](https://github.com/Bomret/NeverNull/issues).

## Contributing
* Read the [Code of Conduct](https://github.com/Bomret/NeverNull/blob/master/CODE_OF_CONDUCT.md)
* Take a look at the [Issues](https://github.com/Bomret/NeverNull/issues). If there is one you want to work on (has labels `ready` and `up-for-grabs`), write a comment that you want to work on it. If you have a new idea/problem, please open an issue and explain it.
* Fork the repo and clone it on your machine.
* Build the project running `build.cmd` on Windows or `build.sh` on Mac OSX/Linux.
* Write your code and don't forget to add xml docs and tests.
* Run the `build.*` for your platform again to ensure the build works and all tests pass.
* Describe your feature in the README, if applicable (e.g. new combinator).
* Create a pull request.

## Versioning
This project uses [SemVer](http://semver.org/) compatible versioning, which means `Breaking.Feature.Fix`.
* `Breaking`: Changes that break API compatibility with earlier versions.
* `Feature`: Added functionality that don't break API compatibility with earlier versions.
* `Fix`: Backwards compatible bugfixes and refactoring/clean up.

## Deprecation of features
If a feature becomes deprecated it is marked with the `Obsolete` attribute and will not throw a compiler error.
In the next release it will throw a compiler error and the major version is incremented by 1 because of breaking changes.
In the release after that, the feature will be removed and its patch version is incremented by 1.

### Example
* 3.2.0
```csharp
[Obsolete("This method is deprecated and will be removed in 2 releases.")]
public bool TryGet(out value) => // ...
```

* 4.0.0
```csharp
[Obsolete("This method is deprecated and will be removed in the next release.", true)]
public bool TryGet(out value) => // ...
```

* 4.0.1
```csharp
// TryGet removed
```

## Maintainers
* [Stefan Reichel (@bomret)](https://github.com/Bomret)

## Contributors
* [@fdub](https://github.com/fdub)

------

## Basics
`Option<T>` represents the absence or presence of a value. If a `Option<T>` contains a value, we'll call it `Some` from now on. If it is empty, we'll call it `None`.

```csharp
Option<int> result = Option.From(2);
```
The above example would evaluate `2` and - because that is a valid integer - return a `Some` and store it in the variable `result`. The result of the calculation is stored inside the `Some` and can be accessed using the `Match`, `IfSome`, several `Get*` or `TryGet`(DEPRECATED) methods:

```csharp
// using the Match method
int two;
result.Match(
    None: () => { /* no value present */ },
    Some: i => two = i);
        
// or using IfSome
int two;
result.IfSome(i => two = i); 

// or using the deprecated TryGet method
int two;
if(result.TryGet(out two))
    /* do something with two */
else
    /* no value present */
```
To find out if `result` represents a `Some` or `None` it provides the boolean properties `IsSome` and `IsNone`:

```csharp
if(result.IsSome)
    // do something;
```

```csharp
if(result.IsNone)
    // do something;
```

### Match
```csharp
Option.From(2).Match(
    Some: i => { /* do something with the value */ },
   	None: () => { /* no value present */ });
```
`Match` allows to use a pattern matching like callback registration. The first function parameter is only executed in case of a `Some` and gets the value to work with. The second function parameter is registered to handle `None` and is only executed if the value is null.

```csharp
string result = Option.From(2).Match(
    Some: i => i.ToString(),
   	None: () => "");
```
This overload for `Match` produces a value. In the above example `result` would be the string `"2"`.

### IfSome
```csharp
Option.From(someString).IfSome(value => /* Do something with the value */);
```
`IfSome` is only executed if `someString` is not `null` and delegates the value of the `Some` to its enclosed callback.

### IfNone
```csharp
Option.From<string>(null).IfNone(() => /* No value present */);
```
`IfNone` is only executed if `None` was returned and then executes its enclosed callback.

## Recommended usage
Every method that may not return a value in some circumstances should return an `Option<T>` instead of the result directly. That way, eventual `null` references or the usage of "magic values" can be avoided.
Optional parameters can also be expressed more clearly with an `Option<T>` instead of some arbitrary value that has to be checked inside the method, which can easily be forgotten. And you don't have to place them last in the parameter list.

So instead of this:
```csharp
string DoSomethingThatCouldReturnNull(string arg) { /* ... */ }
int DoSomethingThatCouldReturnAMagicValue(string arg) { /* ... */ }
void DoSomethingWithAnOptionalParameter(double wouldBeSecond, int? wouldBeFirst = null) { /* ... */ }
```

write this:
```csharp
Option<string> DoSomethingThatCouldReturnNull(string arg) { /* ... */ }
Option<int> DoSomethingThatCouldReturnAMagicValue(string arg) { /* ... */ }
void DoSomethingWithAnOptionalParameter(Option<int> first, double second) { /* ... */ }
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

### ToOptionMapped(mapFn)
```csharp
int? source = 3;
Option<CustomType> value = source.ToOptionMapped(CustomType.Create);
```
Converts a nullable struct to `Option<CustomType>` (where CustomType is a wrapper for `T` with a factory method `Create(T)`) using given map function. 

### ToOptionMappedOrNoneIf(mapfn)
```csharp
int source = 3;
Option<CustomType> value = source.ToOptionMappedOrNoneIf(0, CustomType.Create);
```
Converts a struct to `Option<CustomType>` (where CustomType is a wrapper for `T` with a factory method `Create(T)`) using given map function. If given source value equals given magic value for none, `Option.None` ist returned.

### Using static imports in C# 6
C# 6 offers the feature to statically import classes and use the static methods therein without having to prefix them with the class name.
NeverNull provides a specific module for taking advantage of this feature.
```csharp
using static NeverNull.Predef

Option<int> two = Option(2);
// or
Option<int> two = Some(2);
// io for None
Option<int> none = None;
```

## Combinators
Since `Some` and `None` are simple data structures, a couple of extension methods are provided that make working with both types easier.

### Contains
```csharp
bool containsFive = Option.From(5).Contains(5);
```
Returns `true` if the `Option` it is applied to is a `Some` containing the desired value otherwise `false`.

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

### ToNullable(selectFn)

```csharp
Option<CustomType> source = CustomType.Create(3).ToOption();
int? result = source.ToNullable(ct => ct.Value);
```
Converts an `Option<CustomType>` (where `CustomType` is a wrapper for a `T` with property `public T Value {get;}`) to a `Nullable<T>`.

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

### GetOrDefault (Deprecated)
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
