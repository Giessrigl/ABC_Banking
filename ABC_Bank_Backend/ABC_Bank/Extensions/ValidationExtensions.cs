using ABC_Bank.Models;
using System;

namespace ABC_Bank.Extensions
{
    public static class ValidationExtensions
    {
        public static void ThrowIfNullOrEmpty(this string str, string stringName)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException($"{stringName} is null or empty.");
        }

        public static void ThrowIfNull<T>(this T obj, string objectName) where T : class
        {
            if (obj == null)
                throw new ArgumentNullException($"{objectName} is null.");
        }

        public static void ThrowIfOutOfRange<T>(this T obj, T min, T max, string objectName) where T : IComparable
        {
            if (!(min.CompareTo(max) < 0))
                throw new ArgumentOutOfRangeException($"{objectName} can not be between {min} and {max}");

            if (obj.CompareTo(min) < 0)
                throw new ArgumentOutOfRangeException($"{objectName} precedes {nameof(min)}");

            if (obj.CompareTo(max) > 0)
                throw new ArgumentOutOfRangeException($"{objectName} follows {nameof(max)}");
        }

        public static void ThrowIfSameObject<T>(this T obj, T otherObj) where T : IEquatable<T>
        {
            if (obj.Equals(otherObj))
                throw new ArgumentException($"{nameof(obj)} is equal to {nameof(otherObj)}");
        }

        public static void ValidateContactInfo(this ContactInfo info)
        {
            info.Firstname.ThrowIfNullOrEmpty(nameof(info.Firstname));
            info.Secondname.ThrowIfNullOrEmpty(nameof(info.Secondname));
            info.DateOfBirth.ThrowIfNullOrEmpty(nameof(info.DateOfBirth));
            info.Phonenumber.ThrowIfNullOrEmpty(nameof(info.Phonenumber));
            info.Country.ThrowIfNullOrEmpty(nameof(info.Country));
            info.City.ThrowIfNullOrEmpty(nameof(info.City));
            info.Streetname.ThrowIfNullOrEmpty(nameof(info.Streetname));
            info.Housenumber.ThrowIfNullOrEmpty(nameof(info.Housenumber));
        }
    }
}