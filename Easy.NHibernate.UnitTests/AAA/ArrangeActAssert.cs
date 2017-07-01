using System;
using NUnit.Framework;

namespace Easy.NHibernate.UnitTests.AAA
{
    internal class ArrangeActAssert
    {
        [OneTimeSetUp]
        public void OneTimeSetUpBase()
        {
            OneTimeSetUp();
            Arrange();
            Act();
        }

        [OneTimeTearDown]
        public void OneTimeTearDownBase()
        {
            OneTimeTearDown();
        }

        public virtual void OneTimeSetUp()
        {
        }

        public virtual void Arrange()
        {
        }

        public virtual void Act()
        {
        }

        public virtual void OneTimeTearDown()
        {
        }

        public Exception Trying(Action action)
        {
            Exception result = null;
            try
            {
                action();
            }
            catch (Exception e)
            {
                result = e;
            }
            return result;
        }
    }
}
