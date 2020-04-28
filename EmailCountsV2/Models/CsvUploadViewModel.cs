namespace EmailCountsV2.Models
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;

    public class CsvUploadViewModel
    {
        public List<IFormFile> Files { get; set; }
    }
}
