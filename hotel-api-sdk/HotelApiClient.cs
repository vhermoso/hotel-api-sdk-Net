﻿using System;
using System.Text;

using com.hotelbeds.distribution.hotel_api_model.auto.messages;
using com.hotelbeds.distribution.hotel_api_sdk.config;
using com.hotelbeds.distribution.hotel_api_sdk.types;
using System.Net.Http;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO.Compression;
using System.IO;
using System.Threading.Tasks;

namespace com.hotelbeds.distribution.hotel_api_sdk
{
    /// <summary>
    /// 
    /// </summary>
    public class HotelApiClient
    {
        //https://developer.hotelbeds.com/docs/read/apitude_booking/

        /// <summary>
        /// Atributos
        /// </summary>
        private readonly TimeSpan timeout;
        private readonly string basePath;
        private readonly string baseSecurePath;
        private readonly HotelApiVersion version;
        private readonly string apiKey;
        private readonly string sharedSecret;
        private string environment;

        public HotelApiClient() : this(new HotelApiVersion(HotelApiVersion.versions.V1))
        {
        }

        public HotelApiClient(HotelApiVersion version) : this(version, null, null, false)
        {
            this.apiKey = GetHotelApiKeyFromConfig();
            this.sharedSecret = GetHotelSharedSecretFromConfig();
            CheckHotelApiClientConfig();
        }

        public HotelApiClient(string apiKey, string sharedSecret) : this(new HotelApiVersion(HotelApiVersion.versions.V1), apiKey, sharedSecret)
        {
        }

        public HotelApiClient(HotelApiVersion version, string apiKey, string sharedSecret) : this(version, apiKey, sharedSecret, true)
        {
        }

        private HotelApiClient(HotelApiVersion version, string apiKey, string sharedSecret, Boolean validate)
        {
            this.timeout = GetTimeoutFromConfig();
            this.apiKey = apiKey;
            this.sharedSecret = sharedSecret;
            this.version = version;
            config.Environment currentEnvironment = GetEnvironment();
            if (currentEnvironment != null)
            {
                this.basePath = currentEnvironment.BaseUrl;
                this.baseSecurePath = (currentEnvironment.BaseSecureUrl != null) ? currentEnvironment.BaseSecureUrl : currentEnvironment.BaseUrl;
            }
            if (validate)
            {
                CheckHotelApiClientConfig();
            }
        }

        private void CheckHotelApiClientConfig()
        {
            if (String.IsNullOrEmpty(this.apiKey) || version == null || String.IsNullOrEmpty(this.basePath) || String.IsNullOrEmpty(this.baseSecurePath) || String.IsNullOrEmpty(this.sharedSecret))
                throw new Exception("HotelApiClient cannot be created without specifying an API key, Shared Secret, the Hotel API version and the service you are connecting to.", new ArgumentNullException());
        }

