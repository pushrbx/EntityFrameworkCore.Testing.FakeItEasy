# EntityFrameworkCore.Testing.FakeItEasy [![Build status](https://ci.appveyor.com/api/projects/status/w7sc9oyr9b9a6n2h?svg=true)](https://ci.appveyor.com/project/pushrbx/entityframeworkcore-testing-fakeiteasy)

Simple EntityFrameworkCore FakeItEasy utility class based on [EntityFramework.Testing.FakeItEasy](https://github.com/mgibas/EntityFramework.Testing.FakeItEasy).

### Getting Started:

- Creating fake DbSet<T>:
```csharp
var fakeDbSet = Aef.FakeDbSet(new List<Model>{...});
A.CallTo(() => context.Models).Returns(fakeDbSet);
```

```csharp
var fakeDbSet =  Aef.FakeDbSet<Model>(55); //55 Model fakes created by FakeItEasy
A.CallTo(() => context.Models).Returns(fakeDbSet);
```

```csharp
var fakeDbSet = Aef.FakeDbSet<Model>(); //Empty collection
A.CallTo(() => context.Models).Returns(fakeDbSet);
```

### Disclaimer

The TestAsync classes are from: https://stackoverflow.com/questions/40476233/how-to-mock-an-async-repository-with-entity-framework-core/40491640#40491640