using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InsightDerm.Core.Service.Interfaces
{
    public interface IBaseService<TDto, TEntity> where TDto : class
    {
        IEnumerable<TDto> GetAll(Expression<Func<TEntity, bool>> predicate);
        
        IEnumerable<TDto> GetAll(string filter, string sort);

        TDto GetSingle(Expression<Func<TEntity, bool>> predicate);

        TDto Create(TDto entity);

        bool Exist(Expression<Func<TEntity, bool>> predicate);

        void Remove(TDto entity);

        TDto Update(TDto entity);        
    }
}
