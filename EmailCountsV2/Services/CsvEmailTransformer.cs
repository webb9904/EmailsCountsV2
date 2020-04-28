namespace EmailCountsV2
{
    using System.Linq;
    using CanonicalModels;

    public class CsvEmailTransformer : ICsvEmailTransformer
    {
        public DbEmail Convert(CsvEmail csvEmail, int id)
        {
            var dbEmail = new DbEmail
            {
                Id = id,
                SentDate = csvEmail.SentDateTime.Date,
                SentDateTime = csvEmail.SentDateTime,
                RecipientAddress = RecipientAddressCleaner(csvEmail.RecipientAddress),
                SenderAddress = csvEmail.SenderAddress,
                SenderDomain = SenderDomain(csvEmail.SenderAddress)
            };

            return dbEmail;
        }

        private static string RecipientAddressCleaner(string recipientAddress)
        {
            return recipientAddress.Split('#').First();
        }

        private static string SenderDomain(string senderAddress)
        {
            return senderAddress.Split('@').Last();
        }
    }
}
