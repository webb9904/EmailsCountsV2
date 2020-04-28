namespace EmailCountsV2.Maps
{
    using CanonicalModels;
    using FluentNHibernate.Mapping;

    public class ExclusionMap : ClassMap<Exclusion>
    {
        public ExclusionMap()
        {
            Table("Exclusions");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Domain).Column("Domain").Nullable();
            Map(x => x.FullAddress).Column("FullAddress").Nullable();
        }
    }
}
