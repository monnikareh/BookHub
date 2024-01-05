namespace DataAccessLayer.Entities;

public enum PaymentStatus
{
    Unpaid,
    Paid,
    AwaitingShipment,
    Shipped,
    Delivered
}