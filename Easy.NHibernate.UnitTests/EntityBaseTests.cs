using Easy.NHibernate.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace Easy.NHibernate.UnitTests
{
    internal class EntityStub : EntityBase<EntityStub>
    {
        // For unit tests only.
        public void ChangeId(int id)
        {
            Id = id;
        }

        public static EntityStub Create(int id)
        {
            return new EntityStub {Id = id};
        }
    }

    internal class SiblingEntityStub : EntityBase<EntityStub>
    {
        public static SiblingEntityStub Create(int id)
        {
            return new SiblingEntityStub { Id = id };
        }
    }

    internal class ChildEntityStub : EntityStub
    {
        public new static ChildEntityStub Create(int id)
        {
            return new ChildEntityStub { Id = id };
        }
    }

    internal class EntityBaseTests
    {
        [Test]
        public void Assert_entity_is_not_Object()
        {
            EntityStub e = EntityStub.Create(1);
            e.Should().NotBe(new object());
        }

        [Test]
        public void Assert_entities_with_different_id()
        {
            EntityStub e1 = EntityStub.Create(1);
            EntityStub e2 = EntityStub.Create(2);

            e1.Should().NotBe(e2);
            e1.Should().Be(e1);
            e1.Should().NotBe(1);
        }

        [Test]
        public void Assert_entities_with_same_id_but_not_equal()
        {
            EntityStub e1 = EntityStub.Create(1);
            SiblingEntityStub e2 = SiblingEntityStub.Create(1);

            e1.Should().NotBe(e2);
        }

        [Test]
        public void Assert_transient_entities_are_not_equal()
        {
            EntityStub e1 = new EntityStub();
            EntityStub e2 = new EntityStub();

            e1.Should().NotBe(e2);
        }

        [Test]
        public void Assert_inherited_entities_are_equal()
        {
            EntityStub e1 = EntityStub.Create(1);
            ChildEntityStub e2 = ChildEntityStub.Create(1);

            e1.Should().Be(e2);
        }

        [Test]
        public void Assert_hash_code_does_not_change()
        {
            EntityStub e = new EntityStub();
            int oldHash = e.GetHashCode();
            e.ChangeId(100);
            int newHash = e.GetHashCode();

            newHash.Should().Be(oldHash);
        }

        [Test]
        public void Assert_transient_should_have_not_same_hash_code()
        {
            EntityStub e1 = new EntityStub();
            EntityStub e2 = new EntityStub();

            int firstHashCode = e1.GetHashCode();
            int secondHashCode = e2.GetHashCode();

            firstHashCode.Should().NotBe(secondHashCode);
        }

        [Test]
        public void Assert_persistent_should_have_same_hash_code()
        {
            EntityStub e1 = EntityStub.Create(10);
            EntityStub e2 = EntityStub.Create(10);

            int firstHashCode = e1.GetHashCode();
            int secondHashCode = e2.GetHashCode();

            firstHashCode.Should().Be(secondHashCode);
        }

        [Test]
        public void Assert_inherited_persistent_should_have_same_hash_code()
        {
            EntityStub e1 = EntityStub.Create(10);
            ChildEntityStub e2 = ChildEntityStub.Create(10);

            int firstHashCode = e1.GetHashCode();
            int secondHashCode = e2.GetHashCode();

            firstHashCode.Should().Be(secondHashCode);
        }
    }
}
