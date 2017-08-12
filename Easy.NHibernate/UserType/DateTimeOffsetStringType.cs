using System;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Easy.NHibernate.UserType
{
    public class DateTimeOffsetStringType : IUserType
    {
        public SqlType[] SqlTypes => new SqlType[] {SqlTypeFactory.GetString(33)}; // "2009-06-15T13:45:30.0000000-07:00"
        public Type ReturnedType => typeof(DateTimeOffset);
        public bool IsMutable => false;

        bool IUserType.Equals(object x, object y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            return x != null && y != null && x.Equals(y);
        }

        public int GetHashCode(object x)
        {
            return x == null
                       ? base.GetHashCode()
                       : x.GetHashCode();
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object Disassemble(object value)
        {
            return value;
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            // "o" => ISO8601
            // "2009-06-15T13:45:30.0000000-07:00"
            NHibernateUtil.String.NullSafeSet(cmd, ((DateTimeOffset) value).ToString("o"), index);
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            return DateTimeOffset.Parse(NHibernateUtil.String.NullSafeGet(rs, names[0]).ToString());
        }
    }
}
