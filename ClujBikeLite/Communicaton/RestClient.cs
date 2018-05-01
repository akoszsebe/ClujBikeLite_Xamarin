using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using ClujBikeLite.Models;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ClujBikeLite.Communicaton
{
    class RestClient
    {
        static string ip = "portal.clujbike.eu";//"allamvizsga-akoszsebe.c9users.io";//"portal.clujbike.eu";
        static string port = "";//':8080';
        static string protocol = "http";



        public static StationsData GetStationsData()
        {
            var api_url = "/Station/Read";
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] data = encoder.GetBytes("");
            HttpWebRequest request = WebRequest.Create(@"" + protocol + "://" + ip + "" + port + api_url) as HttpWebRequest;
            request.ContentType = "application/x-www-form-urlencoded";//"application/json"  //"application/x-www-form-urlencoded"
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.Expect = "application/json";
            request.Timeout = 5000;


            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return null;
                    }

                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        if (string.IsNullOrWhiteSpace(content))
                        {
                            response.Close();
                            return null;
                        }
                        else
                        {
                            response.Close();
                            return JsonConvert.DeserializeObject<StationsData>(content);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Android.Util.Log.Error("MYAPP", "exception", ex);
                return null;
            }
        }


        public static StationsData GetStationsDataMokk()
        {
            string sationData = @"{ Data: [
                                            {
                                                StationName: 'Biblioteca Centrala',
                                                Address: 'Biblioteca Județeană Octavian Goga',
                                                OcuppiedSpots: 8,
                                                EmptySpots: 10,
                                                MaximumNumberOfBikes: 18,
                                                LastSyncDate: '28.04.2018 19:04',
                                                IdStatus: 1,
                                                Status: 'Functionala',
                                                StatusType: 'Online',
                                                Latitude: 46.777037,
                                                Longitude: 23.615109,
                                                IsValid: true,
                                                CustomIsValid: false,
                                                Notifies: [],
                                                Id: 85
                                            },
                                            {
                                                StationName: 'Piata Detunata',
                                                Address: 'Piata Detunata\r\n',
                                                OcuppiedSpots: 14,
                                                EmptySpots: 6,
                                                MaximumNumberOfBikes: 20,
                                                LastSyncDate: '28.04.2018 19:04',
                                                IdStatus: 1,
                                                Status: 'Functionala',
                                                StatusType: 'Online',
                                                Latitude: 46.767918,
                                                Longitude: 23.629864,
                                                IsValid: true,
                                                CustomIsValid: false,
                                                Notifies: [],
                                                Id: 86
                                            },
                                            {
                                                StationName: 'Calea Floresti',
                                                Address: 'Calea Floresti',
                                                OcuppiedSpots: 1,
                                                EmptySpots: 19,
                                                MaximumNumberOfBikes: 20,
                                                LastSyncDate: '28.04.2018 19:04',
                                                IdStatus: 1,
                                                Status: 'Functionala',
                                                StatusType: 'Online',
                                                Latitude: 46.75762,
                                                Longitude: 23.545946,
                                                IsValid: true,
                                                CustomIsValid: false,
                                                Notifies: [],
                                                Id: 87
                                            }
                                        ],
                                        Total: 53,
                                        AggregateResults: null,
                                        Errors: null
                                    }";

            return JsonConvert.DeserializeObject<StationsData>(sationData);
        }
    }
}