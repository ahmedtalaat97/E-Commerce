using System.Text.Json.Serialization;

namespace E_Commerce_Core.Enities.OrderEntities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatus
    {
        Pending, Failed , Recived
    }
}
