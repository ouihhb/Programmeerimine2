using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.WindowsForms.Api
{
    public interface IApiClient
    {
        Task<List<Product>> GetProducts();
    }
}