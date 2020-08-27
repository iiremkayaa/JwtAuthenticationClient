using JwtProjectClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtProjectClient.ApiServices.Interfaces
{
    public interface IProductApiService
    {
        Task<List<ProductList>> GetAllAsync();
        Task AddAsync(ProductAdd productAdd);
        Task<ProductList> GetByIdAsync(int id);
        Task UpdateAsync(ProductList productList);
        Task DeleteAsync(int id);
    }
}
