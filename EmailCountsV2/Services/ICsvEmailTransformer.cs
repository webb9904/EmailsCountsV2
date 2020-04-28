namespace EmailCountsV2
{
    using CanonicalModels;

    public interface ICsvEmailTransformer
    {
        DbEmail Convert(CsvEmail csvEmail, int id);
    }
}
