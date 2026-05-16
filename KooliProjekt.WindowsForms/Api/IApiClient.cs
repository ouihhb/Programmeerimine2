using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.WindowsForms.Api
{
    public interface IApiClient
    {
        Task<OperationResult<List<Product>>> List();
        Task<OperationResult<int>> Save(Product product);
        Task<OperationResult> Delete(int id);
    }
}
