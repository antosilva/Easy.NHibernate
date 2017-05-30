using Easy.NHibernate.Repository.Interfaces;

namespace Easy.NHibernate.Repository
{
    public class EntityBase<T> : IEntity where T : EntityBase<T>
    {
        private int? _hashCode;

        public int Id { get; set; }

        public override int GetHashCode()
        {
            if (_hashCode.HasValue)
            {
                return _hashCode.Value;
            }

            if (Id == 0)
            {
                _hashCode = base.GetHashCode(); // Object hash code.
                return _hashCode.Value;
            }

            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Id == ((EntityBase<T>) obj).Id;
        }

        public static bool operator ==(EntityBase<T> left, EntityBase<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EntityBase<T> left, EntityBase<T> right)
        {
            return !Equals(left, right);
        }
    }
}
