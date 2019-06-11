# Ninstance

Create constructor agnostic test instances on the fly, in the blink of an eye...

Use

```c#
// arrange
var service = Instance.Of<FavouriteSongService>();

// act
```

instead of

```c#
// arrange
var service = new FavouriteSongService(
    new ThisService(),
    new ThatService(),
    new ActuallyUsefulService(),
    new AddedService(),
    new ForgottenService(),
    new VeryImportantService(),
    new DontCareService()
);

// act
```

and never look back. Ninstance will instantiate `FavouriteSongService` with substitutes. Stop fixing hundreds of tests when you change the signature of it.

Or, when you do want to pass dependencies, just do

```c#
// arrange
var actuallyUsefulService = Substitute.For<IActuallyUsefulService>();
    actuallyUsefulService.Jump().Returns(true);

var veryImportantService = new VeryImportantService>(42);

var service = Instance.Of<CarService>(actuallyUsefulService, veryImportantService);

// act
```

instead of

```c#
// arrange
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

// act
```

and move on.
