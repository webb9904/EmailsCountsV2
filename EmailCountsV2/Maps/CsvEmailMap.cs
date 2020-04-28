namespace EmailCountsV2.Maps
{
    using CanonicalModels;
    using CsvHelper.Configuration;

    public class CsvEmailMap : ClassMap<CsvEmail>
    {
        public CsvEmailMap()
        {
            Map(c => c.SentDateTime).Name("origin_timestamp");
            Map(c => c.SenderAddress).Name("sender_address");
            Map(c => c.RecipientAddress).Name("recipient_status");
        }
    }
}
