using System;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Easy.NHibernate.UserType
{
    public class GuidStringType : IUserType
    {
        public SqlType[] SqlTypes => new SqlType[] {SqlTypeFactory.GetString(36)}; // "387A1A3D-E409-4F3E-B178-BCBFD0A88582"
        public Type ReturnedType => typeof(Guid);
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
            NHibernateUtil.String.NullSafeSet(cmd, value.ToString().ToUpper(), index);
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            return new Guid(NHibernateUtil.String.NullSafeGet(rs, names[0]).ToString());
        }
    }
}
