﻿//-----------------------------------------------------------------------
// <copyright file="TwitterSearchResultWrapper.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The twitter search result wrapper class</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using Newtonsoft.Json;
    using Twitterizer.Core;

    /// <summary>
    /// The Twitter search result wrapper class
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class TwitterSearchResultWrapper : TwitterObject
    {
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        [JsonProperty(PropertyName = "results")]
        public TwitterSearchResultCollection Results { get; set; }

        /// <summary>
        /// Gets or sets the max id.
        /// </summary>
        /// <value>The max id.</value>
        [JsonProperty(PropertyName = "max_id")]
        public long MaxId { get; set; }

        /// <summary>
        /// Gets or sets the since id.
        /// </summary>
        /// <value>The since id.</value>
        [JsonProperty(PropertyName = "since_id")]
        public long SinceId { get; set; }

        /// <summary>
        /// Gets or sets the refresh query string.
        /// </summary>
        /// <value>The refresh query string.</value>
        [JsonProperty(PropertyName = "refresh_url")]
        public string RefreshQueryString { get; set; }

        /// <summary>
        /// Gets or sets the next page query string.
        /// </summary>
        /// <value>The next page query string.</value>
        [JsonProperty(PropertyName = "next_page")]
        public string NextPageQueryString { get; set; }

        /// <summary>
        /// Gets or sets the results per page.
        /// </summary>
        /// <value>The results per page.</value>
        [JsonProperty(PropertyName = "results_per_page")]
        public int ResultsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>The page number.</value>
        [JsonProperty(PropertyName = "page")]
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the completed in.
        /// </summary>
        /// <value>The completed in.</value>
        [JsonProperty(PropertyName = "completed_in")]
        public double CompletedIn { get; set; }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>The query.</value>
        [JsonProperty(PropertyName = "query")]
        public string Query { get; set; }
    }
}
