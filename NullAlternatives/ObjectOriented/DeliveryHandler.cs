using NullAlternatives.ObjectOriented.Inheritance;

namespace NullAlternatives.ObjectOriented;

public class DeliveryHandler
{
    private readonly CourierService _courierService;
    private readonly EmailService _emailService;

    public DeliveryHandler(CourierService courierService, EmailService emailService)
    {
        _courierService = courierService;
        _emailService = emailService;
    }

    public void DeliverPhysicalProduct(PhysicalProduct product)
    {
        _courierService.Send(product);
    }
    
    public void DeliverDigitalProduct(DigitalProduct product)
    {
        _emailService.Send(product);
    }
}

public class CourierService : IDeliveryService<PhysicalProduct>
{
    public void Send(PhysicalProduct product)
    {
        throw new NotImplementedException();
    }
}

public class EmailService : IDeliveryService<DigitalProduct>
{
    public void Send(DigitalProduct product)
    {
        throw new NotImplementedException();
    }
}

public interface IDeliveryService<in T> where T : Product
{
    public void Send(T product);
}