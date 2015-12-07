(**
Basics
====================

`Option<T>` represents the absence or presence of a value. If a `Option<T>` contains a value, we'll call it `Some` from now on. If it is empty, we'll call it `None`.

*)
Option<int> result = Option.From(2);
(**

The above example would evaluate `2` and - because that is a valid integer - return a `Some` and store it in the variable `result`. The result of the calculation is stored inside the `Some` and can be accessed using the `TryGet` method:

*)

int value;
if(result.TryGet(out value))
    // use value;
(**

To find out if `result` represents a `Some` or `None` it provides the boolean property `HasValue`:

*)

if(result.HasValue) _two = i;
(**

Recommended usage
-------------------
Every method that may not return a value in some circumstances should return an `Option<T>` instead of the result directly. That way, eventual `null` references or the usage of "magic values" can be avoided.

So instead of this:

*)
string DoSomethingThatCouldReturnNull(string a) { /* ... */ };
int DoSomethingThatCouldReturnAMagicValue(string a) { /* ... */ };
(**

write this:

*)
Option<string> DoSomethingThatCouldReturnNull(string a) { /* ... */ };
Option<int> DoSomethingThatCouldReturnAMagicValue(string a) { /* ... */ };