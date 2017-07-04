using System;
using NHibernate;
using NHibernate.Context;
using NHibernate.Engine;

namespace Easy.NHibernate.Session
{
    /// <summary>
    /// CurrentDomain affinity session context.
    /// </summary>
    public class CurrentDomainSessionContext : CurrentSessionContext
    {
        protected readonly string _sessionContextName = nameof(CurrentDomainSessionContext);

        protected override ISession Session
        {
            get => (ISession) AppDomain.CurrentDomain.GetData(_sessionContextName);
            set => AppDomain.CurrentDomain.SetData(_sessionContextName, value);
        }

        // Must exists, used by SessionFactoryImpl.BuildCurrentSessionContext
        public CurrentDomainSessionContext(ISessionFactoryImplementor factory)
        {
        }
    }
}
