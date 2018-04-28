using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using ClujBikeLite.Models;
using System.Text;

namespace ClujBikeLite.Communicaton
{
    class RestClient
    {
        static string ip = "portal.clujbike.eu";//"allamvizsga-akoszsebe.c9users.io";//"192.168.0.106"//fekete feher szurke
        static string port = "";//":8080";
        static string protocol = "http";



        public static StationsData GetStationsData()
        {
            var api_url = "/Station/Read";
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] data = encoder.GetBytes("");
            HttpWebRequest request = WebRequest.Create(@"" + protocol + "://" + ip + "" + port + api_url) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.Expect = "application/json";
            request.Timeout = 3000;
            request.GetRequestStream().Write(data, 0, data.Length);

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
    }
}