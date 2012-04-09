using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Shared.Core.Extensions;

namespace Shared.Core.Utilities
{
    public static class AssemblyScanner
    {
        public static IEnumerable<Type> GetMatchingTypesFromAssemblyContaining<T>(Func<Type, bool> expression)
        {
            return (typeof(T)).Assembly.GetTypes().Matching(expression);
        }

        public static IEnumerable<Type> FromAssemblyContaining<T>()
        {
            return FromAssemblyContaining(typeof(T));
        }

        public static IEnumerable<Type> FromAssemblyContaining(Type type)
        {
            return type.Assembly.GetTypes();
        }

        public static IEnumerable<Type> FromThisAssembly()
        {
            return Assembly.GetCallingAssembly().GetTypes();
        }

        public static IEnumerable<Type> Matching(this IEnumerable<Type> types, Func<Type, bool> expression)
        {
            return types.Where(expression);
        }

        public static IEnumerable<Type> GetControllersFromAssemblyContaining<T>()
        {
            return GetMatchingTypesFromAssemblyContaining<T>(c => c.CanBeCastTo<Controller>() && !c.IsAbstract);
        }

    }
}
