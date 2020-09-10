using System;
using System.Collections.Generic;
using System.Text;

namespace Dannys.Framework.Services
{
    public interface IValidateObject<TEntity> where TEntity : class
    {
        ValidationResult Validate(TEntity entity, bool addNew);
    }
}
