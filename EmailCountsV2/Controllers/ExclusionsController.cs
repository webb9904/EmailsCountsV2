namespace EmailCountsV2.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using CanonicalModels;
    using Models;
    using System.Collections.Generic;

    public class ExclusionsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Exclusion> _exclusionRepository;

        public ExclusionsController(IUnitOfWork unitOfWork,
            IRepository<Exclusion> exclusionRepository)
        {
            _unitOfWork = unitOfWork;
            _exclusionRepository = exclusionRepository;
        }

        public IActionResult List(string selected)
        {
            var model = new List<ExclusionViewModel>();

            var query = !string.IsNullOrEmpty(selected) ?
                _exclusionRepository.FilterBy(x => x.FullAddress.Contains(selected) || x.Domain.Contains(selected)) :
                _exclusionRepository.GetAll();

            foreach (var item in query)
            {
                model.Add(new ExclusionViewModel()
                {
                    Id = item.Id,
                    FullAddress = item.FullAddress,
                    Domain = item.Domain
                });
            }

            return View(model);
        }
    }
}
