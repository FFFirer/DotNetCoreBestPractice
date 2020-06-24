using AttributeDemo.AttibuteCollection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace AttributeDemo.ValidateHelpers
{
    public class RectangleValidater
    {
        public static void ValidateMaxLength(object obj)
        {
            Type objType = obj.GetType();

            var properties = objType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!property.IsDefined(typeof(MaxLengthLimitAttribute), false)) continue;

                var attributes = property.GetCustomAttributes();

                foreach (var item in attributes)
                {
                    var maxlength = (double)item.GetType().GetProperty("MaxLength").GetValue(item);

                    double actuellength = (double)property.GetValue(obj);

                    if(actuellength > maxlength)
                    {
                        throw new Exception($"长度不能超过{maxlength}");
                    }
                }
            }
        }
    }
}
