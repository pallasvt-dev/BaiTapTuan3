using BaiTapTuan3.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiTapTuan3.Controllers
{
    public class StudentController : Controller
    {
        // Static list to store registered students
        private static List<Student> _registeredStudents = new List<Student>();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Student student)
        {
            // Check if MSSV already exists
            if (_registeredStudents.Any(s => s.MSSV == student.MSSV))
            {
                ModelState.AddModelError("MSSV", "MSSV này đã được đăng ký. Vui lòng nhập MSSV khác.");
                return View(student);
            }

            if (ModelState.IsValid)
            {
                // Add student to the list
                _registeredStudents.Add(student);

                // Count students with same major
                int soDangKyCungNganh = _registeredStudents.Count(s => s.ChuyenNganh == student.ChuyenNganh);

                // Store data in TempData (secure, not exposed in URL)
                TempData["MSSV"] = student.MSSV;
                TempData["HoTen"] = student.HoTen;
                TempData["ChuyenNganh"] = student.ChuyenNganh;
                TempData["SoDangKyCungNganh"] = soDangKyCungNganh;

                return RedirectToAction("ShowKQ");
            }

            return View(student);
        }

        public IActionResult ShowKQ()
        {
            return View();
        }
    }
}
