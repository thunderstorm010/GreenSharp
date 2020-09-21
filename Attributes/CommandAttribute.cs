using System;

namespace GreenSharp.Attributes
{
    /// Represents the attribute class for command methods.
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        public String? Alias { get; set; }
        public Boolean AllowDMs { get; set; }

        public String? Description { get; set; }
        public CommandAttribute(String? alias = null,String? description = null, Boolean allowDMs = false)
        {
            Alias = alias;
            Description = description;
            AllowDMs = allowDMs;
        }
    }
}