using NullAlternatives.Functional;
using NullAlternatives.Functional.UsingOption;

namespace Tests.UsingOption;

// Using Option type that was made in this repo

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
}