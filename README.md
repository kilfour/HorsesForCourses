# HorsesForCourses

> It's Only a Model


**HorsesForCourses** is a tiny, opinionated DDD reference domain you can use as a SUT for examples, demos, tests, and documentation.

- Ships as a **binary** package (DLL)
- Marked **development-only** 
- **Not for production use**

## Why
Spinning up a real DDD domain for every demo is busywork. Trivial Person/OrderLine examples are too anemic to be interesting. This package gives you a "good enough" domain with value objects, aggregates, and generic identities so you can focus on the thing you're actually building or explaining.

The model looks small, but it hides just enough complexity to stay interesting.

*See also:* [Domain Requirements](./requirements.md).

Its most deceptively simple rule: 'a coach can only be assigned if they're available', quickly becomes tricky once you imagine thousands of coaches and courses.
How do you efficiently ask, "Who's free next Thursday at 10?", without scanning the whole database?

It also contains a tiny inconsistency you might not notice at first glance.  
A missing guard that lets something slip through the cracks.  
*Can you spot it?*

## Installation
Reference the pre-release package from your **test** or **examples** project:

```xml
<ItemGroup>
  <PackageReference Include="HorsesForCourses" Version="0.0.1-preview" PrivateAssets="all" />
</ItemGroup>
```

* `PrivateAssets=all` keeps the reference local to your project and prevents transitively exposing it if you pack your project.

## License
This project is licensed under the [MIT License](https://github.com/kilfour/HorsesForCourses/blob/main/LICENSE).  

## Disclaimer

This package is for **development and educational purposes only**. Do not ship it to production. It's only a model.
