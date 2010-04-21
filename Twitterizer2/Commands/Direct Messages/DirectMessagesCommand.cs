﻿//-----------------------------------------------------------------------
// <copyright file="DirectMessagesCommand.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The Direct Messages Command class.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer.Commands
{
    using System;
    using System.Globalization;
    using Twitterizer;
    using Twitterizer.Core;

    /// <summary>
    /// The Direct Messages Command
    /// </summary>
    internal sealed class DirectMessagesCommand : TwitterCommand<TwitterDirectMessageCollection>
    {
        /// <summary>
        /// The base address to the API method.
        /// </summary>
        private const string Path = "http://api.twitter.com/1/direct_messages.json";

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectMessagesCommand"/> class.
        /// </summary>
        /// <param name="tokens">The request tokens.</param>
        public DirectMessagesCommand(OAuthTokens tokens)
            : base("GET", new Uri(Path), tokens)
        {
            if (tokens == null)
            {
                throw new ArgumentNullException("tokens");
            }
        }
        #endregion

        #region API Properties
        /// <summary>
        /// Gets or sets the since status id.
        /// </summary>
        /// <value>The since status id.</value>
        public long SinceStatusId { get; set; }

        /// <summary>
        /// Gets or sets the max status id.
        /// </summary>
        /// <value>The max status id.</value>
        public long MaxStatusId { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; set; }
        #endregion

        /// <summary>
        /// Initializes the command.
        /// </summary>
        public override void Init()
        {
            if (this.SinceStatusId > 0)
                this.RequestParameters.Add("since_id", this.SinceStatusId.ToString(CultureInfo.InvariantCulture));

            if (this.MaxStatusId > 0)
                this.RequestParameters.Add("max_id", this.MaxStatusId.ToString(CultureInfo.InvariantCulture));

            if (this.Count > 0)
                this.RequestParameters.Add("count", this.Count.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public override void Validate()
        {
            this.IsValid = this.Tokens != null;
        }
    }
}
