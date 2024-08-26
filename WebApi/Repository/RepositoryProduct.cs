using Microsoft.EntityFrameworkCore;
using WebApi.Config;
using WebApi.Entities;

namespace WebApi.Repository
{
    public class RepositoryProduct : IProduto
    {
        private readonly DbContextOptions<ContextBase> _db;

        public RepositoryProduct()
        {
            _db = new DbContextOptions<ContextBase>();
        }
        public async Task Add(ProductModel obj)
        {
            using(var data = new ContextBase(_db)) 
            {
                await data.Set<ProductModel>().AddAsync(obj);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(ProductModel obj)
        {
            using (var data = new ContextBase(_db))
            {
                 data.Set<ProductModel>().Remove(obj);
                await data.SaveChangesAsync();
            }
        }

        public async Task<List<ProductModel>> GetAll()
        {
            using (var data = new ContextBase(_db))
            {
                return await data.Set<ProductModel>().ToListAsync();
            }
        }

        public async Task<ProductModel> GetEntityById(int id)
        {
            using (var data = new ContextBase(_db))
            {
                return await data.Set<ProductModel>().FindAsync(id);
            }
        }

        public async Task Update(ProductModel obj)
        {
            using (var data = new ContextBase(_db))
            {
                data.Set<ProductModel>().Update(obj);
                await data.SaveChangesAsync();
            }
        }
    }
}
