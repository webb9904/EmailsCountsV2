namespace EmailCountsV2.Maps
{
    using CanonicalModels;
    using FluentNHibernate.Mapping;

    public class vwAllEmails_TraceMap : ClassMap<vwAllEmails_Trace>
    {
        public vwAllEmails_TraceMap()
        {
            ReadOnly();
            Table("vwAllEmails_Trace");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.SentDate).Column("SentDate").Nullable();
            Map(x => x.SentDateTime).Column("SentDateTime").Nullable();
            Map(x => x.SenderAddress).Column("SenderAddress").Nullable();
            Map(x => x.SenderDomain).Column("SenderDomain").Nullable();
            Map(x => x.RecipientAddress).Column("RecipientAddress").Nullable();
        }
    }
}
