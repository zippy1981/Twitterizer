﻿//-----------------------------------------------------------------------
// <copyright file="SerializationHelper.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://www.twitterizer.net/)
// 
//  Copyright (c) 2010, Patrick "Ricky" Smith (ricky@digitally-born.com)
//  All rights reserved.
//  
//  Redistribution and use in source and binary forms, with or without modification, are 
//  permitted provided that the following conditions are met:
// 
//  - Redistributions of source code must retain the above copyright notice, this list 
//    of conditions and the following disclaimer.
//  - Redistributions in binary form must reproduce the above copyright notice, this list 
//    of conditions and the following disclaimer in the documentation and/or other 
//    materials provided with the distribution.
//  - Neither the name of the Twitterizer nor the names of its contributors may be 
//    used to endorse or promote products derived from this software without specific 
//    prior written permission.
// 
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
//  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
//  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//  IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
//  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
//  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
//  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
//  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
//  POSSIBILITY OF SUCH DAMAGE.
// </copyright>
// <author>Ricky Smith</author>
// <summary>The serialization helper class.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.Core
{
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The Serialization Helper class. Provides a simple interface for common serialization tasks.
    /// </summary>
    /// <typeparam name="T">The type of object to be deserialized</typeparam>
    internal static class SerializationHelper<T>
        where T : ITwitterObject
    {
        /// <summary>
        /// The JavascriptConversionDelegate. The delegate is invokes when using the JavaScriptSerializer to manually construct a result object.
        /// </summary>
        /// <param name="value">Contains nested dictionary objects containing deserialized values for manual parsing.</param>
        /// <returns>A strongly typed object representing the deserialized data of type <typeparamref name="T"/></returns>
        public delegate T DeserializationHandler(JObject value);

        /// <summary>
        /// Deserializes the specified web response.
        /// </summary>
        /// <param name="webResponse">The web response.</param>
        /// <param name="deserializationHandler">The deserialization handler.</param>
        /// <returns>
        /// A strongly typed object representing the deserialized data of type <typeparamref name="T"/>
        /// </returns>
        public static T Deserialize(WebResponse webResponse, DeserializationHandler deserializationHandler)
        {
            try
            {
                T resultObject = default(T);

                // Get the response
                using (Stream responseStream = webResponse.GetResponseStream())
                {
                    byte[] data = ConversionUtility.ReadStream(responseStream);
#if DEBUG
                    Debug.WriteLine("----------- RESPONSE -----------");
                    Debug.WriteLine(Encoding.UTF8.GetString(data));
                    Debug.WriteLine("----------- END -----------");
#endif

                    // Deserialize the results.
                    resultObject = Deserialize(data, deserializationHandler);

                    responseStream.Close();

                    Trace.Write(resultObject, "Twitterizer2");
                }

                return resultObject;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deserializes the specified web response.
        /// </summary>
        /// <param name="webResponse">The web response.</param>
        /// <returns>
        /// A strongly typed object representing the deserialized data of type <typeparamref name="T"/>
        /// </returns>
        public static T Deserialize(WebResponse webResponse)
        {
            return Deserialize(webResponse, null);
        }

        /// <summary>
        /// Deserializes the specified serializer.
        /// </summary>
        /// <param name="data">The data to be deserialized.</param>
        /// <param name="javascriptConversionDeligate">The javascript conversion deligate.</param>
        /// <returns>
        /// A strongly typed object representing the deserialized data of type <typeparamref name="T"/>
        /// </returns>
        public static T Deserialize(byte[] data, DeserializationHandler javascriptConversionDeligate)
        {
            T resultObject = default(T);

            if (javascriptConversionDeligate == null)
            {
                resultObject = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(data));
            }
            else
            {
                resultObject = javascriptConversionDeligate((JObject)JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data)));
            }

            return resultObject;
        }

        /// <summary>
        /// Deserializes the specified serializer.
        /// </summary>
        /// <param name="data">The data to be deserialized.</param>
        /// <returns>
        /// A strongly typed object representing the deserialized data of type <typeparamref name="T"/>
        /// </returns>
        public static T Deserialize(byte[] data)
        {
            return Deserialize(data, null);
        }
    }
}
