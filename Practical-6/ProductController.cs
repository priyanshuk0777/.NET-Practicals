using Microsoft.AspNetCore.Mvc;
using p6.Models;

namespace p6.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new List<Product>()
        {
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 55000,
                category = "Electronics",
                Description = "High performance laptop"
            },

            new Product
            {
                Id = 2,
                Name = "Mobile",
                Price = 25000,
                category = "Electronics",
                Description = "Smart Android mobile"
            },

            new Product
            {
                Id = 3,
                Name = "Headphones",
                Price = 2000,
                category = "Accessories",
                Description = "Wireless headphones"
            },

            new Product
            {
                Id = 4,
                Name = "Keyboard",
                Price = 1500,
                category = "Accessories",
                Description = "Mechanical keyboard"
            }
        };

        // Product List + Search + Category Filter + Price Filter
        public IActionResult Index(string search, string category, decimal? minPrice, decimal? maxPrice)
        {
            var result = products.AsEnumerable();

            if (!string.IsNullOrEmpty(search))
            {
                result = result.Where(p =>
                    p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(category))
            {
                result = result.Where(p => p.category == category);
            }

            if (minPrice.HasValue)
            {
                result = result.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                result = result.Where(p => p.Price <= maxPrice.Value);
            }

            ViewBag.Categories = products
                .Select(p => p.category)
                .Distinct()
                .ToList();

            return View(result.ToList());
        }

        // Product Details
        public IActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // Create - GET
        public IActionResult Create()
        {
            return View();
        }

        // Create - POST
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = products.Max(p => p.Id) + 1;

                products.Add(product);

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // Edit - GET
        public IActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // Edit - POST
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct =
                    products.FirstOrDefault(p => p.Id == product.Id);

                if (existingProduct == null)
                {
                    return NotFound();
                }

                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.category = product.category;
                existingProduct.Description = product.Description;

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // Delete
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                products.Remove(product);
            }

            return RedirectToAction("Index");
        }
    }
}
