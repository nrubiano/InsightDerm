using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsightDerm.Core.Service.Interfaces
{
    public interface IBaseService<TDto> where TDto : class
    {
        Task<List<TDto>> GetAll();

        Task<TDto> Create(TDto entity);

        void Remove(TDto entity);

        TDto Update(TDto entity);        
    }
}
