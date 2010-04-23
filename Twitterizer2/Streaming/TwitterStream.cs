﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Twitterizer
{
    public delegate void TwitterStatusReceivedHandler(object sender, TwitterStatus status);

    public class TwitterStream
    {
        private WebRequest request;
        private bool stopReceived;

        public string Username { get; set; }
        public string Password { get; set; }

        public event TwitterStatusReceivedHandler OnStatus;

        public TwitterStream(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public void StartStream()
        {
            request = WebRequest.Create("http://stream.twitter.com/1/statuses/sample.json");
            request.Credentials = new NetworkCredential(this.Username, this.Password);
            request.BeginGetResponse(ar =>
            {
                var req = (WebRequest)ar.AsyncState;
                // TODO: Add exception handling: EndGetResponse could throw
                using (var response = req.EndGetResponse(ar))
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    // This loop goes as long as twitter is streaming
                    while (!reader.EndOfStream)
                    {
                        if (this.stopReceived)
                        {
                            req.Abort();
                        }

                        try
                        {
                            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(TwitterStatus));
                            TwitterStatus resultObject = (TwitterStatus)ds.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(reader.ReadLine())));

                            if (this.OnStatus != null && resultObject != null && resultObject.Id > 0)
                            {
                                this.OnStatus(this, resultObject);
                            }

                        }
                        catch (SerializationException)
                        {
                        }
                    }
                }
            }, request);
        }

        public void EndStream()
        {
            this.stopReceived = true;
        }
    }
}
