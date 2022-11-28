using Newtonsoft.Json;

namespace Web.Models.ServicePointAggregate
{
    public class ServicePointInformationRoot
    {
        [JsonProperty("servicePointInformationResponse")]
        public ServicePointInformation? ServicePointInformationResponse { get; set; }
    }
}
