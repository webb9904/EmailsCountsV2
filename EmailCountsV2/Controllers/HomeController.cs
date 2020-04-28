namespace EmailCountsV2.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using CanonicalModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<DbEmail> _dbEmailRepository;
        private ICsvReaderHandler _reader;
        private ICsvEmailTransformer _transformer;

        public HomeController(IHostingEnvironment hostingEnvironment,
            IUnitOfWork unitOfWork,
            IRepository<DbEmail> dbEmailRepository,
            ICsvReaderHandler reader,
            ICsvEmailTransformer transformer)
        {
            _hostingEnvironment = hostingEnvironment;
            _unitOfWork = unitOfWork;
            _dbEmailRepository = dbEmailRepository;
            _reader = reader;
            _transformer = transformer;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CsvUploadViewModel upload)
        {
            if (ModelState.IsValid)
            {
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "upload");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (upload.Files != null && upload.Files.Count > 0)
                {
                    UploadFiles(upload, uploadsFolder);

                    try
                    {
                        _unitOfWork.BeginTransaction();

                        foreach (var dbEmail in _reader.Read(uploadsFolder))
                        {
                            await _dbEmailRepository.Save(dbEmail);
                        }

                        await _unitOfWork.Commit();
                    }
                    catch (Exception ex)
                    {
                        await _unitOfWork.Rollback();

                        throw ex;
                    }
                    finally
                    {
                        _unitOfWork.CloseTransaction();

                        ClearUploadsFolder(uploadsFolder);
                    }
                }
            }

            return View();
        }

        private static void UploadFiles(CsvUploadViewModel upload, string uploadsFolder)
        {
            foreach (IFormFile file in upload.Files)
            {
                var uniqueFileName = $"{Guid.NewGuid().ToString()}_{file.FileName}";

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
        }

        private static void ClearUploadsFolder(string uploadsFolder)
        {
            foreach (var file in new DirectoryInfo(uploadsFolder).GetFiles())
            {
                file.Delete();
            }
        }
    }
}
