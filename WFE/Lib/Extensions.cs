using System;
using System.Linq;
using System.Reflection;
using WFE.Models.Output;
using System.Collections.Generic;

namespace WFE.Lib
{
    internal static class Extensions
    {
        internal static bool IsDerivativeOf(this Type inputType, Type typeToCompare)
        {
            if (inputType == null) throw new NullReferenceException();

            if (inputType.BaseType == null)
                return false;
            if (inputType.BaseType == typeToCompare)
                return true;
            else
            {
#if DEBUG
                bool output = inputType.BaseType.IsDerivativeOf(typeToCompare);
                return output;
#else
                return inputType.BaseType.IsDerivativeOf(typeToCompare);
#endif
            }
        }

        internal static T GetPrivateFieldValue<T>(this object obj, string propName)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            Type type = obj.GetType();
            FieldInfo field = null;
            while (field == null && type != null)
            {
                field = type.GetField(propName, BindingFlags.Default |
                                          BindingFlags.CreateInstance |
                                          BindingFlags.Instance |
                                          BindingFlags.NonPublic |
                                          BindingFlags.Public);
                type = type.BaseType;
            }
            if (field == null)
                throw new ArgumentOutOfRangeException("propName", string.Format("Field {0} was not found in Type {1}", propName, obj.GetType().FullName));

            return (T)field.GetValue(obj);
        }

        internal static T GetPrivateMethodValue<T>(this object obj, string propName)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            Type type = obj.GetType();
            MethodInfo method = null;
            while (method == null && type != null)
            {
                method = type.GetMethod(propName, BindingFlags.Default |
                                          BindingFlags.CreateInstance |
                                          BindingFlags.Instance |
                                          BindingFlags.NonPublic |
                                          BindingFlags.Public);
                type = type.BaseType;
            }
            if (method == null)
                throw new ArgumentOutOfRangeException("propName", string.Format("Field {0} was not found in Type {1}", propName, obj.GetType().FullName));

            return (T)method.Invoke(obj, null);
        }

        internal static T GetPublicPropertValue<T>(this object obj, string propName)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            Type type = obj.GetType();
            PropertyInfo property = null;
            while (property == null && type != null)
            {
                property = type.GetProperty(propName, BindingFlags.Default |
                                             BindingFlags.CreateInstance |
                                             BindingFlags.Instance |
                                             BindingFlags.NonPublic |
                                             BindingFlags.Public | 
                                             BindingFlags.IgnoreCase | 
                                             BindingFlags.Static);
                type = type.BaseType;
            }
            if (property == null)
                throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));

            return (T)property.GetValue(obj, null);
        }

        internal static string ToCsv(this string[] input)
        {
            if (input == null)
                return string.Empty;

            string output = input.Aggregate(string.Empty, (current, t) => current + (t + "  "));

            return output.TrimEnd().Replace("  ", ", ");
        }

        internal static Settings Find<T>(this List<Settings> input) where T: Settings
        {
            var output = input.FirstOrDefault(x => x is T);
            if (output == null)
                return Activator.CreateInstance<T>();
            else
                return output;
        }
    }
}
