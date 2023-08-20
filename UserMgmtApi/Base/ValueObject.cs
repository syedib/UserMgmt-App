using System;

namespace UserMgmtApi.Base 
{
    public abstract class ValueObject<T> : IEquatable<T> where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Equals(obj as T);
        }

        public bool Equals(T other)
        {
            if (other == null)
                return false;

            // Implement your equality comparison logic here
            // Compare the individual properties or fields of the objects
            // and return true if they are equal, otherwise return false

            return true; // Default to true for simplicity
        }

        public override int GetHashCode()
        {
            // Implement a GetHashCode method that generates a hash code
            // based on the values of the object's properties or fields

            return base.GetHashCode(); // Default to base.GetHashCode()
        }

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }

}



