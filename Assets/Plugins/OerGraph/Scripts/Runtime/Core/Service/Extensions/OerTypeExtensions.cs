using System;
using System.Collections.Generic;

namespace OerGraph.Runtime.Core.Service.Extensions
{
    public static class OerTypeExtensions
    {
        public static HashSet<Type> GetTypeHierarchy(this Type type) => type.GetTypeHierarchy(typeof(object));
        
        public static HashSet<Type> GetTypeHierarchy(this Type type, Type closingType)
        {
            var result = new HashSet<Type>();
            var baseType = type;

            if (baseType == closingType)
            {
                result.Add(baseType);
                return result;
            }
            
            do
            {
                result.Add(baseType);
                baseType = baseType != typeof(object)
                    ? baseType.BaseType
                    : null;
            } while (baseType != closingType);

            return result;
        }
    }
}