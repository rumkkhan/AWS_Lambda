using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using linqq.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace linqq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {

        private readonly PlutoCodeFirstContext _plutoCodeFirstContext;

        public CoursesController(PlutoCodeFirstContext plutoCodeFirstContext)
        {
            _plutoCodeFirstContext = plutoCodeFirstContext;
        }

        [HttpGet]
        public List<Courses> GetCouses()
        {
            var courses = _plutoCodeFirstContext.Courses.ToList();
            return courses;
        }
    }
}