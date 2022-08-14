using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VNEngine.Runtime.Core.Service.Extensions
{
    public static class NTypeExtensions
    {
        /// <summary>Caching of cast support between types to avoid repeat reflection</summary>
        private static readonly Dictionary<(Type, Type), bool> CachedCacheSupport = new ();

        /// <summary>
        /// Test if a type can cast to another, taking in account cast operators.
        /// </summary>
        public static bool IsCastableTo(this Type from, Type to, bool implicitly = false)
        {
            // Based on https://stackoverflow.com/a/22031364
            var key = (from, to);
            if (CachedCacheSupport.TryGetValue(key, out bool support))
            {
                return support;
            }

            support = to.IsAssignableFrom(from) || from.HasCastDefined(to, implicitly);
            CachedCacheSupport.Add(key, support);
            return support;
        }

        private static bool HasCastDefined(this Type from, Type to, bool implicitly)
        {
            if ((from.IsPrimitive || from.IsEnum) && (to.IsPrimitive || to.IsEnum))
            {
                if (!implicitly)
                {
                    return from == to || (from != typeof(bool) && to != typeof(bool));
                }
                
                Type[][] typeHierarchy = {
                    new [] { typeof(byte),  typeof(sbyte), typeof(char) },
                    new [] { typeof(short), typeof(ushort) },
                    new [] { typeof(int), typeof(uint) },
                    new [] { typeof(long), typeof(ulong) },
                    new [] { typeof(float) },
                    new [] { typeof(double) }
                };

                IEnumerable<Type> lowerTypes = Enumerable.Empty<Type>();
                foreach (Type[] types in typeHierarchy)
                {
                    if (types.Any(t => t == to))
                    {
                        return lowerTypes.Any(t => t == from);
                    }
                        
                    lowerTypes = lowerTypes.Concat(types);
                }

                return false; // IntPtr, UIntPtr, Enum, Boolean
            }

            return HasCastOperator(to, m => m.GetParameters()[0].ParameterType, _ => from, implicitly, false)
                || HasCastOperator(from, _ => to, m => m.ReturnType, implicitly, true);
        }

        private static bool HasCastOperator(
            Type type, 
            Func<MethodInfo, Type> baseType, 
            Func<MethodInfo, Type> derivedType,
            bool implicitly, 
            bool lookInBase
        ) {
            var bindingFlags = BindingFlags.Public | BindingFlags.Static
                            | (lookInBase ? BindingFlags.FlattenHierarchy : BindingFlags.DeclaredOnly);

            return type.GetMethods(bindingFlags).Any(
                m => (m.Name == "op_Implicit" || (!implicitly && m.Name == "op_Explicit"))
                    && baseType(m).IsAssignableFrom(derivedType(m))
            );
        }
    }
}