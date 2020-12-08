using System;

namespace Authorization
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuthorizeUserAttribute: Attribute
    {
        public string ParameterName { get; private set; }

        public AuthorizeUserAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }
    }
}
