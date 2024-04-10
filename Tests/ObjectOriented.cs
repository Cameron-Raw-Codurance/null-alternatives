using NullAlternatives.ObjectOriented;
using NullAlternatives.ObjectOriented.Inheritance;
using NullAlternatives.ObjectOriented.NullObject;

namespace Tests;

public class ObjectOriented
{
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
}