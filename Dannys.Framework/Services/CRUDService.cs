using Dannys.Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dannys.Framework.Services
{
    public class CRUDService<TEntity, TKey, TUserKey> : ICRUDService<TEntity, TKey, TUserKey>, IValidateObject<TEntity>
        where TEntity : class, IEntity<TKey, TUserKey>
        where TKey : struct, IEquatable<TKey>
        where TUserKey : struct, IEquatable<TUserKey>
    {
        ICRUDRepository<TEntity, TKey, TUserKey> repository;
        public CRUDService(ICRUDRepository<TEntity, TKey, TUserKey> repository)
        {
            this.repository = repository;
        }
        public virtual OperationResult<TEntity> Create(TEntity entity)
        {
            var validateResult = Validate(entity, true);
            if (!validateResult.Passed) return new OperationResult<TEntity>(validateResult);

            try
            {
                repository.Create(entity);
                repository.SaveChanges();

                return new OperationResult<TEntity>(entity);
            }
            catch (Exception e)
            {
                return new OperationResult<TEntity>(e.Message);
            }
        }

        public virtual OperationResult Delete(TKey id)
        {
            try
            {
                repository.Delete(id);
                repository.SaveChanges();

                return new OperationResult();
            }
            catch (Exception e)
            {
                return new OperationResult(e.Message);
            }
        }

        public virtual TEntity Get(TKey id)
        {
            return repository.Get(id);
        }

        public virtual IEnumerable<TEntity> Search(SearchOptions<TEntity> options)
        {
            return repository.Search(options);
        }

        public virtual int SearchCount(SearchCountOptions<TEntity> options)
        {
            return repository.SearchCount(options);
        }

        public virtual OperationResult Update(TEntity entity)
        {
            var validateResult = Validate(entity, false);
            if (!validateResult.Passed) return new OperationResult(validateResult);

            try
            {
                repository.Update(entity);
                repository.SaveChanges();

                return new OperationResult();
            }
            catch (Exception e)
            {
                return new OperationResult(e.Message);
            }
        }

        public virtual ValidationResult Validate(TEntity entity, bool addNew)
        {
            return new ValidationResult();
        }
    }
}
