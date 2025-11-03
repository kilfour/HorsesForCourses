# HorsesForCourses

> It's Only a Model


**HorsesForCourses** is a tiny, opinionated DDD reference domain you can use as a SUT for examples, demos, tests, and documentation.

- Ships as a **binary** package (DLL)
- Marked **development-only** 
- **Not for production use**

## Why
Spinning up a real DDD domain for every demo is busywork. Trivial Person/OrderLine examples are too anemic to be interesting. This package gives you a "good enough" domain with value objects, aggregates, and generic identities so you can focus on the thing you're actually building or explaining.

## Installation
Reference the pre-release package from your **test** or **examples** project:

```xml
<ItemGroup>
  <PackageReference Include="HorsesForCourses" Version="0.0.1-preview" PrivateAssets="all" />
</ItemGroup>
```

* `PrivateAssets=all` keeps the reference local to your project and prevents transitively exposing it if you pack your project.

## Disclaimer

This package is for **development and educational purposes only**. Do not ship it to production. It's only a model.
