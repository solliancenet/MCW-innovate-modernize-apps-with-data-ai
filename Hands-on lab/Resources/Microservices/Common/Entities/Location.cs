using System;
using System.Text.Json.Serialization;

namespace Common.Entities
{
    public class Location
    {
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}