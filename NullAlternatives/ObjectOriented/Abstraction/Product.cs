namespace NullAlternatives.ObjectOriented.Abstraction;

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
