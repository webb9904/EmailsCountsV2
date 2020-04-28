namespace EmailCountsV2.Controllers
{
    using CanonicalModels;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System;
    using System.Collections.Generic;

    public class CountsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<DbEmail> _dbEmailRepository;

        public CountsController(IUnitOfWork unitOfWork, IRepository<DbEmail> dbEmailRepository)
        {
            _unitOfWork = unitOfWork;
            _dbEmailRepository = dbEmailRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index(DateTime SentDate)
        {
            var model = new List<DbEmailViewModel>();

            var query = _dbEmailRepository.FilterBy(x => x.SentDate == SentDate);
            var queryMonth = _dbEmailRepository.FilterBy(x => x.SentDate.Month == SentDate.Month);

            foreach (var item in queryMonth)
            {
                model.Add(new DbEmailViewModel()
                {
                    Id = item.Id,
                    SentDate = item.SentDate,
                    SentDateTime = item.SentDateTime,
                    SenderAddress = item.SenderAddress,
                    SenderDomain = item.SenderDomain,
                    RecipientAddress = item.RecipientAddress
                });
            }

            return View(model);
        }
    }
}
