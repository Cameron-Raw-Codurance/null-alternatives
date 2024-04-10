using NullAlternatives.ObjectOriented;
using NullAlternatives.ObjectOriented.Inheritance;

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
            }
        };

        #region switch case problem

        foreach (var product in products)
        {
            switch (product)
            {
                case PhysicalProduct physicalProduct:
                    deliveryHandler.DeliverPhysicalProduct(physicalProduct);
                    break;
                case DigitalProduct digitalProduct:
                    deliveryHandler.DeliverDigitalProduct(digitalProduct);
                    break;
            }
        }

        #endregion
        
        products.ForEach(p => p.Deliver(deliveryHandler));
        
        Assert.Pass();
    }
}