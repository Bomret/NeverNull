# NeverNull
A Option type that prevents using null in your code, inspired by its counterpart in the Scala programming language (http://www.scala-lang.org/).

Licensed under the Apache License 2.0 (http://www.apache.org/licenses/LICENSE-2.0.html).

## Example
Reading the content type of a url as string and printing it to the console. If the safe cast to `HttpWebRequest` would return `null` the subsequent calls to `Map` and `Filter` would not execute and *"No matching result"* would be printed to the console. The same would happen if any of the calls to `Map` would return null or the predicate `contentType.StartsWith("text")` would not hold in the Filter function.

```csharp
Option.Create(() => WebRequest.Create(Url) as HttpWebRequest)
      .Map(request => request.GetResponse())
      .Map(response => response.ContentType)
      .Filter(contentType => contentType.StartsWith("text"))
      .Match(
          contentType => Console.WriteLine("Success: {0}", contentType),
          () => Console.WriteLine("No matching result."));
```
------

## Basics
`Option` represents the absence or presence of `null`. If the `Create` function receives a reference to `null` or a `Func<T>` that produces `null`, a `None` is returned, otherwise a `Some`.

```csharp
var result = Option.Create(() => 2 + 4);
```

The above example would evaluate the anonymous function `() => 2 + 4` and, since a valid integer will be returned, return a `Some<int>` and store it in the variable `result`. The result of the calculation is stored inside the `Some` and can be accessed using the `Value` property:

```csharp
var six = result.Value;
```

To find out if `result` represents a `Some` or `None` it provides a boolean property: `HasValue`. To be safe, you can either check this property first or use the corresponding extension (more on extensions below):

```csharp
result.WhenSome(i => _six = i);
```

## Extensions and Combinators
Since `Some` and `None` are simple data structures, a couple of extension methods are provided that make working with both types easier.

### Extensions
An extension is either void or returns something different than a `Some` or `None`. Extensions can be used to execute actions with side effect.

#### WhenSome
```csharp
Option.Create(() => 2 + 3)
      .WhenSome(i => _five = i);
```

`WhenSome` is only executed if `() => 2 + 3` does not return `null` and delegates the value of the `Some` to its enclosed callback. In the above example `_five` would be a int with Value *5*.

#### WhenNone
```csharp
Option.Create(() => null)
      .WhenNone(() => _wasNull = true;);
```

`WhenNone` is only executed if `null` was returned and then executes its enclosed callback.

#### Match
```csharp
Option.Create(() => 2 + 3)
      .Match(
          i => _five = i,
         () => _wasNull = true);
```

`Match` allows to use a pattern matching like callback registration. The first function parameter is only executed in case of a `Some` and gets the value to work with. The second function parameter is registered to handle `None` and is only executed if `null` was returned.

#### Get
```csharp
var five = Option.Create(() => 2 + 3).Get();
```

`Get` is the most straight forward extension. It returns the value if the result is a `Some` or throws a NotSupportedException if it is a `None`. In the above example `five` would be *5*.

#### GetOrElse
```csharp
var five = Option.Create(() => 2 + 3).GetOrElse(-1);
```

`GetOrElse` either returns the value, if `Create` returned a `Some` or the else value, if `Create` returned a `None`. In the above example `five` would be *5*. It would have been *-1* if `() => 2 + 3` had returned `null`.

### Combinators
A combinator always returns a `Some` or `None` and thus lets you combine it with other combinators and allows function composition.
This library provides a growing number of combinators that empowers you to write concise and bloat-free code for error handling. Some of them, like `Map` and `Filter` have already been shown in the topmost example.

#### OrElse
```csharp
var result = Option.Create(() => null)
				   .OrElse(new Success<int>(-1));

// or

var result = Option.Create(() => null)
				   .OrElse(() => new Success<int>(-1));
```

In the above examples `null` would been returned and `result` would be a `None`. The 
`OrElse` combinator makes it possible to return a different `Option` in case of a `None`. In both cases above `result` would be a `Some<int>` with the Value *-1*.

#### Map
```csharp
var result = Option.Create(() => 2 + 3)
				   .Map(i => i.ToString());
```

`Map` allows to apply a function to the value of a `Some`. In the above example `result` would be a `Some<string>` with Value *"5"*.

#### Filter
```csharp
var result = Try.To(() => 2 + 3)
				.Filter(i => i == 5);
```

`Filter` checks if a given predicate holds true for an `Option`. In the above example `result` would be a `Some<int>` with Value *5*. If the predicate `i => i == 5` would not hold, `result` would have been a `None<int>`. If `() => 2 + 3` would have returned `null`, `result` would have been the original `None<int>`.

#### FlatMap
```csharp
var result = Option.Create(() => 2 + 3)
                   .FlatMap(i => Option.Create(() => i.ToString()));
```

`FlatMap` allows to apply a function to the value of a `Some` that returns another `Option` and avoid the nesting that would occur otherwise. In the above example `result` would be a `Some<string>` with Value *"5"*. If `Map` would have been used, `result` would have been a `Some<Some<string>>`.

#### Recover
```csharp
var result = Option.Create(() => null)
				   .Recover(() => -1);
```

This combinator is used to recover from a `None`. In the above example `result` would be a `Some<int>` with Value *-1*.