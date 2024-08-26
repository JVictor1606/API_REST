using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProduto _repository;

        public ProductsController(IProduto repository)
        {
            _repository = repository;
        }

        [HttpGet("/api/List")]
        [Produces("application/json")]
        public async Task<object> List()
        {
            return await _repository.GetAll();
        }

        [HttpPost("/api/Add")]
        [Produces("application/json")]
        public async Task<object> Add(ProductModel product)
        {
            try
            {
                await _repository.Add(product);
            }
            catch (Exception e)
            {

                throw;
            }

            return Task.FromResult("OK");
        }


        [HttpPut("/api/Update")]
        [Produces("application/json")]
        public async Task<object> Update(ProductModel product)
        {
            try
            {
                await _repository.Update(product);
            }
            catch (Exception e)
            {

                throw;
            }
            return Task.FromResult("OK");
        }

        [HttpGet("/api/GetProduct")]
        [Produces("application/json")]
        public async Task<object> GetEntityById(int id)
        {
            return await _repository.GetEntityById(id);
        }

        [HttpDelete("/api/Delete")]
        [Produces("application/json")]
        public async Task<object> Delete(int id)
        {
            try
            {
                var product = await _repository.GetEntityById(id);
                await _repository.Delete(product);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
