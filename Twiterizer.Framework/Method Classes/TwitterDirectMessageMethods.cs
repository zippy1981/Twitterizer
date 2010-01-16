/*
 * This file is part of the Twitterizer library <http://code.google.com/p/twitterizer/>
 *
 * Copyright (c) 2008, Patrick "Ricky" Smith <ricky@digitally-born.com>
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
using System;
using System.Web;

namespace Twitterizer.Framework
{
	public class TwitterDirectMessageMethods : TwitterMethodBase
	{
		private readonly string userName;
		private readonly string password;
        private readonly string proxyUri = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterDirectMessageMethods"/> class.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        /// <param name="uri_proxy">The uri_proxy.</param>
        public TwitterDirectMessageMethods(string UserName, string Password, string ProxyUri) 
		{
			userName = UserName;
			password = Password;
            proxyUri = ProxyUri;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterDirectMessageMethods"/> class.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="Password">The password.</param>
        public TwitterDirectMessageMethods(string UserName, string Password)
        {
            userName = UserName;
            password = Password;
        }


		/// <summary>
		/// Returns a list of the 20 most recent direct messages sent to the authenticating user.
		/// </summary>
		/// <returns>A collection of <typeparamref name="TwitterStatus"/>TwitterStatus</returns> objects
		public TwitterStatusCollection DirectMessages()
		{
			return DirectMessages(null);
		}

		/// <summary>
		/// Returns a list of the most recent direct messages sent to the authenticating user.
		/// </summary>
		/// <param name="Parameters">Accepts Since, SinceID, and Page parameters</param>
		/// <returns></returns>
		public TwitterStatusCollection DirectMessages(TwitterParameters Parameters)
		{
			TwitterRequest Request = new TwitterRequest(proxyUri);
			TwitterRequestData Data = new TwitterRequestData();
			Data.UserName = userName;
			Data.Password = password;

            Data.ActionUri = this.BuildConditionalUrl(Parameters, "direct_messages.xml");

			Data = Request.PerformWebRequest(Data, "GET");

			return Data.Statuses;
		}



		/// <summary>
		/// Returns a list of the 20 most recent direct messages sent by the authenticating user.
		/// </summary>
		/// <returns></returns>
		public TwitterStatusCollection DirectMessagesSent()
		{
			return DirectMessagesSent(null);
		}

		/// <summary>
		/// Returns a list of the most recent direct messages sent by the authenticating user.
		/// </summary>
		/// <param name="Parameters">Accepts Since, SinceID, and Page parameters</param>
		/// <returns></returns>
		public TwitterStatusCollection DirectMessagesSent(TwitterParameters Parameters)
		{
            TwitterRequest Request = new TwitterRequest(proxyUri);
			TwitterRequestData Data = new TwitterRequestData();
			Data.UserName = userName;
			Data.Password = password;

			Data.ActionUri = this.BuildConditionalUrl(Parameters, "direct_messages/sent.xml");

            Data = Request.PerformWebRequest(Data, "GET");

			return Data.Statuses;
		}

		/// <summary>
		/// Sends a new direct message to a user.
		/// </summary>
		/// <param name="user">The user to send the direct message to.</param>
		/// <param name="message">The message to send.</param>
		/// <returns></returns>
		public TwitterStatus New(string user, string message)
		{
            TwitterRequest request = new TwitterRequest(proxyUri);
			TwitterRequestData data = new TwitterRequestData();
			data.UserName = userName;
			data.Password = password;

			data.ActionUri = new Uri(
				string.Format("{2}direct_messages/new.xml?user={0}&text={1}", user, HttpUtility.UrlEncode(message), Twitter.Domain));

			data = request.PerformWebRequest(data);
			return data.Statuses[0];
		}

        /// <summary>
        /// Destroys the specified direct message.
        /// </summary>
        /// <param name="ID">The ID of the direct message.</param>
        /// <returns></returns>
        public TwitterStatus Destroy(Int64 ID)
        {
            TwitterRequest request = new TwitterRequest(proxyUri);
            TwitterRequestData data = new TwitterRequestData();
            data.UserName = userName;
            data.Password = password;

            data.ActionUri = new Uri(
                string.Format("{1}direct_messages/destroy/{0}.xml", ID, Twitter.Domain));

            data = request.PerformWebRequest(data, "POST");
            return data.Statuses[0];
        }

        /// <summary>
        /// Destroys the specified direct message.
        /// </summary>
        /// <param name="Status">The status to destroy.</param>
        /// <returns></returns>
        public TwitterStatus Destroy(TwitterStatus Status)
        {
            return Destroy(Status.ID);
        }
	}
}
