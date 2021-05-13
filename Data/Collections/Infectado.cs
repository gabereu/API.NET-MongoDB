using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Api.Data.Collections
{
    public class Infectado
    {
        public DateTime DataNascimento {get; set;}
        public string Sexo {get; set;}
        public GeoJson2DGeographicCoordinates Localização {get; set;}

        public Infectado(DateTime dataNascimento, string sexo, double latitude, double longitude)
        {
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Localização = new GeoJson2DGeographicCoordinates(longitude: longitude, latitude: latitude);
        }
    }
}