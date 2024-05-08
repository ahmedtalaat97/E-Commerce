namespace E_Commerce_Core.Enities.OrderEntities
{
    public class OrderItem :BaseEntity<Guid>
    {
      public OrderItemProduct OrderItemProduct { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }


       

    }
}