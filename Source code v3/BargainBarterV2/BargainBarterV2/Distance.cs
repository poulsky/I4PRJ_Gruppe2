using System;
using System.Net;
using System.Xml.Linq;
using BargainBarterV2.Models;
//using Windows.Services.Maps;
//using Windows.Devices.Geolocation;

namespace BargainBarterV2
{
    public class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Coordinates()
        {
            
        }

        public Coordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }

    public static class CoordinatesDistanceExtensions
    {
        public static double DistanceTo(this Coordinates baseCoordinates, Coordinates targetCoordinates)
        {
            return DistanceTo(baseCoordinates, targetCoordinates, UnitOfLength.Kilometers);
        }

        public static double DistanceTo(this Coordinates baseCoordinates, Coordinates targetCoordinates,
            UnitOfLength unitOfLength)
        {
            var baseRad = Math.PI*baseCoordinates.Latitude/180;
            var targetRad = Math.PI*targetCoordinates.Latitude/180;
            var theta = baseCoordinates.Longitude - targetCoordinates.Longitude;
            var thetaRad = Math.PI*theta/180;

            double dist =
                Math.Sin(baseRad)*Math.Sin(targetRad) + Math.Cos(baseRad)*
                Math.Cos(targetRad)*Math.Cos(thetaRad);
            dist = Math.Acos(dist);

            dist = dist*180/Math.PI;
            dist = dist*60*1.1515;

            return unitOfLength.ConvertFromMiles(dist);
        }

        public static Coordinates GetCoordinates(Address address)
        {
            Coordinates cord = new Coordinates(0, 0);

            string adresse = address.StreetNumber + " " + address.StreetName + ", " + address.PostalCode;
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false",
                Uri.EscapeDataString(adresse));
            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var xdoc = XDocument.Load(response.GetResponseStream());

            var xElement = xdoc.Element("GeocodeResponse");
            if (xElement != null)
            {
                var result = xElement.Element("result");
                var locationElement = result.Element("geometry").Element("location");
                var lat = locationElement.Element("lat");
                var lng = locationElement.Element("lng");


                cord.Latitude = ((double) lat);
                cord.Longitude = ((double) lng);

            }

            return cord;
        }

        public static Coordinates GetCoordinates(string address)
        {
            Coordinates cord = new Coordinates(0, 0);

            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false",
                Uri.EscapeDataString(address));
            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var xdoc = XDocument.Load(response.GetResponseStream());

            var xElement = xdoc.Element("GeocodeResponse");
            if (xElement != null)
            {
                var result = xElement.Element("result");
                var locationElement = result.Element("geometry").Element("location");
                var lat = locationElement.Element("lat");
                var lng = locationElement.Element("lng");


                cord.Latitude = ((double)lat);
                cord.Longitude = ((double)lng);

            }

            return cord;
        }



    }

    public class UnitOfLength
    {
        public static UnitOfLength Kilometers = new UnitOfLength(1.609344);
        public static UnitOfLength NauticalMiles = new UnitOfLength(0.8684);
        public static UnitOfLength Miles = new UnitOfLength(1);

        private readonly double _fromMilesFactor;

        private UnitOfLength(double fromMilesFactor)
        {
            _fromMilesFactor = fromMilesFactor;
        }

        public double ConvertFromMiles(double input)
        {
            return input * _fromMilesFactor;
        }
    }
}