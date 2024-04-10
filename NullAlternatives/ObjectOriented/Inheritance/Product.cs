namespace NullAlternatives.ObjectOriented.Inheritance;

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
