using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Products.Web.Areas.Admin.Controllers;
using Products.Web.Data;
using Products.Web.Admin.Models;
using Products.Web.Admin.ViewModel;

namespace Products.Web.Controllers
{
    public class CategoryController : BaseController
    {

        private ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult index()
        {
            var category = _db.Categories.Where(x=> !x.IsDeleted).ToList();
            return View(category);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {

                var category = _db.Categories.SingleOrDefault(x => x.Id == id && !x.IsDeleted);
                if (category == null)
                {
                    return NotFound();
                }
                var vm = new UpdateCategoryViewModel();
                vm.Id = category.Id;
                vm.Name = category.Name;

                 return View(vm);

        }

        [HttpPost]
        public IActionResult Update(UpdateCategoryViewModel input)
        {
            if(ModelState.IsValid)
            {
                var category = _db.Categories.SingleOrDefault(x => x.Id == input.Id && !x.IsDeleted);
                if (category == null)
                {
                    return NotFound();
                }
                category.Name = input.Name;
                _db.Categories.Update(category);
                _db.SaveChanges();

                TempData["msg"] = "s: Category Updated successfully";
                return RedirectToAction("Index");
             
            }
            return View(input);

        }

        [HttpPost]
        public IActionResult Create(CreateCategoryViewModel input) {

            if(ModelState.IsValid)
            {
                var category = new Category();
                category.Name = input.Name;
                category.CreatedAt = DateTime.Now;
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["msg"] = "s: Category added successfully";
                return RedirectToAction("Index");
            }
            else
            {

                return View(input);
            }

        }

        public IActionResult Delete(int id)
        {
            var category = _db.Categories.SingleOrDefault(x => x.Id == id && !x.IsDeleted);
            category.IsDeleted = true;
            _db.Categories.Update(category);
            _db.SaveChanges();
            TempData["msg"] = "e: Category Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
