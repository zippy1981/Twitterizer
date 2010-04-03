﻿//-----------------------------------------------------------------------
// <copyright file="friends.aspx.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The example friends web page</summary>
//-----------------------------------------------------------------------

using System;
using Twitterizer;

public partial class friends : System.Web.UI.Page
{
    public TwitterUserCollection FriendsCollection { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            long userId = 0;

            if (!string.IsNullOrEmpty(Request.QueryString["userid"]) && long.TryParse(Request.QueryString["userid"], out userId))
            {
                this.FriendsCollection = TwitterUser.GetFriends(Master.Tokens, userId);
            }
            else
            {
                this.FriendsCollection = TwitterUser.GetFriends(Master.Tokens);
            }

            ViewState.Add("friends", this.FriendsCollection);
            this.DataBind();
        }
        else
        {
            this.FriendsCollection = ViewState["friends"] as TwitterUserCollection;
        }
    }

    protected void NextPageLinkButton_Click(object sender, EventArgs e)
    {
        this.FriendsCollection = this.FriendsCollection.NextPage();
        this.DataBind();

        ViewState["friends"] = this.FriendsCollection;
    }
}
