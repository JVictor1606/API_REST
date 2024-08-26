using WebApi.Entities;

namespace WebApi.Repository
{
    public interface IProduto
    {
        Task Add(ProductModel obj);
        Task Update(ProductModel obj);
        Task Delete(ProductModel obj);
        Task<ProductModel> GetEntityById(int id);
        Task<List<ProductModel>> GetAll();
    }
}