        private string GetHotelApiKeyFromConfig()
        {
            try
            {
                string returnValue = ConfigurationManager.AppSettings.Get("ApiKey");

                return returnValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private TimeSpan GetTimeoutFromConfig()
        {
            try
            {
                int returnValue = int.Parse(ConfigurationManager.AppSettings.Get("TimeoutSeconds"));
                return TimeSpan.FromSeconds(returnValue);
            }
            catch
            {
                // In case the client updated the version and did not configure the new parameter
                return TimeSpan.FromSeconds(5000);
            }
        }

        private string GetHotelSharedSecretFromConfig()
        {
            try
            {
                string returnValue = ConfigurationManager.AppSettings.Get("SharedSecret");

                return returnValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private config.Environment GetEnvironment()
        {
            try
            {
                config.Environment returnValue = null;
                String environmentName = ConfigurationManager.AppSettings.Get("ENVIRONMENT");
                if (!String.IsNullOrEmpty(environmentName))
                {
                    EnvironmentsSection environmentsSection = (EnvironmentsSection)ConfigurationManager.GetSection("environments");
                    List<config.Environment> environments = new List<config.Environment>(environmentsSection.Environments);
                    returnValue = environments.Find(x => x.Name == environmentName);
                }
                return returnValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public StatusRS status()
        {
            return this.statusAsync().Result;
        }
        public Task<StatusRS> statusAsync()
        {
            HotelApiPaths.STATUS avail = new HotelApiPaths.STATUS();
            return callRemoteApiAsync<StatusRS, Object>(null, avail, null, this.version);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AvailabilityRS doAvailability(AvailabilityRQ request)
        {
            return doAvailabilityAsync(request).Result;
        }

        public Task<AvailabilityRS> doAvailabilityAsync(AvailabilityRQ request)
        {
            try
            {
                HotelApiPaths.AVAILABILITY avail = new HotelApiPaths.AVAILABILITY();
                return callRemoteApiAsync<AvailabilityRS, AvailabilityRQ>(request, avail, null, this.version);
                //return response;
            }
            catch (HotelSDKException e)
            {
                throw e;
            }
        }

        public CheckRateRS doCheck(CheckRateRQ checkRateRQ)
        {
            return doCheckAsync(checkRateRQ).Result;
        }

        public Task<CheckRateRS> doCheckAsync(CheckRateRQ checkRateRQ)
        {
            try
            {
                HotelApiPaths.CHECK_AVAIL checkRate = new HotelApiPaths.CHECK_AVAIL();
                return callRemoteApiAsync<CheckRateRS, CheckRateRQ>(checkRateRQ, checkRate, null, this.version);                
            }
            catch (HotelSDKException e)
            {
                throw e;
            }
        }

        public BookingRS confirm(BookingRQ bookingRQ)
        {
            return this.confirmAsync(bookingRQ).Result;
        }
        public Task<BookingRS> confirmAsync(BookingRQ bookingRQ)
        {
            try
            {
                HotelApiPaths.BOOKING_CONFIRM bookingConfirm = new HotelApiPaths.BOOKING_CONFIRM();
                HotelApiVersion version = this.version;
                string baseUrl = (bookingRQ != null && bookingRQ.paymentData != null) ? this.baseSecurePath : this.basePath;
                return callRemoteApiAsync<BookingRS, BookingRQ>(bookingRQ, bookingConfirm, null, version, baseUrl);
            }
            catch (HotelSDKException e)
            {
                throw e;
            }
        }

        public BookingCancellationRS Cancel(List<Tuple<string, string>> param)
        {
            return this.CancelAsync(param).Result;
        }
        public Task<BookingCancellationRS> CancelAsync(List<Tuple<string, string>> param)
        {
            try
            {
                HotelApiPaths.BOOKING_CANCEL bookingCancel = new HotelApiPaths.BOOKING_CANCEL();
                return callRemoteApiAsync<BookingCancellationRS, Tuple<string, string>[]>(null, bookingCancel, param, this.version);                
            }
            catch (HotelSDKException e)
            {
                throw e;
            }
        }

        public BookingDetailRS Detail(List<Tuple<string, string>> param)
        {
            return this.DetailAsync(param).Result;
        }

        public Task<BookingDetailRS> DetailAsync(List<Tuple<string, string>> param)
        {
            try
            {
                HotelApiPaths.BOOKING_DETAIL bookingDetail = new HotelApiPaths.BOOKING_DETAIL();
                return callRemoteApiAsync<BookingDetailRS, Tuple<string, string>[]>(null, bookingDetail, param, this.version);
                
            }
            catch (HotelSDKException e)
            {
                throw e;
            }
        }

        public BookingListRS List(List<Tuple<string, string>> param)
        {
            return this.ListAsync(param).Result;
        }

        public Task<BookingListRS> ListAsync(List<Tuple<string, string>> param)
        {
            try
            {
                HotelApiPaths.BOOKING_LIST bookingList = new HotelApiPaths.BOOKING_LIST();
                return callRemoteApiAsync<BookingListRS, Tuple<string, string>[]>(null, bookingList, param, this.version);
                
            }
            catch (HotelSDKException e)
            {
                throw e;
            }
        }

        private Task<T> callRemoteApiAsync<T, U>(U request, HotelApiPaths.HotelApiPathsBase path, List<Tuple<string, string>> param, HotelApiVersion version)
        {
            return this.callRemoteApiAsync<T, U>(request, path, param, version, this.basePath);
        }

        private async Task<T> callRemoteApiAsync<T, U>(U request, HotelApiPaths.HotelApiPathsBase path, List<Tuple<string, string>> param, HotelApiVersion version, string baseUrl)
        {
            try
            {
                T response = default(T);

                using (var client = new HttpClient(
                                        new HttpClientHandler()
                                        {
                                            AutomaticDecompression = System.Net.DecompressionMethods.GZip
                                        }))
                {
                    if (request == null && (path.GetType() != typeof(HotelApiPaths.STATUS)
                        && path.GetType() != typeof(HotelApiPaths.BOOKING_CANCEL) && path.GetType() != typeof(HotelApiPaths.BOOKING_DETAIL) && path.GetType() != typeof(HotelApiPaths.BOOKING_LIST)))
                        throw new Exception("Object request can't be null");

                    client.BaseAddress = new Uri(path.getUrl(baseUrl, version));
                    client.DefaultRequestHeaders.Clear();
                    client.Timeout = this.timeout;
                    client.DefaultRequestHeaders.Add("Api-Key", this.apiKey);

                    long ts = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds / 1000;
                    SHA256 hashstring = SHA256Managed.Create();
                    byte[] hash = hashstring.ComputeHash(Encoding.UTF8.GetBytes(this.apiKey + this.sharedSecret + ts));
                    string signature = BitConverter.ToString(hash).Replace("-", "");
                    client.DefaultRequestHeaders.Add("X-Signature", signature.ToString());
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json; charset=utf-8");

                    // GET Method
                    if (path.getHttpMethod() == HttpMethod.Get)
                    {
                        string Uri = path.getEndPoint();

                        if (param != null)
                        {
                            Uri = path.getEndPoint(param);
                        }

                        HttpResponseMessage resp = await client.GetAsync(Uri);
                        response = await resp.Content.ReadAsAsync<T>();
                        return response;
                    }

                    // DELETE Method
                    if (path.getHttpMethod() == HttpMethod.Delete)
                    {
                        string Uri = path.getEndPoint();

                        if (param != null)
                        {
                            Uri = path.getEndPoint(param);
                        }

                        HttpResponseMessage resp = await client.DeleteAsync(Uri);
                        response = await resp.Content.ReadAsAsync<T>();
                        return response;
                    }

                    StringContent contentToSend = null;
                    if (request != null)
                    {
                        string objectSerialized = JsonConvert.SerializeObject(request, Formatting.Indented, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
                        contentToSend = new StringContent(objectSerialized, Encoding.UTF8, "application/json");
                    }

                    if (path.getHttpMethod() == HttpMethod.Post)
                    {
                        HttpResponseMessage resp = null;
                        if (param == null)
                            resp = await client.PostAsync(path.getEndPoint(), contentToSend);
                        else
                            resp = await client.PostAsync(path.getEndPoint(param), contentToSend);

                        response = await resp.Content.ReadAsAsync<T>();
                    }
                }

                return response;
            }
            catch (HotelSDKException e)
            {
                throw e;
            }
        }
    }
}
