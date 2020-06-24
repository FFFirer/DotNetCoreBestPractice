using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace AttributeDemo.AttibuteCollection
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MaxLengthLimitAttribute : Attribute
    {
        private double maxLength;
        public double MaxLength
        {
            get
            {
                return maxLength;
            }
            set
            {
                maxLength = value;
            }
        }

        public MaxLengthLimitAttribute(double maxLength)
        {
            this.MaxLength = maxLength;
        }
    }
}
