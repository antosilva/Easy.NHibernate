using Easy.NHibernate.Domain.Interfaces;

namespace Easy.NHibernate.Domain
{
    public abstract class EntityBase<T> : IEntity where T : EntityBase<T>
    {
        private int? _hashCode;

        public virtual int Id { get; protected set; }

        public override int GetHashCode()
        {
            if (_hashCode.HasValue)
            {
                return _hashCode.Value;
            }

            // Transient entity.
            if (Id == 0)
            {
                _hashCode = base.GetHashCode();
                return _hashCode.Value;
            }

            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            T other = obj as T;
            if (other == null)
            {
                return false;
            }

            // Both transient entities.
            if (Id == 0 && other.Id == 0)
            {
                return ReferenceEquals(this, other);
            }

            return Id == other.Id;
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
