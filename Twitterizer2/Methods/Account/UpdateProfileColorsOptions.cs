﻿namespace Twitterizer
{
    using System.Drawing;

    /// <summary>
    /// Optional properties for the <see cref="TwitterUser.UpdateProfileColors"/> method.
    /// </summary>
    public class UpdateProfileColorsOptions : OptionalProperties
    {
        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the link.
        /// </summary>
        /// <value>The color of the link.</value>
        public Color LinkColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the sidebar fill.
        /// </summary>
        /// <value>The color of the sidebar fill.</value>
        public Color SidebarFillColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the sidebar border.
        /// </summary>
        /// <value>The color of the sidebar border.</value>
        public Color SidebarBorderColor { get; set; }
    }
}
