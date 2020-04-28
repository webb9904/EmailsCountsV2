namespace EmailCountsV2.Maps
{
    using CanonicalModels;
    using FluentNHibernate.Mapping;

    public class RecipientMap : ClassMap<Recipient>
    {
        public RecipientMap()
        {
            Table("RecipientsToCapture");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.EmailAddress).Column("EmailAddress").Nullable();
            Map(x => x.Department).Column("Department").Nullable();
        }
    }
}
