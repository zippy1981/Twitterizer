﻿//-----------------------------------------------------------------------
// <copyright file="TwitterStatus.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The TwitterStatus class</summary>
//-----------------------------------------------------------------------
namespace Twitterizer
{
    using System;
    using System.Diagnostics;
    using Newtonsoft.Json;
    using Twitterizer.Core;

    /// <include file='TwitterStatus.xml' path='TwitterStatus/TwitterStatus/*'/>
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    [DebuggerDisplay("{User.ScreenName}/{Text}")]
    public class TwitterStatus : TwitterObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterStatus"/> class.
        /// </summary>
        public TwitterStatus()
            : base()
        {
        }

        #region Properties
        /// <summary>
        /// Gets or sets the status id.
        /// </summary>
        /// <value>The status id.</value>
        [JsonProperty(PropertyName = "id")]
        public decimal Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this status message is truncated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this status message is truncated; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "truncated")]
        public bool? IsTruncated { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        [JsonProperty(PropertyName = "created_at")]
        [JsonConverter(typeof(TwitterizerDateConverter))]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the screenName the status is in reply to.
        /// </summary>
        /// <value>The screenName.</value>
        [JsonProperty(PropertyName = "in_reply_to_screen_name")]
        public string InReplyToScreenName { get; set; }

        /// <summary>
        /// Gets or sets the user id the status is in reply to.
        /// </summary>
        /// <value>The user id.</value>
        [JsonProperty(PropertyName = "in_reply_to_user_id")]
        public decimal? InReplyToUserId { get; set; }

        /// <summary>
        /// Gets or sets the status id the status is in reply to.
        /// </summary>
        /// <value>The status id.</value>
        [JsonProperty(PropertyName = "in_reply_to_status_id")]
        public decimal? InReplyToStatusId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the authenticated user has favorited this status.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is favorited; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "favorited")]
        public bool? IsFavorited { get; set; }

        /// <summary>
        /// Gets or sets the text of the status.
        /// </summary>
        /// <value>The status text.</value>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user that posted this status.</value>
        [JsonProperty(PropertyName = "user")]
        public TwitterUser User { get; set; }

        /// <summary>
        /// Gets or sets the retweeted status.
        /// </summary>
        /// <value>The retweeted status.</value>
        [JsonProperty(PropertyName = "retweeted_status")]
        public TwitterStatus RetweetedStatus { get; set; }

        /// <summary>
        /// Gets or sets the place.
        /// </summary>
        /// <value>The place.</value>
        [JsonProperty(PropertyName = "place")]
        public TwitterPlace Place { get; set; }

        /// <summary>
        /// Gets or sets the geo location data.
        /// </summary>
        /// <value>The geo location data.</value>
        [JsonProperty(PropertyName = "geo")]
        public TwitterGeo Geo { get; set; }

        /// <summary>
        /// Gets or sets the entities.
        /// </summary>
        /// <value>The entities.</value>
        [JsonProperty(PropertyName  = "entities")]
        [JsonConverter(typeof(Entities.TwitterEntityCollection.Converter))]
        public Entities.TwitterEntityCollection Entities { get; set; }
        #endregion

        /// <summary>
        /// Updates the authenticated user's status to the supplied text.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="text">The status text.</param>
        /// <returns>A <see cref="TwitterStatus"/> object of the newly created status.</returns>
        public static TwitterStatus Update(OAuthTokens tokens, string text)
        {
            return Update(tokens, text, null);
        }

        /// <summary>
        /// Updates the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="text">The status text.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A <see cref="TwitterStatus"/> object of the newly created status.
        /// </returns>
        public static TwitterStatus Update(OAuthTokens tokens, string text, StatusUpdateOptions options)
        {
            return CommandPerformer<TwitterStatus>.PerformAction(new Commands.UpdateStatusCommand(tokens, text, options));
        }

        /// <summary>
        /// Deletes the specified status.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <param name="id">The status id.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A <see cref="TwitterStatus"/> object of the deleted status.
        /// </returns>
        public static TwitterStatus Delete(OAuthTokens tokens, decimal id, OptionalProperties options)
        {
            Commands.DeleteStatusCommand command = new Twitterizer.Commands.DeleteStatusCommand(tokens, id, options);

            return CommandPerformer<TwitterStatus>.PerformAction(command);
        }

        /// <summary>
        /// Deletes the specified status.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <param name="id">The status id.</param>
        /// <returns>A <see cref="TwitterStatus"/> object of the deleted status.</returns>
        public static TwitterStatus Delete(OAuthTokens tokens, decimal id)
        {
            return Delete(tokens, id, null);
        }

        /// <summary>
        /// Returns a single status, with user information, specified by the id parameter.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="options">The options.</param>
        /// <returns>A <see cref="TwitterStatus"/> instance.</returns>
        public static TwitterStatus Show(OAuthTokens tokens, decimal statusId, OptionalProperties options)
        {
            return CommandPerformer<TwitterStatus>.PerformAction(new Commands.ShowStatusCommand(tokens, statusId, options));
        }

        /// <summary>
        /// Returns a single status, with user information, specified by the id parameter.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <returns>A <see cref="TwitterStatus"/> instance.</returns>
        public static TwitterStatus Show(OAuthTokens tokens, decimal statusId)
        {
            return Show(tokens, statusId, null);
        }

        /// <summary>
        /// Returns a single status, with user information, specified by the id parameter.
        /// </summary>
        /// <param name="statusId">The status id.</param>
        /// <returns>A <see cref="TwitterStatus"/> instance.</returns>
        public static TwitterStatus Show(decimal statusId)
        {
            return Show(null, statusId);
        }

        /// <summary>
        /// Retweets a tweet. Requires the id parameter of the tweet you are retweeting. (say that 5 times fast)
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="options">The options.</param>
        /// <returns>A <see cref="TwitterStatus"/> representing the newly created tweet.</returns>
        public static TwitterStatus Retweet(OAuthTokens tokens, decimal statusId, OptionalProperties options)
        {
            return CommandPerformer<TwitterStatus>.PerformAction(
                new Commands.RetweetCommand(tokens, statusId, options));
        }

        /// <summary>
        /// Retweets a tweet. Requires the id parameter of the tweet you are retweeting. (say that 5 times fast)
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <returns>A <see cref="TwitterStatus"/> representing the newly created tweet.</returns>
        public static TwitterStatus Retweet(OAuthTokens tokens, decimal statusId)
        {
            return Retweet(tokens, statusId, null);
        }

        /// <summary>
        /// Returns up to 100 of the first retweets of a given tweet.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A <see cref="TwitterStatusCollection"/> instance.
        /// </returns>
        public static TwitterStatusCollection Retweets(OAuthTokens tokens, decimal statusId, RetweetsOptions options)
        {
            return CommandPerformer<TwitterStatusCollection>.PerformAction(
                new Commands.RetweetsCommand(tokens, statusId, options));
        }

        /// <summary>
        /// Returns up to 100 of the first retweets of a given tweet.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="statusId">The status id.</param>
        /// <returns>A <see cref="TwitterStatusCollection"/> instance.</returns>
        public static TwitterStatusCollection Retweets(OAuthTokens tokens, decimal statusId)
        {
            return Retweets(tokens, statusId, null);
        }

        /// <summary>
        /// Retweets a tweet. Requires the id parameter of the tweet you are retweeting. (say that 5 times fast)
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A <see cref="TwitterStatus"/> representing the newly created tweet.
        /// </returns>
        public TwitterStatus Retweet(OAuthTokens tokens, OptionalProperties options)
        {
            return Retweet(tokens, this.Id, options);
        }

        /// <summary>
        /// Retweets a tweet. Requires the id parameter of the tweet you are retweeting. (say that 5 times fast)
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// A <see cref="TwitterStatus"/> representing the newly created tweet.
        /// </returns>
        public TwitterStatus Retweet(OAuthTokens tokens)
        {
            return Retweet(tokens, this.Id, null);
        }

        /// <summary>
        /// Deletes the status.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A <see cref="TwitterStatus"/> object of the deleted status.
        /// </returns>
        public TwitterStatus Delete(OAuthTokens tokens, OptionalProperties options)
        {
            return Delete(tokens, this.Id, options);
        }

        /// <summary>
        /// Deletes the status.
        /// </summary>
        /// <param name="tokens">The oauth tokens.</param>
        /// <returns>
        /// A <see cref="TwitterStatus"/> object of the deleted status.
        /// </returns>
        public TwitterStatus Delete(OAuthTokens tokens)
        {
            return Delete(tokens, this.Id, null);
        }
    }
}
