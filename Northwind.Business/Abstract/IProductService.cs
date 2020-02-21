using Northwind.Entities.Concrete;
using System.Collections.Generic;

namespace Northwind.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();

        List<Product> GetProductsByCategory(int categoryId);

        List<Product> GetProductByProductName(string key);

        void Add(Product product);

        void Update(Product product);

        void Delete(Product product);
    }
}