namespace EmailCountsV2.Maps
{
    using CanonicalModels;
    using FluentNHibernate.Mapping;

    public class DepartmentMap : ClassMap<Department>
    {
        public DepartmentMap()
        {
            Table("Departments");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.DepartmentName).Column("DepartmentName").Not.Nullable();
        }
    }
}
