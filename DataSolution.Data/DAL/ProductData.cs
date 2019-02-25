using DataSolution.Data.Data;
using DataSolution.Domain.Interfaces.Repository;
using DataSolution.Domain.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSolution.Data.DAL
{
    public class ProductData : IProductRepository
    {
        public bool InsertProduct(ProductModel Product)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProduct(ProductModel Product)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduct(ProductModel Product)
        {

            throw new NotImplementedException();
        }

        public List<ProductModel> GetProductsByType(int ReportTypeID)
        {
            List<ProductModel> products = new List<ProductModel>();
            using (ProductEntities product = new ProductEntities())
            {
                 products = (from p in product.Products
                             join m in product.ReportTypes
                             on p.ReportTypeID equals m.ReportTypeID
                             select new ProductModel
                             {
                                 ProductID = p.ProductID,
                                 ProductName = p.ProductName
                             }).ToList();
            }

            return products;
        }
    }
}
