﻿//-----------------------------------------------------------------------
// <copyright file="Default.aspx.cs" company="Patrick 'Ricky' Smith">
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
// <summary>The default example page.</summary>
//-----------------------------------------------------------------------

using System;
using Twitterizer;

public partial class _Default : System.Web.UI.Page
{
    public TwitterStatusCollection HomePageStatuses { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Trace.Write("Start TwitterUser.GetHomeTimeline");
            this.HomePageStatuses = TwitterTimeline.HomeTimeline(Master.Tokens);
            this.Trace.Write("End TwitterUser.GetHomeTimeline");
            this.DataBind();

            ViewState.Add("homePageStatuses", this.HomePageStatuses);
        }
        else
        {
            this.HomePageStatuses = ViewState["homePageStatuses"] as TwitterStatusCollection;
        }
    }

    protected void NextPageLinkButton_Click(object sender, EventArgs e)
    {
        this.HomePageStatuses = this.HomePageStatuses.NextPage();
        ViewState["homePageStatuses"] = this.HomePageStatuses;
        this.DataBind();
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(this.UpdateTextBox.Text))
        {
            StatusUpdateLabel.Text = "You have to enter something first.<br/>";
            return;
        }

        if (this.UpdateTextBox.Text.Length > 140)
        {
            StatusUpdateLabel.Text = "Your tweet is too long.<br/>";
            return;
        }

        if (TwitterStatus.Update(this.Master.Tokens, this.UpdateTextBox.Text) != null)
        {
            StatusUpdateLabel.Text = "Your tweet has been posted successfully.<br/>";

            this.HomePageStatuses = TwitterTimeline.HomeTimeline(Master.Tokens);
            this.DataBind();
        }
        else
        {
            StatusUpdateLabel.Text = "Your tweet could not be posted.";
        }
    }
}
