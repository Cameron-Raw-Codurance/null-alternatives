# Alternatives to null

This repo accompanied a presentation on the alternatives to using null frequently throughout a codebase.

The presentation is available for use to Codurance via [this link](https://docs.google.com/presentation/d/115Y80V6sK5NrhDoEIQziKybtFEYoRgINhMqN2IODrhA/edit#slide=id.gbf3c3ccaa8_0_2144).

## The Problem

The repo features a Product class, which in it's original "null oriented" design, uses a 
nullable double for it's Weight property. This means that null checks are necessary whenever
working with this property. 

```
public class Product 
{
    string Name { get; }
    string Description { get; }
    double Price { get; }
    double Weight? { get; }
}
```

### Improvement from the Domain Modeling perspective

The presentation makes a case for identifying improvements to how the Domain is modeled if
the use of null implies that it could be better. The improvement in this repo is represented
by two possible solutions; an abstract Product class and the IProduct interface:

```
// The inheritance alternative

public abstract class Product 
{
    public string Name { get; init; }
    public double Price { get; init; }
    public abstract void Deliver(DeliveryHandler deliveryHandler);
}

public class PhysicalProduct : Product
{
    public double Weight { get; init; }
    public override void Deliver(DeliveryHandler deliveryHandler) => 
        deliveryHandler.DeliverPhysicalProduct(this);
}

public class DigitalProduct : Product
{
    public override void Deliver(DeliveryHandler deliveryHandler) =>
        deliveryHandler.DeliverDigitalProduct(this);
}

// The interface alternative

public interface IProduct
{
    public string PerformDelivery();
}

class PhysicalProduct : IProduct
{
    public string PerformDelivery()
    {
        return "Delivered to physical address";
    }
}

class DigitalProduct : IProduct
{
    public string PerformDelivery()
    {
        return "Sent to email address";
    }
}
```

### Improvements to null from the perspective of Maintainability and Legibility

#### Object Oriented approach

The presentation and this repo make the case for the Null Object pattern (as mentioned in 
Martin Fowler's book, Refactoring). This is achieved in the repo by having a NullProduct
object, inheriting from the Product abstract class above. 

```
public class NullProduct : Product
{
    public override void Deliver(DeliveryHandler deliveryHandler)
    {
        // 
    }
}
```

There is an example in the Tests
project of how this "null object" can co-exist in iterators with "normal" Product objects
without causing disruption to the application flow.

```
    [Test]
    public void ObjectOrientedApproach()
    {
        var courierService = new CourierService();
        var emailService = new EmailService();
        
        var deliveryHandler = new DeliveryHandler(courierService, emailService);

        var products = new List<Product>
        {
            new DigitalProduct
            {
                Name = "Digital Product",
                Price = 5.00
            },
            new PhysicalProduct
            {
                Name = "Physical Product",
                Price = 10.00,
                Weight = 50.00
            },
            new NullProduct()
        };

        // Choosing this method over if / switch statements ensures we don't need to add a new case
        // if we introduce a new Product type
        products.ForEach(p => p.Deliver(deliveryHandler));
        
        Assert.Pass();
    }
```


#### Functional approach

This repo also includes the use of monads, which help to wrap or "quarantine" the nullable property,
and offer methods to use the potential value whilst also forcing the developer to consider and deal
with null scenarios. The repo has a simple Option type (named after the Rust equivalent) which was
written for the repo itself, but also has the Nuget package CSharpFunctionalExtensions which provides
(amongst other things) the Maybe and Result types.

The Option type written for this repo is as below:

```
public class Option<T>
{
   private readonly T? _value;
   private readonly bool _hasValue;

   private Option(T? value)
   {
      if (value is not null)
      {
         _hasValue = true;
         _value = value;
      }

      _hasValue = false;
   }

   public static Option<T> FromSome(T value) => new(value);
   public static Option<T> FromNone() => new(default);

   public Option<T> Some(Action<T> action)
   {
      if (_hasValue && _value is not null)
      {
         action(_value);
      }

      return this;
   }

   public Option<T> None(Action action)
   {
      if (!_hasValue)
      {
         action();
      }

      return this;
   }

   public bool IsSome() => _hasValue;

   public bool IsSomeAnd(Func<T, bool> action)
   {
      return _hasValue && action(_value!);
   }

   public bool IsNone() => !_hasValue;
}
```

Besides this, there are examples of the usages of these alternatives.

First, the custom written Option type:

```
    [Test]
    public void FunctionalApproach()
    {
        var products = new List<Product>
        {
            new()
            {
                Name = "Functional product",
                Price = 50.00,
                Weight = Option<double>.FromNone()
            },
            new() 
            {
                Name = "Physical product",
                Price = 30.00,
                Weight = Option<double>.FromSome(25.00)
            }
        };
        
        products.ForEach(product =>
        {
            product.Weight.Some(weight =>
            {
                // Case where weight is available - Only available within this scope
            }).None(() =>
            {
                // Case where weight is not available - Non existent in this scope
            });
            
            // Using popular libraries, we can perform more operations

            #region examples of further functionality

            if (product.Weight.IsSomeAnd(w => w > 0.00))
            {
                // Example of how else we can work with Option types
            }

            #endregion
        });
        
        Assert.Pass();
    }
```

And the Maybe type from CSharpFunctionalExtensions

```
 [Test]
    public void FunctionalApproach()
    {
        var products = new List<Product>
        {
            new()
            {
                Name = "Functional product",
                Price = 50.00,
                Weight = Maybe.None
            },
            new() 
            {
                Name = "Physical product",
                Price = 30.00,
                Weight = Maybe<double>.From(25.00)
            }
        };
        
        products.ForEach(product =>
        {
            product.Weight.Match(weight =>
            {

            }, () =>
            {

            });
            
            // Using popular libraries, we can perform more operations

            product.Weight.ToResult("No weight set")
                .Ensure(w => w > 0.00, (w) => $"Weight is not more than {w}")
                .Map(w => w * 10);

            product.Weight.Should().HaveValue(50.00);

            #region examples of further functionality

            if (product.Weight.TryGetValue(out double weight) && weight.Equals(5.00))
            {
                // Example of how else we can work with Option types
            }

            #endregion
        });
        
        Assert.Pass();
    }
```

As a bonus, there is also a brief introduction to Result types as an alternative / improvement to Exceptions.

Below, the repo highlights a major issue with the Exception model; methods do not _tell_ you
that they throw:

```
public static class MethodsThatThrow
{
   // Which one of these workers throws an Exception? Impossible to tell
   public static int ExceptionMethod1()
   {
      return ExceptionWorkers.DoWork1();
   } 
   public static int ExceptionMethod2()
   {
      return ExceptionWorkers.DoWork2();
   } 
   public static int ExceptionMethod3()
   {
      return ExceptionWorkers.DoWork3();
   } 
}

public static class ExceptionWorkers
{
   // There is no indication in the method signature that it throws Exceptions. You have to investigate, or 
   // trust that the package author has made it easy to see from within the IDE tooling
   public static int DoWork1()
   {
      throw new Exception();
   }

   public static int DoWork2()
   {
      return 200;
   }

   public static int DoWork3()
   {
      return 300;
   }
}
```

And that by contrast, Result types will be the return type and must have their errors handled:

```

public static class ResultsThatShow
{
   public static int ResultMethod1()
   {
      return ResultWorkers.DoWork1();
   } 
   
   // In contrast to methods that throw Exceptions, methods that return Results force the dev to handle
   // the unhappy path
   public static int ResultMethod2()
   {
      var answer = 0;
      if (ResultWorkers.DoWork2().TryGetValue(out var returnedInt))
      {
         answer = returnedInt;
      }

      return answer;
   } 
   public static int ResultMethod3()
   {
      return ResultWorkers.DoWork3();
   } 
}

public static class ResultWorkers
{
   public static int DoWork1()
   {
      return 400;
   }

   // Unlike methods that might throw Exceptions, methods that handle Results make it clear that there's an
   // error case by having a Result return type
   public static Result<int> DoWork2()
   {
      return Result.Success(500);
   }

   public static int DoWork3()
   {
      return 600;
   }
}
```
