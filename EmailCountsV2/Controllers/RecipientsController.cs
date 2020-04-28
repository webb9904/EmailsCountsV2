namespace EmailCountsV2.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using CanonicalModels;
    using System.Collections.Generic;
    using Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Linq;
    using System;
    using System.Threading.Tasks;

    public class RecipientsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Recipient> _recipientRepository;
        private readonly IRepository<Department> _departmentRespository;

        public RecipientsController(IUnitOfWork unitOfWork, 
            IRepository<Recipient> recipientRepository,
            IRepository<Department> departmentRespository)
        {
            _unitOfWork = unitOfWork;
            _recipientRepository = recipientRepository;
            _departmentRespository = departmentRespository;
        }

        public IActionResult List(string selected)
        {
            var model = new List<RecipientViewModel>();

            var query = !string.IsNullOrEmpty(selected) ?
                _recipientRepository.FilterBy(x => x.EmailAddress.Contains(selected) || x.Department.Contains(selected)) :
                _recipientRepository.GetAll();

            foreach (var item in query)
            {
                model.Add(new RecipientViewModel()
                {
                    Id = item.Id,
                    EmailAddress = item.EmailAddress,
                    Department = item.Department
                });
            }

            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.DepartmentList = new SelectList(Departments(), "DepartmentName", "DepartmentName");

            return View();
        }

        public IActionResult Edit(int id)
        {
            var query = _recipientRepository.FindBy(x => x.Id == id);

            var model = new RecipientViewModel()
            {
                Id = query.Id,
                EmailAddress = query.EmailAddress,
                Department = query.Department
            };

            ViewBag.DepartmentList = new SelectList(Departments(), "DepartmentName", "DepartmentName");

            return View(model);
        }

        public async Task<IActionResult> Save(RecipientViewModel recipient)
        {
            if (ModelState.IsValid)
            {
                var model = new Recipient()
                {
                    EmailAddress = recipient.EmailAddress,
                    Department = recipient.Department
                };

                try
                {
                    _unitOfWork.BeginTransaction();

                    await _recipientRepository.Save(model);

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
                }

                return Redirect("/Recipients/List");
            }

            ViewBag.DepartmentList = new SelectList(Departments(), "DepartmentName", "DepartmentName");

            return View("Create");
        }

        public async Task<IActionResult> Update(RecipientViewModel recipient)
        {
            if (ModelState.IsValid)
            {
                var model = new Recipient()
                {
                    Id = recipient.Id,
                    EmailAddress = recipient.EmailAddress,
                    Department = recipient.Department
                };

                try
                {
                    _unitOfWork.BeginTransaction();

                    await _recipientRepository.Save(model);

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
                }

                return Redirect("/Recipients/List");
            }

            ViewBag.DepartmentList = new SelectList(Departments(), "DepartmentName", "DepartmentName");

            return View("Edit");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var query = _recipientRepository.Get(id);

            try
            {
                _unitOfWork.BeginTransaction();

                await _recipientRepository.Delete(query);

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
            }

            return Redirect("/Recipients/List");
        }

        private List<Department> Departments()
        {
            return _departmentRespository.GetAll().ToList();
        }
    }
}
