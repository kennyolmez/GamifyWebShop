using Newtonsoft.Json;

namespace Web.Models.ServicePointAggregate
{
    public class ServicePoint
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("deliveryAddress")]
        public Address DeliveryAddress { get; set; }
    }
}
