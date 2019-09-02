using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using InsightDerm.Core.Data;
using InsightDerm.Core.Service.Interfaces;

namespace InsightDerm.Core.Service
{
    public class BaseService<TEntity, TDto> : IBaseService<TDto, TEntity> 
                                                    where TDto : class
                                                    where TEntity : class
    {
        protected IUnitOfWork UnitOfWork { get; }

        private readonly IMapper _mapper;

        protected readonly IRepository<TEntity> Repository;

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;

            _mapper = mapper;
            Repository = UnitOfWork.GetRepository<TEntity>();
        }

        public IEnumerable<TDto> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            var list = Repository.GetPagedList(predicate, null, null, 0, 20);

            return _mapper.Map<IEnumerable<TDto>>(list.Items);
        }

        public IEnumerable<TDto> GetAll(string filter, string sort)
        {
            var list = Repository.GetPagedList(filter);

            return _mapper.Map<IEnumerable<TDto>>(list.Items);
        }
        
		public TDto GetSingle(Expression<Func<TEntity, bool>> predicate)
		{
            var single = Repository.GetFirstOrDefault(predicate, null, null, true);

			return _mapper.Map<TDto>(single);
		}

        public TDto Create(TDto entity)
        {
            var toInsert = _mapper.Map<TEntity>(entity);

            Repository.Insert(toInsert);

            UnitOfWork.SaveChanges(true);

            Repository.Detach(toInsert);

            return _mapper.Map<TDto>(toInsert);
        }

		public bool Exist(Expression<Func<TEntity, bool>> predicate)
		{
			return Repository.Exist(predicate);
		}

        public void Remove(TDto entity)
        {
            var toDelete = _mapper.Map<TEntity>(entity);

            Repository.Delete(toDelete);

			UnitOfWork.SaveChanges(true);
        }

        public TDto Update(TDto entity)
        {
            var toUpdate = _mapper.Map<TEntity>(entity);

            Repository.Update(toUpdate);

            UnitOfWork.SaveChanges(true);

            Repository.Detach(toUpdate);

            return _mapper.Map<TDto>(toUpdate);
        }
    }
}
