using AttributeDemo.AttibuteCollection;
using AttributeDemo.Models;
using AttributeDemo.ValidateHelpers;
using System;
using System.Reflection;

namespace AttributeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rectangle = new Rectangle(4.5, 7.5);

            RectangleValidater.ValidateMaxLength(rectangle);
            //Rectangle rectangle1 = new Rectangle(5.7, 6.7);
            //rectangle.Display();

            Type type = typeof(Rectangle);

            // 遍历类特性
            foreach (Attribute attr in type.GetCustomAttributes(false))
            {
                if(attr.GetType() == typeof(DebugInfoAttribute))
                {
                    DebugInfoAttribute di = (DebugInfoAttribute)attr;
                    if (di != null)
                    {
                        Console.WriteLine($"Bug No: {di.BugNo}");
                        Console.WriteLine($"Developer: {di.Developer}");
                        Console.WriteLine($"Last Reviewed: {di.LastReview}");
                        Console.WriteLine($"Remarks: {di.Message}");
                    }
                }

                if(attr.GetType() == typeof(HelpAttribute))
                {
                    HelpAttribute ha = (HelpAttribute)attr;
                    Console.WriteLine($"Url: {ha.Url}");
                    Console.WriteLine($"Topic: {ha.Topic}");
                }
            }

            // 遍历方法特性
            foreach (MethodInfo m in type.GetMethods())
            {
                foreach (Attribute attr in m.GetCustomAttributes(true))
                {
                    if(attr.GetType() == typeof(DebugInfoAttribute))
                    {
                        DebugInfoAttribute di = (DebugInfoAttribute)attr;
                        if (di != null)
                        {
                            Console.WriteLine($"Bug No: {di.BugNo}");
                            Console.WriteLine($"Developer: {di.Developer}");
                            Console.WriteLine($"Last Reviewed: {di.LastReview}");
                            Console.WriteLine($"Remarks: {di.Message}");
                        }
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
