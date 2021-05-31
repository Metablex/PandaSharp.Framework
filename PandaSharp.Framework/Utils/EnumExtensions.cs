using System;
using System.Linq;
using System.Reflection;
using PandaSharp.Framework.Attributes;

namespace PandaSharp.Framework.Utils
{
    internal static class EnumExtensions
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

        public static void AddEnumMember<T>(this ref T? summand1, T summand2)
            where T : struct, Enum
        {
            if (!Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
            {
                throw new InvalidOperationException("This operation is only valid for enums with Flags attribute");
            }

            if (!summand1.HasValue)
            {
                summand1 = summand2;
                return;
            }

            var result = Convert.ToInt32(summand1) | Convert.ToInt32(summand2);
            summand1 = (T)Enum.ToObject(typeof(T), result);
        }
    }
}