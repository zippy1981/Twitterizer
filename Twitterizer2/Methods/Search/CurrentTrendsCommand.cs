﻿//-----------------------------------------------------------------------
// <copyright file="CurrentTrendsCommand.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://code.google.com/p/twitterizer/)
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
// <summary>The current trends command class</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.Commands
{
    using System;
    using Twitterizer;
    using Twitterizer.Core;

    /// <summary>
    /// The create list command class
    /// </summary>
    internal sealed class CurrentTrendsCommand : TwitterCommand<TwitterTrendTimeframe>
    {
        /// <summary>
        /// The base address to the API method.
        /// </summary>
        private const string Path = "http://api.twitter.com/1/trends/current.json";

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentTrendsCommand"/> class.
        /// </summary>
        /// <param name="excludeHashTags">if set to <c>true</c> [exclude hash tags].</param>
        public CurrentTrendsCommand(bool excludeHashTags)
            : base("GET", new Uri(Path), null)
        {
            this.ExcludeHashTags = excludeHashTags;
            this.DeserializationHandler = TwitterTrendTimeframe.DeserializeJson;
        }
        #endregion

        #region API Properties
        /// <summary>
        /// Gets or sets a value indicating whether [exclude hash tags].
        /// </summary>
        /// <value><c>true</c> if [exclude hash tags]; otherwise, <c>false</c>.</value>
        public bool ExcludeHashTags { get; set; }        
        #endregion

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public override void Init()
        {
            if (this.ExcludeHashTags)
            {
                this.RequestParameters.Add("exclude", "hashtags");
            }
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public override void Validate()
        {
            this.IsValid = true;
        }
    }
}
