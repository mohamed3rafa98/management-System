using Demo.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Demo.PL.ViewModel
{
    public class DepartmentViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Code is Required !! ")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is Required !! ")]
        public string Name { get; set; }

        [DisplayName("Date Of Creation")]
        public DateTime DateOfCreation { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
