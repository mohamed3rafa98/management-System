using Microsoft.AspNetCore.Mvc;
using Demo.BLL.Repositories;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json.Linq;
using Demo.PL.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Demo.PL.Controllers
{
    [Authorize]

    public class DepartmentController : Controller
    {

        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public DepartmentController(/*IDepartmentRepository departmentRepository*/IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_departmentRepository = departmentRepository;
        }


        public async Task<IActionResult> Index(string DepartmentSearchInput)
        {
            var departments = Enumerable.Empty<Department>();
          
            if (string.IsNullOrEmpty(DepartmentSearchInput))
                 departments =   await _unitOfWork.DepartmentRepository.GetAllAsync();        
            else
                departments= await _unitOfWork.DepartmentRepository.GetByNameAsync(DepartmentSearchInput.ToLower());

            var departmentsViewModel =_mapper.Map<IEnumerable<DepartmentViewModel>>(departments);


            return View(departmentsViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel department)
        {
            var departmentViewModel = _mapper.Map<Department>(department);
            if(ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Add(departmentViewModel);
                int count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }  
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }


        public async Task<IActionResult> Details(int? id  , string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest(); // Error 400
            }
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);
            var departmentViewModel = _mapper.Map<DepartmentViewModel>(department);

            if (departmentViewModel is null)
                return NotFound();

            return View(ViewName, departmentViewModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int? id ,DepartmentViewModel department)
        {
            if (ModelState.IsValid)
            {
                var employeeViewModel = _mapper.Map<Department>(department);

                _unitOfWork.DepartmentRepository.Update(employeeViewModel);
                int count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }
        public async Task<IActionResult> Edit(int? id) => await Details(id, "Edit");



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int? id ,DepartmentViewModel department)
        {
            var employeeViewModel =_mapper.Map<Department>(department);

             _unitOfWork.DepartmentRepository.Delete(employeeViewModel);
            int count = await _unitOfWork.CompleteAsync();

            if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            
            return View(department);
        }
        public async Task<IActionResult> Delete(int? id) => await Details(id, "Delete");

    }
}
