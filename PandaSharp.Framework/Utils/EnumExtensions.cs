using System;
using System.Linq;
using System.Reflection;
using PandaSharp.Framework.Attributes;

namespace PandaSharp.Framework.Utils
{
    public static class EnumExtensions
    {
        public static string GetEnumStringRepresentation<T>(this T enumeration)
            where T : struct, Enum
        {
            var attribute = enumeration
                .GetType()
                .GetMember(enumeration.ToString())
                .First(member => member.MemberType == MemberTypes.Field)
                .GetCustomAttributes(typeof(StringRepresentationAttribute), false)
                .Cast<StringRepresentationAttribute>()
                .SingleOrDefault();

            return attribute?.AsString;
        }

        public static object GetEnumMember(this string enumString, Type enumType)
        {
            if (!enumType.IsEnum)
            {
                return null;
            }
            
            foreach (var member in enumType.GetMembers().Where(i => i.MemberType == MemberTypes.Field))
            {
                var attribute = member.GetCustomAttribute<StringRepresentationAttribute>(false);
                if (attribute != null)
                {
                    if (StringComparer.OrdinalIgnoreCase.Equals(enumString, attribute.AsString))
                    {
                        return Enum.Parse(enumType, member.Name);
                    }
                }
            }

            return null;
        }
    }
}