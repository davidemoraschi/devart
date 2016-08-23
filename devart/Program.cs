using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace devart
{
    class Program
    {
        static void Main(string[] args)
        {
            CrmDemoEntities context = new CrmDemoEntities();

            // Create a new category
            ProductCategories newCategory = new ProductCategories();
            newCategory.CategoryID = 1001;
            newCategory.CategoryName = "New category";
            context.AddToProductCategories(newCategory);

            // Create a new product
            Products newProduct = new Products();
            newProduct.ProductID = 2001;
            newProduct.ProductName = "New product";
            newProduct.Price = 20;
            // Associate the new product with the new category
            newProduct.ProductCategories = newCategory;
            context.AddToProducts(newProduct);

            // Send the changes to the database.
            // Until you do it, the changes are cached on the client side.
            context.SaveChanges();

            // Request the new product from the database
            var query = from it in context.Products.Include("ProductCategories")
                        where it.ProductID == 2000
                        select it;

            // Since we query for a single object instead of a collection, we can use the method First()
            Products product = query.First();
            Console.WriteLine("{0} | {1} | {2}",
              product.ProductCategories.CategoryName, product.ProductName, product.Price);
            product.ProductName = "Edited product";
            product.Price = 15;
            context.SaveChanges();
            Console.ReadLine();
        }
    }
}
