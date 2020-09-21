using System;
using DSharpPlus;

namespace GreenSharp.Attributes.Helpers
{
    public class HelpPage
    {
        public String Accepts { get; set; }
        public String Returns { get; set; }
        public String Description { get; set; }

        public Permissions Permissions
        {
            get;
            set;
        }
    }
}