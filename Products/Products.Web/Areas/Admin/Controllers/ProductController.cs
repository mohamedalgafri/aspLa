using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Products.Web.Areas.Admin.Controllers;
using Products.Web.Data;
using Products.Web.Admin.Models;
using Products.Web.Admin.ViewModel;
using Products.Web.Services;

namespace Products.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {

        private ApplicationDbContext _db;
        private IFileService _formFile;

        public ProductController(ApplicationDbContext db , IFileService formFile)
        {
            _db = db;
            _formFile = formFile;
        }

        public IActionResult Index() {

            var Products = _db.Products.Include(x=>x.Category).Where(x=> !x.IsDeleted).OrderByDescending(x=>x.CreatedAt).ToList();
            
            return  View(Products) ;
        }

        [HttpGet]
        public IActionResult Create() {
            ViewData["CategoryList"] = new SelectList(_db.Categories.Where(x => !x.IsDeleted).ToList(), "Id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateProductViewModel input)
        {

            if (ModelState.IsValid)
            {
                var nameIsExist = _db.Products.Any(x => x.Name == input.Name && !x.IsDeleted);
                if (nameIsExist)
                {
                    TempData["msg"] = "e: Product Name is Already Exist!";
                    ViewData["CategoryList"] = new SelectList(_db.Categories.Where(x => !x.IsDeleted).ToList(), "Id", "Name");
                    return View(input);
                }

                var product = new Product();
                product.Name = input.Name;
                product.Description = input.Description;
                product.Price = input.Price;
                if(input.ImageUrl != null)
                {
                    product.ImageUrl = await _formFile.SaveFile(input.ImageUrl, "images");
                }
                //product.ImageUrl = input.ImageUrl;
                product.CategoryId = input.CategoryId;
                product.CreatedAt = DateTime.Now;

                _db.Products.Add(product);
                _db.SaveChanges();
                TempData["msg"] = "s: Product added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["CategoryList"] = new SelectList(_db.Categories.Where(x => !x.IsDeleted).ToList(), "Id", "Name");
                return View(input);
            }
        }


        [HttpGet]
        public IActionResult Update(int id)
        {

            var product = _db.Products.SingleOrDefault(x => x.Id == id && !x.IsDeleted);
            if (product == null)
            {
                return NotFound();
            }
            var vm = new UpdateProductViewModel();
            vm.Id = product.Id;
            vm.Name = product.Name;
            vm.Discrabtion = product.Description;

            return View(vm);

        }

        [HttpPost]
        public IActionResult Update(UpdateProductViewModel input)
        {
            if (ModelState.IsValid)
            {
                var product = _db.Products.SingleOrDefault(x => x.Id == input.Id && !x.IsDeleted);
                if (product == null)
                {
                    return NotFound();
                }
                product.Name = input.Name;
                product.Description = input.Discrabtion;
                _db.Products.Update(product);
                _db.SaveChanges();

                TempData["msg"] = "s: product Updated successfully";
                return RedirectToAction("Index");

            }
            return View(input);

        }

        public IActionResult Delete(int id) {

            var x = _db.Products.SingleOrDefault(x => x.Id == id && !x.IsDeleted);

            x.IsDeleted = true;
            _db.Products.Update(x);
            _db.SaveChanges();
            return RedirectToAction("Index");
            
        }
    }
}
