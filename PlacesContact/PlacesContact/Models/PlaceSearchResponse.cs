using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PlacesContact.Models
{
    public class PlaceSearchResponse
    {
        public string ID { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "formatted_address")]
        public string Address { get; set; }
        [XmlAttribute(AttributeName = "lat")]
        public double Latitude { get; set; }
        [XmlAttribute(AttributeName = "lng")]
        public double Longtitude { get; set; }
        [XmlAttribute(AttributeName = "place_id")]
        public string PlaceId { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string PlaceType { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string BusinessCityId { get; set; }
    }
}
