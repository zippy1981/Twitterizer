using System;
using System.Windows.Forms;
using Twitterizer.Framework;

namespace Twitterizer.TestApp
{
    public partial class MainForm : Form
    {
        private readonly ConfigureForm ConfigFormSingleton = new ConfigureForm();

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            Width = 541;
            Height = 209;
        }

        /// <summary>
        /// Handles the TextChanged event of the UpdateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            CharCountLabel.Text =
                string.Format("{0} chars left",
                140 - UpdateTextBox.Text.Length);

            UpdateButton.Enabled = (UpdateTextBox.Text.Length <= 140);
        }

        /// <summary>
        /// Handles the Click event of the configureToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigFormSingleton.Show();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the MainFormTabControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MainFormTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (MainFormTabControl.SelectedTab.Name)
            {
                case "UpdateTabPage":
                    Width = 541;
                    Height = 209;
                    break;
                case "TimelineTabPage":
                    Width = 789;
                    Height = 599;
                    break;
                case "FriendsTabPage":
                    Width = 789;
                    Height = 599;
                    break;
                default:
                    break;
            }
        }

        #region Timeline Menu Clicks
        private void friendsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitter t = new Twitter(userName, password);
                TwitterParameters Parameters = new TwitterParameters();
                TimelineDataGridView.DataSource = t.Status.FriendsTimeline();
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            MainFormTabControl.SelectedIndex = 2;
        }

        private void publicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitter t = new Twitter(userName, password);

                TimelineDataGridView.DataSource = t.Status.PublicTimeline();
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            MainFormTabControl.SelectedIndex = 2;
        }

        private void twitterizerTimelineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitter t = null;
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                    t = new Twitter(userName, password);
                else
                    t = new Twitter();

                TwitterParameters param = new TwitterParameters();
                param.Add(TwitterParameterNames.ID, "Twit_er_izer");

                TimelineDataGridView.DataSource = t.Status.UserTimeline(param);
                MainFormTabControl.SelectedIndex = 2;
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitter t = null;
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                    t = new Twitter(userName, password);
                else
                    t = new Twitter();

                TwitterParameters param = new TwitterParameters();
                param.Add(TwitterParameterNames.Cursor, -1);

                TimelineDataGridView.DataSource = t.Status.UserTimeline(param);
                MainFormTabControl.SelectedIndex = 2;
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }

        }

        private void mentionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitter t = new Twitter(userName, password);
                TimelineDataGridView.DataSource = t.Status.Mentions();
                MainFormTabControl.SelectedIndex = 2;
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void repliesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitter t = new Twitter(userName, password);
                TimelineDataGridView.DataSource = t.Status.Replies();
                MainFormTabControl.SelectedIndex = 2;
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        /// <summary>
        /// Handles the Click event of the UpdateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                Twitter t = new Twitter(userName, password);
                t.Status.Update(UpdateTextBox.Text);
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #region Friends and Followers

        private void friendsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitter t = new Twitter(userName, password);
                FriendsDataGridView.DataSource = t.User.Friends();
                MainFormTabControl.SelectedIndex = 1;
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void followersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitter t = new Twitter(userName, password);
                FriendsDataGridView.DataSource = t.User.Followers();
                MainFormTabControl.SelectedIndex = 1;
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void directMessagesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitter t = new Twitter(userName, password);
                TimelineDataGridView.DataSource = t.DirectMessages.DirectMessages(null);
                MainFormTabControl.SelectedIndex = 2;
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Direct Messages
        private void directMessagesSentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitter t = new Twitter(userName, password);
                TimelineDataGridView.DataSource = t.DirectMessages.DirectMessagesSent(null);
                MainFormTabControl.SelectedIndex = 2;
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void DirectMessageButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                Twitter t = new Twitter(userName, password);
                t.DirectMessages.New(DirectMessageUserTextBox.Text, UpdateTextBox.Text);
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        /// <summary>
        /// Handles the Click event of the btnVCSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnVCSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                lblVCMessage.Text = Twitter.VerifyCredentials(tbVCUsername.Text, tbVCPassword.Text).ToString();
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the DeleteRowMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DeleteRowMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = TimelineDataGridView.SelectedRows[0];
            TwitterStatus status = selectedRow.DataBoundItem as TwitterStatus;

            if (status == null)
                return;

            if (MessageBox.Show("Are you sure you would like to delete this message from Twitter?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                Twitter t = new Twitter(userName, password);
                if (status.IsDirectMessage)
                    t.DirectMessages.Destroy(status);
                else
                    t.Status.Destroy(status);
                Cursor = Cursors.Default;
                MessageBox.Show("The message has been deleted! Please re-query the timeline.");

            }
        }

        /// <summary>
        /// Handles the Click event of the favoritesToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void favoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitter t = new Twitter(userName, password);
                TimelineDataGridView.DataSource = t.Status.FavoritesTimeline(null);
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            MainFormTabControl.SelectedIndex = 2;
        }

        /// <summary>
        /// Handles the Click event of the userHomeToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void userHomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitter t = new Twitter(userName, password);
                TimelineDataGridView.DataSource = t.Status.HomeTimeline();
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            MainFormTabControl.SelectedIndex = 2;
        }

        /// <summary>
        /// Handles the Click event of the removeFriendshipToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void removeFriendshipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                DataGridViewRow selectedRow = FriendsDataGridView.SelectedRows[0];
                TwitterUser user = selectedRow.DataBoundItem as TwitterUser;

                if (user == null)
                {
                    return;
                }

                Twitter t = new Twitter(userName, password);
                t.User.UnFollowUser(user.ID);
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            MainFormTabControl.SelectedIndex = 2;
        }

        /// <summary>
        /// Handles the Click event of the viewUserTimelineToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void viewUserTimelineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                DataGridViewRow selectedRow = FriendsDataGridView.SelectedRows[0];
                TwitterUser user = selectedRow.DataBoundItem as TwitterUser;

                if (user == null)
                {
                    return;
                }

                Twitter t = new Twitter(userName, password);
                TwitterParameters args = new TwitterParameters(TwitterParameterNames.UserID, user.ID);
                t.Status.UserTimeline(args);
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            MainFormTabControl.SelectedIndex = 3;
        }
    }
}