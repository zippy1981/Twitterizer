﻿//-----------------------------------------------------------------------
// <copyright file="TwitterListWrapper.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The twitter list wrapper class. </summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System.Runtime.Serialization;
    using Twitterizer.Core;

    /// <summary>
    /// A wrapper class for the lists collection (Twitter has their data structure wierd for this one)
    /// </summary>
    [DataContract]
    internal class TwitterListWrapper : BaseObject
    {
        /// <summary>
        /// The private variable for the Lists property
        /// </summary>
        private TwitterListCollection lists;

        /// <summary>
        /// Gets or sets the lists.
        /// </summary>
        /// <value>The lists.</value>
        [DataMember(Name = "lists")]
        public TwitterListCollection Lists
        {
            get
            {
                this.lists.RateLimiting = this.RateLimiting;
                this.lists.Tokens = this.Tokens;

                return this.lists;
            }

            set
            {
                this.lists = value;
            }
        }

        /// <summary>
        /// Gets or sets the next cursor.
        /// </summary>
        /// <value>The next cursor.</value>
        [DataMember(Name = "next_cursor")]
        public int NextCursor { get; set; }

        /// <summary>
        /// Gets or sets the previous cursor.
        /// </summary>
        /// <value>The previous cursor.</value>
        [DataMember(Name = "previous_cursor")]
        public int PreviousCursor { get; set; }
    }
}
