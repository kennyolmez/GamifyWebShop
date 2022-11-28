using Newtonsoft.Json;

namespace Web.Models.ServicePointAggregate
{
    public class Address
    {
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("streetName")]
        public string StreetName { get; set; }

        [JsonProperty("streetNumber")]
        public string StreetNumber { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }
    }
}
