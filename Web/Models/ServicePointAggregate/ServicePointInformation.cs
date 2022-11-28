using Newtonsoft.Json;

namespace Web.Models.ServicePointAggregate
{
    public class ServicePointInformation
    {
        [JsonProperty("servicePoints")]
        public List<ServicePoint> ServicePoints { get; set; }
    }
}
