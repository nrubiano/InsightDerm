using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using InsightDerm.Core.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsightDerm.Core.Service
{
    public class BaseService<TEntity, TDto> : IBaseService<TDto, TEntity> 
                                                    where TDto : class
                                                    where TEntity : class
    {
        protected IUnitOfWork UnitOfWork { get; }

        private readonly IMapper _mapper;

        private readonly IRepository<TEntity> _repository;

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;

            _mapper = mapper;
            _repository = UnitOfWork.Repository<TEntity>();
        }

        public IEnumerable<TDto> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _repository.Query(predicate).Select(x => x);

            return _mapper.Map<IEnumerable<TDto>>(list);
        }

		public TDto GetSingle(Expression<Func<TEntity, bool>> predicate)
		{
            var single = _repository.Query(predicate).FirstOrDefault();

			return _mapper.Map<TDto>(single);
		}

        public async Task<TDto> Create(TDto entity)
        {
            var toInsert = _mapper.Map<TEntity>(entity);

            await _repository.InsertAsync(toInsert);

            await UnitOfWork.SaveChangesAsync(true);

            return _mapper.Map<TDto>(toInsert);
        }

        public void Remove(TDto entity)
        {
            var toDelete = _mapper.Map<TEntity>(entity);

            UnitOfWork.SaveChangesAsync(true);

            _repository.Delete(toDelete);
        }

        public TDto Update(TDto entity)
        {
            var toUpdate = _mapper.Map<TEntity>(entity);

            _repository.Update(toUpdate);

            UnitOfWork.SaveChangesAsync(true);

            return _mapper.Map<TDto>(toUpdate);
        }
    }
}
