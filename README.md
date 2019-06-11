# Ninstance

Create constructor agnostic test instances on the fly, in the blink of an eye...

Use

```c#
var service = Instance.Of<FavouriteSongService>();
```

instead of

```c#
var service = new FavouriteSongService(
    new ThisService(),
    new ThatService(),
    new ActuallyUsefulService(),
    new AddedService(),
    new ForgottenService(),
    new VeryImportantService(),
    new DontCareService()
);
```

and never look back. Ninstance will take care of instantiate FavouriteSongService with substitutes. Stop fixing hundreds of tests when you change the signature of a class.

Or, when you do want to pass dependencies, just do

```c#
var actuallyUsefulService = Substitute.For<IActuallyUsefulService>();
actuallyUsefulService.Jump().Returns(true);

var veryImportantService = new VeryImportantService>(42);

var testCarSserviceervice = Instance.Of<ICarService>(actuallyUsefulService, veryImportantService);
```

instead of

```c#
var actuallyUsefulService = Substitute.For<IActuallyUsefulService>();
actuallyUsefulService.Jump().Returns(true);

var veryImportantService = new VeryImportantService>(42);

var service = new CarService(
    new ThisService(),
    new ThatService(),
    actuallyUsefulService,
    new AddedService(),
    new ForgottenService(),
    veryImportantService,
    new DontCareService()
);
```

and move on.
