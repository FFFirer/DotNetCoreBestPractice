using System;
using System.Collections.Generic;
using System.Text;

namespace AttributeDemo.AttibuteCollection
{
    [AttributeUsage(AttributeTargets.All)]
    public class HelpAttribute : Attribute
    {
        public readonly string Url;

        private string topic;
        public string Topic
        {
            get
            {
                return topic;
            }
            set
            {
                topic = value;
            }
        }

        public HelpAttribute(string url)
        {
            this.Url = url;
        }
    }
}
