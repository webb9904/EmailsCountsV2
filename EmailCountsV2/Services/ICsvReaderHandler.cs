namespace EmailCountsV2
{
    using CanonicalModels;
    using System.Collections.Generic;

    public interface ICsvReaderHandler
    {
        List<DbEmail> Read(string path);
    }
}
