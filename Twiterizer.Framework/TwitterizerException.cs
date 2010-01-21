/*
 * This file is part of the Twitterizer library <http://code.google.com/p/twitterizer/>
 *
 * Copyright (c) 2010, Patrick "Ricky" Smith <ricky@digitally-born.com>
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, are 
 * permitted provided that the following conditions are met:
 *
 * - Redistributions of source code must retain the above copyright notice, this list 
 *   of conditions and the following disclaimer.
 * - Redistributions in binary form must reproduce the above copyright notice, this list 
 *   of conditions and the following disclaimer in the documentation and/or other 
 *   materials provided with the distribution.
 * - Neither the name of the Twitterizer nor the names of its contributors may be 
 *   used to endorse or promote products derived from this software without specific 
 *   prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE.
 */
namespace Twitterizer.Framework
{
    using System;
    using System.Text.RegularExpressions;

    public class TwitterizerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterizerException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="requestData">The request data.</param>
        public TwitterizerException(string message, TwitterRequestData requestData)
            : base(message)
        {
            this.RequestData = requestData;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterizerException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="requestData">The request data.</param>
        /// <param name="innerException">The inner exception.</param>
        public TwitterizerException(string message, TwitterRequestData requestData, Exception innerException)
            : base(message, innerException)
        {
            this.RequestData = requestData;
        }

        /// <summary>
        /// Contains the Request Data that is used in the Twitter API request.
        /// </summary>
        /// <value>The request data.</value>
        public TwitterRequestData RequestData { get; set; }

        /// <summary>
        /// Contains the raw xml returned by the Twitter API.
        /// </summary>
        /// <value>The raw XML.</value>
        public string RawXML
        {
            get
            {
                return this.RequestData == null ? string.Empty : this.RequestData.Response;
            }
        }

        /// <summary>
        /// Parses the error message.
        /// </summary>
        public static string ParseErrorMessage(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return string.Empty;
            }

            if (!Regex.IsMatch(xml, "<error>.+</error>", RegexOptions.IgnoreCase))
            {
                return string.Empty;
            }

            return Regex.Match(xml, "(?:<error>).+(?:</error>)", RegexOptions.IgnoreCase).Value;
        }
    }
}
