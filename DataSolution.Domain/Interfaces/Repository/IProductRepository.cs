using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSolution.Domain.Model.Data;

namespace DataSolution.Domain.Interfaces.Repository
{
    public interface IProductRepository
    {
        bool InsertProduct(ProductModel Product);
        bool UpdateProduct(ProductModel Product);
        bool DeleteProduct(ProductModel Product);
    }
}
