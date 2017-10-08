using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        private readonly IRepository<TEntity> _repository;

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;

            _mapper = mapper;
            _repository = UnitOfWork.GetRepository<TEntity>();
        }

        public IEnumerable<TDto> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _repository.GetPagedList(predicate, null, null, 0, 20);

            return _mapper.Map<IEnumerable<TDto>>(list.Items);
        }

		public TDto GetSingle(Expression<Func<TEntity, bool>> predicate)
		{
            var single = _repository.GetFirstOrDefault(predicate, null, null, true);

			return _mapper.Map<TDto>(single);
		}

        public TDto Create(TDto entity)
        {
            var toInsert = _mapper.Map<TEntity>(entity);

            _repository.Insert(toInsert);

            UnitOfWork.SaveChanges(true);

            _repository.Detach(toInsert);

            return _mapper.Map<TDto>(toInsert);
        }

		public bool Exist(Expression<Func<TEntity, bool>> predicate)
		{
			return _repository.Exist(predicate);
		}

        public void Remove(TDto entity)
        {
            var toDelete = _mapper.Map<TEntity>(entity);

            _repository.Delete(toDelete);

			UnitOfWork.SaveChanges(true);
        }

        public TDto Update(TDto entity)
        {
            var toUpdate = _mapper.Map<TEntity>(entity);

            _repository.Update(toUpdate);

            UnitOfWork.SaveChanges(true);

            _repository.Detach(toUpdate);

            return _mapper.Map<TDto>(toUpdate);
        }
    }
}
