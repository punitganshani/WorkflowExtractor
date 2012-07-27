using System;
using System.Reflection;

namespace WFE.Exceptions
{
    public class CouldNotCreateInstanceException : Exception
    {
        public CouldNotCreateInstanceException(string message) : base(message)
        {

        }

        public static string CreateMessage(FieldInfo field)
        {
            return String.Format("Instance could not be created for field '{0}' of type '{1}' in Assembly '{2}'", 
                                    field.Name, 
                                    field.DeclaringType.FullName, 
                                    field.Module);
        }
    }
}
