using System;
using System.Collections.Generic;
using System.Text;

namespace AttributeDemo.AttibuteCollection
{
    [AttributeUsage(AttributeTargets.Class 
        | AttributeTargets.Constructor 
        | AttributeTargets.Field 
        | AttributeTargets.Method 
        | AttributeTargets.Property, AllowMultiple = true)]
    public class DebugInfoAttribute : Attribute
    {
        private int bugNo;
        private string developer;
        private string lastReview;
        private string message;

        public DebugInfoAttribute(int bg, string dever, string d)
        {
            this.bugNo = bg;
            this.developer = dever;
            this.lastReview = d;
        }

        public int BugNo
        {
            get
            {
                return bugNo;
            }
        }

        public string Developer
        {
            get
            {
                return developer;
            }
        }

        public string LastReview
        {
            get
            {
                return lastReview;
            }
        }

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }
    }
}
