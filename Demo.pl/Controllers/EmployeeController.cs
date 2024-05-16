using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.PL.ViewModel;
using Demo.BLL.Repositories;
using Microsoft.CodeAnalysis;
using Demo.PL.Helper;
using Microsoft.AspNetCore.Authorization;

namespace Demo.PL.Controllers
{
    [Authorize]

    public class EmployeeController : Controller
    {

        //private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public EmployeeController(/*IEmployeeRepository employeeRepository ,*/IUnitOfWork unitOfWork, IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_employeeRepository = employeeRepository;

        }


        
        public async Task<IActionResult> Index(string EmployeeSearchInput)
        {
            var employees= Enumerable.Empty<Employee>();

            if(string.IsNullOrEmpty(EmployeeSearchInput))
            {
                //employees = _employeeRepository.GetAll();
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(EmployeeSearchInput.ToLower());
                //employees = _employeeRepository.GetByName(SearchInput.ToLower());
            }

            var result =_mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            //Views Dictionary use to transfer Extra Information from Action to view and (One Way) (key,value)

            //1. viewData : Property Inherited from controller class work as 

            //ViewData["Message"] = "Hello from viewData";

            //2. viewBag

            //2. ViewBag : Property Inherited from controller class work as Dynamic word

            //ViewBag.Message = "hello from ViewBag";

            //3. tempData

            // 3 TempData : Property Inherited from controller class work as 

            return View(result);


        }
      


        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {


            if (ModelState.IsValid)
            {
                model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
                var employee = _mapper.Map<Employee>(model);

                _unitOfWork.EmployeeRepository.Add(employee);
                int count = await _unitOfWork.CompleteAsync();
                //int count = _employeeRepository.Add(employee);
                if (count > 0)
                    TempData["Message"] = "Employee Added !!";
 
                else
                    TempData["Message"] = "Employee Not Added !! ";
                return RedirectToAction(nameof(Index));

            }
            return View();
        }
        public IActionResult Create() => View();


        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest(); // Error 400

            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            //var employee = _employeeRepository.GetById(id.Value);

            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);

            if (employeeViewModel is null)
                return NotFound();

            return View(ViewName, employeeViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel employee)
        {
            if(id != employee.Id)
                return BadRequest();

            if(employee.ImageName is not null)
            {
                DocumentSettings.DeleteFile(employee.ImageName, "images");
            }
           employee.ImageName= DocumentSettings.UploadFile(employee.Image, "images");


            var employeeViewModel = _mapper.Map<Employee>(employee);
            if (ModelState.IsValid)
            {
                _unitOfWork.EmployeeRepository.Update(employeeViewModel);
                int count = await _unitOfWork.CompleteAsync();

                //int count = _employeeRepository.Update(employeeViewModel);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(employee);
        }
        public async Task<IActionResult> Edit(int? id) => await Details(id, "Edit");



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel employee)
        {
            if(id !=employee.Id)
                return BadRequest();

            var employeeViewModel = _mapper.Map<Employee>(employee);
            _unitOfWork.EmployeeRepository.Delete(employeeViewModel);
            int count = await _unitOfWork.CompleteAsync();

            //var count = _employeeRepository.Delete(employeeViewModel);
            if (count > 0)
            {
                DocumentSettings.DeleteFile(employee.ImageName,"images");
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }
        public async Task<IActionResult> Delete(int? id) => await Details(id, "Delete");

    }
}
