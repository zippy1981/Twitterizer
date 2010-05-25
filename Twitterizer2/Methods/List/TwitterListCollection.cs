﻿//-----------------------------------------------------------------------
// <copyright file="TwitterListCollection.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The twitter list collection class.</summary>
//-----------------------------------------------------------------------

namespace Twitterizer
{
    using System;
    using Twitterizer.Core;

    /// <summary>
    /// The twitter list collection class.
    /// </summary>
    [Serializable]
    public class TwitterListCollection : Core.TwitterCollection<TwitterList>
    {
        /// <summary>
        /// Gets or sets the next cursor.
        /// </summary>
        /// <value>The next cursor.</value>
        public int NextCursor { get; set; }

        /// <summary>
        /// Gets or sets the previous cursor.
        /// </summary>
        /// <value>The previous cursor.</value>
        public int PreviousCursor { get; set; }

        /// <summary>
        /// Gets or sets the request parameters.
        /// </summary>
        /// <value>The request parameters.</value>
        public new OAuthTokens Tokens { get; set; }

        /// <summary>
        /// Gets or sets information about the user's rate usage.
        /// </summary>
        /// <value>The rate limiting object.</value>
        public new RateLimiting RateLimiting { get; set; }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        internal Core.TwitterCommand<TwitterListWrapper> Command { get; set; }

        /// <summary>
        /// Gets the next page.
        /// </summary>
        /// <returns>A <see cref="TwitterListCollection"/> instance.</returns>
        /// <value>The next page.</value>
        public TwitterListCollection NextPage()
        {
            CursorPagedCommand<TwitterListWrapper> newCommand =
                (CursorPagedCommand<TwitterListWrapper>)this.Command.Clone();
            newCommand.Cursor = this.NextCursor;

            TwitterListWrapper result = CommandPerformer<TwitterListWrapper>.PerformAction(newCommand);
            result.Lists.Command = newCommand;
            return result.Lists;
        }

        /// <summary>
        /// Gets the previous page.
        /// </summary>
        /// <returns>A <see cref="TwitterListCollection"/> instance.</returns>
        /// <value>The previous page.</value>
        public TwitterListCollection PreviousPage()
        {
            CursorPagedCommand<TwitterListWrapper> newCommand =
                (CursorPagedCommand<TwitterListWrapper>)this.Command.Clone();
            newCommand.Cursor = this.PreviousCursor;

            TwitterListWrapper result = Core.CommandPerformer<TwitterListWrapper>.PerformAction(newCommand);
            result.Lists.Command = newCommand;
            return result.Lists;
        }
    }
}
