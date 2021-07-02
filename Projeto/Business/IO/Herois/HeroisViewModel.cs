using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Business.IO.Herois
{
    public class HeroisViewModel
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Copyright { get; set; }
        public string AttributionText { get; set; }
        public string AttributionHTML { get; set; }
        public Data Data { get; set; }
    }
    public class Data
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public IEnumerable<Results> Results { get; set; }

    }
    public class Results
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Modified { get; set; }
        public bool Favorito { get; set; }
        public String ResourceURI { get; set; }
        [JsonIgnore]
        public IEnumerable<Urls> Urls { get; set; }
        [JsonIgnore]
        public Thumbnail Thumbnail { get; set; }
        [JsonIgnore]
        public Comics Comics { get; set; }
        [JsonIgnore]
        public Stories Stories { get; set; }
        [JsonIgnore]
        public Events Events { get; set; }
        [JsonIgnore]
        public Series Series { get; set; }
    }
    public class Urls
    {
        public string Type { get; set; }
        public string Url { get; set; }
    }
    public class Thumbnail
    {
        public string Path { get; set; }
        public string Extension { get; set; }
    }
     public class Comics
    {
        public int Available { get; set; }
        public int Returned { get; set; }
        public string CollectionURI { get; set; }
        public IEnumerable<Items> Items { get; set; }
    }
    public class Items
    {
        public string ResourceURI { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
    public class Stories
    {
        public int Available { get; set; }
        public int Returned { get; set; }
        public string CollectionURI { get; set; }
        public IEnumerable<Items> Items { get; set; }
    }
    public class Events
    {
        public int Available { get; set; }
        public int Returned { get; set; }
        public string CollectionURI { get; set; }
        public IEnumerable<Items> Items { get; set; }
    }
    public class Series
    {
        public int Available { get; set; }
        public int Returned { get; set; }
        public string CollectionURI { get; set; }
        public IEnumerable<Items> Items { get; set; }
    }
}
