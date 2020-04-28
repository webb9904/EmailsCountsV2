namespace EmailCountsV2
{
    using CanonicalModels;
    using CsvHelper;
    using CsvHelper.Configuration;
    using Maps;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class CsvReaderHandler : ICsvReaderHandler
    {
        private readonly IRepository<DbEmail> _session;
        private ICsvEmailTransformer _transformer;

        public CsvReaderHandler(IRepository<DbEmail> session, ICsvEmailTransformer transformer)
        {
            _session = session;
            _transformer = transformer;
        }

        public List<DbEmail> Read(string path)
        {
            var dbEmail = new List<DbEmail>();

            var files = new DirectoryInfo(path).GetFiles("*.csv");

            var id = KeyReader();

            foreach (var file in files)
            {
                dbEmail.AddRange(GetContents(file.FullName)
                    .Select(csvEmail => _transformer.Convert(csvEmail, id++)));
            }

            return dbEmail;
        }

        private static IEnumerable<CsvEmail> GetContents(string path)
        {
            using (var stream = new StreamReader(path, Encoding.Unicode))
            {
                using (var csvReader = new CsvReader(stream, Configuration()))
                {
                    return csvReader.GetRecords<CsvEmail>().ToList();
                }
            }
        }

        private static Configuration Configuration()
        {
            var config = new Configuration();

            config.RegisterClassMap<CsvEmailMap>();
            config.HasHeaderRecord = true;
            config.QuoteAllFields = true;
            config.Quote = '"';
            config.BadDataFound = null;
            return config;
        }

        public int KeyReader()
        {
            var nextId = _session.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
            return nextId == null ? 1 : nextId.Id + 1;
        }
    }
}
