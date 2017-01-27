## 5.0.0 - 2017-01-27
* CHANGED: Using `IsEmpty` or `HasValue` now throws.
* FEATURE: Added `net40` as compile target.
* FEATURE: `netstandard1.0` does not target the whole library anymore, just `System.Linq` and `System.Diagnostics.Debug`.
* FEATURE: Removed `TryGet` and `GetOrDefault`.

## 4.0.0 - 2016-12-29
* CHANGED: Using TryGet or GetOrDefault will not compile anymore.
* FEATURE: Added a `FromTryPattern` method without arguments.
* FEATURE: NeverNull is now a `netstandard 1.0` compatible library.

### 3.2.0 - 2015-12-27
* Match, IfSome and IfNone methods are implemented directly in Option<T> 
* Deprecated TryGet in favor of Match, IfSome and IfNone
* Deprecated GetOrDefault because it might reintroduce null
* Added Predef module for C# 6 static imports
* Fixed a bug with AggregateOptional and AggregateOptionalNullable [Issue #6](https://github.com/Bomret/NeverNull/issues/6)

### 3.1.0 - 2015-12-12
* Additional lazy overloads for GetOrElse and OrElse
* Additional overloads and methods for working with nullables

### 3.0.1 - 2015-12-08
* Fixed broken Nuget package

## 3.0.0 - 2015-12-07
* Complete rewrite

## 2.0.0.14 - 2014-08-25
* Last legacy build

