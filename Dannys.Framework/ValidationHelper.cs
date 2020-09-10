using Dannys.Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Dannys.Framework
{
    public static class ValidationHelper
    {
        public const int NameMaxLength = 100;

        static Lazy<Regex> keyRegexLazy = new Lazy<Regex>(() => new Regex("^[0-9A-Za-z-_.]{1,30}$"));
        public static bool ValidateKey(this ValidationResult result, IKey entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Key))
                result.AddError(nameof(IKey.Key), "Key cannot be empty");
            else if (!keyRegexLazy.Value.IsMatch(entity.Key))
                result.AddError(nameof(IKey.Key), "Key is not in correct format (Letters, number or symbols (dash, underscore, dot) with length between 1 and 30)");
            else
                return true;
            return false;
        }

        public static bool ValidateUniqueKey<TEntity, TKey, TUserKey>(this ValidationResult result, TEntity entity, IUniqueKeyRepository<TEntity, TKey, TUserKey> repository)
            where TEntity : class, IKey, IEntity<TKey, TUserKey>
            where TKey : struct, IEquatable<TKey>
            where TUserKey : struct, IEquatable<TUserKey>
        {
            if (result.ValidateKey(entity))
            {
                TKey id = repository.GetID(entity.Key);
                if (!default(TKey).Equals(id) && !entity.ID.Equals(id))
                    result.AddError(nameof(entity.Key), $"Key {entity.Key} is duplicated");
                else return true;
            }
            return false;
        }

        public static bool ValidateName(this ValidationResult result, IName entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
                result.AddError(nameof(IName.Name), "Name cannot be empty");
            else if (entity.Name.Length > NameMaxLength)
                result.AddError(nameof(IName.Name), $"Maximum length of name is {NameMaxLength}");
            else
                return true;
            return false;
        }
    }
}
