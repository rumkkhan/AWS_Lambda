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

        private readonly PlutoCodeFirstContext _plutoCodeFirstContext = new PlutoCodeFirstContext();

        //1 _plutoCodeFirstContext.Courses.Where(x => x.Level == 1).ToList();
        //var courses = _plutoCodeFirstContext.Courses.Where(x => x.Level == 1).OrderBy(y => y.FullPrice).ThenBy(x => x.Title).ToList();
        // var courses = _plutoCodeFirstContext.Courses.Where(x => x.Level == 1).OrderByDescending(y => y.Id).OrderByDescending(x => x.FullPrice).ToList();
        //---------------------------------------------------
        //  var courses = _plutoCodeFirstContext.Courses.Where(x => x.Level == 1).OrderByDescending(y => y.Id).OrderByDescending(x => x.FullPrice).Select(x => new { Title = x.Title, FullPirce = x.FullPrice}).ToList();
        //    List<Courses> course = new List<Courses>();
        //        foreach (var item in courses)
        //        {
        //            course.Add(new Courses { FullPrice = item.FullPirce, Title = item.Title
        //});
        //        }
        //-----------------------------------------------------
        // var courses = _plutoCodeFirstContext.Courses.Where(x => x.Level == 1).SelectMany(x => x.TagCourses).Distinct();
        //========================
        //groupby
//        var courses = _plutoCodeFirstContext.Courses.ToList().GroupBy(x => x.Level);

//            foreach (var group in courses)
//            {
//                var key = group.Key;
//                foreach (var item in group)
//                { 
//                    var name = item.Title;
//    }
//}

[HttpGet]
        public List<Courses> GetCouses()
        {
            var courses = _plutoCodeFirstContext.Courses.ToList().GroupBy(x => x.Level);

            foreach (var  group in courses)
            {
                var key = group.Key;
                foreach (var item in group)
                {
                    var name = item.Title;
                }
            }

            return null;
            
        }
    }
} 