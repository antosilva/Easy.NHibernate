using System.Collections.Generic;
using Easy.NHibernate.UnitTests.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;

namespace Easy.NHibernate.UnitTests.Mappings
{
    public class EmployeeMappings : ClassMapping<Employee>
    {
        public EmployeeMappings()
        {
            DynamicUpdate(true);
            SelectBeforeUpdate(true);
            Id(e => e.Id, mapper =>
            {
                mapper.Generator(Generators.HighLow, hilomapper =>
                {
                    hilomapper.Params(new Dictionary<string, object>
                    {
                        { "max_lo", 10},
                        { "next_hi", 200}
                    });
                });
            });

            Property(e => e.EmployeeNumber, mapper => { mapper.Lazy(true);});
            Property(e => e.Firstname);
            Property(e => e.Lastname);
            Property(e => e.EmailAddress);
            Property(e => e.DateOfBirth);
            Property(e => e.DateOfJoining);
            Property(e => e.IsAdmin);
            Property(e => e.Password);

            OneToOne(e => e.ResidentialAddress, mapper =>
            {
                mapper.PropertyReference(a => a.Employee);
                mapper.Cascade(Cascade.All);
                //mapper.Constrained(true);
            });

            Set(e => e.Benefits,
                mapper => { 
                    mapper.Key(k => k.Column("Employee_Id")); 
                    mapper.Cascade(Cascade.All.Include(Cascade.DeleteOrphans));
                    mapper.Inverse(true);
                    mapper.Lazy(CollectionLazy.Extra);
                    mapper.Cache(c =>
                    {
                        c.Usage(CacheUsage.ReadWrite);
                        c.Region("Employee_Benefits");
                    });
                },
                relation => relation.OneToMany(mapping => mapping.Class(typeof(Benefit))));

            Set(e => e.Communities,
                mapper =>
                {
                    mapper.Table("Employee_Community");
                    mapper.Cascade(Cascade.All);
                    mapper.Key(k => k.Column("Employee_Id"));
                },
                relation => relation.ManyToMany(mtm =>
                {
                    mtm.Class(typeof(Community));
                    mtm.Column("Community_Id");
                }));
            Property(e => e.IsEmployeed, mapper => mapper.Type<YesNoType>());
            //Version(e => e.RowVersion, versionMapper =>
            //{
            //    versionMapper.Generated(VersionGeneration.Always);
            //    versionMapper.UnsavedValue(null);
            //    versionMapper.Type<BinaryTimestamp>();
            //    versionMapper.Column(v =>
            //    {
            //        v.SqlType("timestamp");
            //        v.NotNullable(false);
            //    });
            //});

        }
    }
}
