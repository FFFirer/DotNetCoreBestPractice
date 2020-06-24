using System;
using System.Collections.Generic;
using System.Text;
using AttributeDemo.AttibuteCollection;

namespace AttributeDemo.Models
{
    [Help("Information on the class Rectangle")]
    [DebugInfo(45, "Zara", "12/8/2012", Message = "Return type mismatch")]
    [DebugInfo(49, "Nuha", "10/10/2012", Message = "Unused varible")]
    class Rectangle
    {
        [MaxLengthLimit(5.0)]
        protected double length;


        [MaxLengthLimit(3.0)]
        protected double width;

        public Rectangle(double l, double w)
        {
            length = l;
            width = w;
        }

        [DebugInfo(55, "Zara", "19/10/2012", Message = "Return type mesmatch")]
        public double GetArea()
        {
            return length * width;
        }

        [DebugInfo(56, "Zara", "19/10/2012")]
        public void Display()
        {
            Console.WriteLine("Length {0}", length);
            Console.WriteLine("Width {0}", width);
            Console.WriteLine("Area {0}", GetArea());
        }
    }
}
