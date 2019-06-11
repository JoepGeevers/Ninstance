# Ninstance
Quickly create an instances of your class for testing without being bothered with constructor parameters

Instead of

```
var testCarService = new CarService(
  new ThisService(),
  new ThatService(),
  new ActuallyUsefulService(),
  new AddedService(),
  new ForgottenService(),
  new VeryImportantService(),
  new DontCareService()
)
```

and fixing every single test when someone adds a new dependency

Just do

```
var testCarService = Instance.Of<CarService>();
```

and never look back. Ninstance will instantiate CarService with Substitute.For<>() for every argument.

Or, when you want to pass dependencies, instead of

```
var actuallyUsefulService = Substitute.For<IActuallyUsefulService>();
actuallyUsefulService.Jump().Returns(true);

var veryImportantService = Substitute.For<IVeryImportantService>();
veryImportantService.Guess().Returns(42);

 var testCarService = new CarService(
  new ThisService(),
  new ThatService(),
  actuallyUsefulService,
  new AddedService(),
  new ForgottenService(),
  veryImportantService,
  new DontCareService()
)

 ```
 
 Just do 
 
```
var actuallyUsefulService = Substitute.For<IActuallyUsefulService>();
actuallyUsefulService.Jump().Returns(true);

var veryImportantService = Substitute.For<IVeryImportantService>();
veryImportantService.Guess().Returns(42);

 var testCarService = Instance.Of<ICarService>(actuallyUsefulService, veryImportantService);

 ```
 
 And move on.
