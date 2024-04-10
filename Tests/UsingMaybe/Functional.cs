using CSharpFunctionalExtensions;
using FluentAssertions;
using NullAlternatives.Functional.UsingMaybe;

namespace Tests.UsingMaybe;

// Using CSharpFunctionalExtensions for the Maybe type rather than the Option type in this repo

public class Functional
{
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
}