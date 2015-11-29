# NeverNull
A Option type that prevents using null or "magic values" (NullObject, exit code -1, index out of range, etc.) in your code.
Licensed under the MIT License (http://opensource.org/licenses/MIT).

## Build status
|  |  BuildScript | Status of last build |
| :------ | :------: | :------: |
| **Mono** | [build.sh](https://github.com/Bomret/NeverNull/blob/master/build.sh) | [![Travis build status](https://travis-ci.org/Bomret/NeverNull.svg)](https://travis-ci.org/Bomret/NeverNull) |
| **Windows** | [build.cmd](https://github.com/Bomret/NeverNull/blob/master/build.cmd) | [![AppVeyor Build status](http://img.shields.io/appveyor/ci/stefanreichel/nevernull.svg)](https://ci.appveyor.com/project/StefanReichel/nevernull) |

## Nuget status
[![NuGet Status](http://img.shields.io/nuget/v/NeverNull.svg)](https://www.nuget.org/packages/NeverNull/)

## Example
Reading the content type of a url as string and printing it to the console. If the safe cast to `HttpWebRequest` would return `null` the subsequent calls to `Map` and `Filter` would not execute and *"No matching result"* would be printed to the console. The same would happen if any of the calls to `Map` would return null or the predicate `contentType.StartsWith("text")` would not hold in the Filter function.

```csharp
Option.From(WebRequest.Create(Url) as HttpWebRequest)
      .Map(request => request.GetResponse())
      .Map(response => response.ContentType)
      .Filter(contentType => contentType.StartsWith("text"))
      .Match(
          contentType => Console.WriteLine("Content-Type: {0}", contentType),
          () => Console.WriteLine("No matching result."));
```
------

## Basics
`Option<T>` represents the absence or presence of a value. If a `Option<T>` contains a value, we'll call it `Some` from now on. If it is empty, we'll call it `None`.

```csharp
Option<int> result = Option.From(2);
```
The above example would evaluate `2` and - because that is a valid integer - return a `Some` and store it in the variable `result`. The result of the calculation is stored inside the `Some` and can be accessed using the `Value` property:

```csharp
var two = result.Value;
```
To find out if `result` represents a `Some` or `None` it provides two boolean properties: `HasValue` and `IsEmpty`. To be safe, you can either check these properties first or use the corresponding applicator (more on applicators below):

```csharp
result.IfSome(i => _two = i);
```

## Recommended usage
Every method that may not return a value in some circumstances should return an `Option<T>` instead of the result directly. That way, eventual `null` references or the usage of "magic values" can be avoided.

So instead of this:

```csharp
private string DoSomethingThatCouldReturnNull(string a) { //... };
private int DoSomethingThatCouldReturnAMagicValue(string a) { //... };
```
write this:

```csharp
private Option<string> DoSomethingThatCouldReturnNull(string a) { //... };
private Option<int> DoSomethingThatCouldReturnAMagicValue(string a) { //... };
```

## Creating an Option
There are several ways to create an `Option<T>`.

### From
```csharp
Option<int> option = Option.From(2);
```
Evaluates a `T` synchronously and returns a `Some` if the value is not null or `None` otherwise. Always returns `Some`for non-nullable (value) types.

### FromNullable
```csharp
DateTime? now = DateTime.Now;

Option<DateTime> option = Option.FromNullable(now);
```
Evaluates a `T?` synchronously and returns a `Some` if the value is not null or `None` otherwise.

### FromTryPattern
```csharp
Option<double> option = Option.FromTryPattern<string, double>(Double.TryParse, "2.6");
```
Evaluates the call to a given method that follows the TryParse pattern and arguments synchronously and returns a `Some` if the method succeeded or `None` otherwise.

Currently the method is overloaded with versions that take up to five args.

### Some
```csharp
Option<int> five = Option.Some(5);
```
Create a `Some` that contains the given value.

### None
```csharp
Option<int> none = Option.None;
```
Returns `None` that represents the absence of a value.

## Combinators
Since `Some` and `None` are simple data structures, a couple of extension methods are provided that make working with both types easier.

### IfSome
```csharp
Option.From(2)
      .IfSome(i => _two = i);
```
`IfSome` is only executed if `2` is not return `null` and delegates the value of the `Some` to its enclosed callback. In the above example `_two` would be a int with Value `2`.

### IfNone
```csharp
Option.From(null)
      .IfNone(() => _wasNull = true;);
```
`IfNone` is only executed if `null` was returned and then executes its enclosed callback.

### Match
```csharp
Option.From(2)
       .Match(
   		i => _two = i,
   		() => _isNone = true);
```
`Match` allows to use a pattern matching like callback registration. The first function parameter is only executed in case of a `Some` and gets the value to work with. The second function parameter is registered to handle `None` and is only executed if the value is null.

```csharp
string result = Option.From(2)
                .Match(
   		             i => i.ToString(),
   		             () => "");
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
`Get` is the most straight forward applicator. It returns the value if the result is a `Some` or throws an `InvalidOperationException`, if `None`. In the above example `two` would be `2`.

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
var result = Option.From(null)
				   .OrElse(-1);
```
In the above examples `null` would have been returned and `result` would be `None`. The `OrElse` combinator makes it possible to return a different value in case of `None`. `result` would be a `Some<int>` with the Value *-1*.

### OrElseWith
```csharp
var result = Option.From(null)
				   .OrElseWith(Option.Some(-1));
```
In the above examples `null` would have been returned and `result` would be `None`. The `OrElseWith` combinator makes it possible to return a different `Option` in case of `None`. `result` would be a `Some<int>` with the Value *-1*.

### Map
```csharp
var result = Option.From(2)
				   .Map(i => i.ToString());
```
`Map` allows to apply a function to the value of a `Some`. In the above example `result` would be a `Some<string>` with Value *"5"*.

### FlatMap
```csharp
var result = Option.From(2)
                   .FlatMap(i => Option.From(i.ToString()));
```
`FlatMap` allows to apply a function to the value of a `Some` that returns another `Option` and avoid the nesting that would occur otherwise. In the above example `result` would be a `Some<string>` with Value `"5"`. If `Map` would have been used, `result` would have been a `Some<Some<string>>`.

### Filter
```csharp
var result = Option.From(5)
				   .Filter(i => i == 5);
```
`Filter` checks if a given predicate holds true for an `Option`. In the above example `result` would be a `Some<int>` with Value `5`. If the predicate `i => i == 5` would not hold, `result` would have been `None`.

### Reject
```csharp
var result = Option.From(5)
				   .Reject(i => i == 5);
```
`Reject` does the exact opposite of `Filter`. In the above example `result` would be `None`. If the predicate `i => i == 5` would not hold, `result` would have been a `Some<int>` with value `5`.

### Zip
```csharp
Option<int> five = 5;
Option<int> four = 4;

Option<int> nine = five.Zip(four, (a, b) => a + b);
```
Takes one other `Option` and allows to apply a `Func` to the values of both options. If one `Option` would be `None` the function would not be applied.
In the above example `nine` would be a `Some<int>` containing `9`.

### ZipWith
```csharp
Option<int> five = 5;
Option<int> four = 4;

Option<int> nine = five.ZipWith(four, (a, b) => Option.From(a + b));
```
Takes one other `Option` and allows to apply a `Func` to the values of both options. If one `Option` would be `None` the function would not be applied.
In the above example `nine` would be a `Some<int>` containing `9`.

### Transform
```csharp
Option<string> result = Option.From(2)
        				.Transform(
        					i => i.ToString(),
        					() => "");
```
Can be used to transform the value of an `Option`. The first function parameter transforms the resulting value if it is a `Some`, the second returns a value if it is `None`. In the above example `result` would be a `Some<string>` with Value `"5"`.

### TransformWith
```csharp
Option<string> result = Option.From(2)
        				.TransformWith(
        					i => Option.From(i.ToString()),
        					() => Option.None);
```
Can be used to transform the value of an `Option`. The first function parameter transforms the resulting value if it is a `Some`, the second returns a value if it is `None`. In the above example `result` would be a `Some<string>` with Value `"5"`.

### Tap
```csharp
Option<int> result = Option.From(2)
        			 .Tap(i => Console.WriteLine(i))
        			 .Map(i => i + 1);
```
Can be used to take a look at the value of an `Option` without modifying it. In the above example `result` would be a `Some<int>` containing the value `3`. `Tap` would have printed `2` to the console.

### Do
```csharp
Option<string> result = Option.From(2)
				        .Do(() => Console.Write("Do executed"))
				        .Map(i => i.ToString());
```
Can be used to execute side effecting behavior without modifying an `Option`. In the above example `result` would be a `Some<string>` containing `"2"`. The given `Action` will be executed in _either case_, regardless if the incoming `Option` is a `Some` or `None`.

### Flatten
```csharp
Option<Option<int>> nestedOption = Option.From(Option.From(2));
Option<int> result = nestedOption.Flatten();
```
Flattens a nested `Option`.