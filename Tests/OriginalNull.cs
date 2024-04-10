using NullAlternatives.NullApproach;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var product = new Product
        {
            Name = "Null example",
            Price = 5.00,
        };

        if (product.Weight is not null)
        {
            // Logic here for physical delivery
        }
        else
        {
            // Logic here for digital delivery
        }

        // Even though we've already checked...

        if (product.Weight is not null)
        {
            // ...we've got to do it again for other operations
        }
    }
}