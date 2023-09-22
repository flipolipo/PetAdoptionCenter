using System.Reflection;
using FluentValidation;

namespace SImpleWebLogic.Configuration;

public class ValidatorFactory
{
    private readonly Dictionary<Type, object> _validators;

    public ValidatorFactory()
    {
        _validators = new Dictionary<Type, object>();
        LoadValidatorsFromAssembly(Assembly.GetExecutingAssembly());     }

    public AbstractValidator<T> GetValidator<T>()
    {
        if (_validators.TryGetValue(typeof(T), out var validator))
        {
            return (AbstractValidator<T>)validator;
        }
        return null;
    }

    private void LoadValidatorsFromAssembly(Assembly assembly)
    {
        var validatorTypes = assembly.GetTypes().Where(type =>
            type.BaseType != null &&
            type.BaseType.IsGenericType &&
            type.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>)
        );

        foreach (var validatorType in validatorTypes)
        {
            var genericArguments = validatorType.BaseType.GetGenericArguments();
            if (genericArguments.Length == 1)
            {
                var targetType = genericArguments[0];
                var validatorInstance = Activator.CreateInstance(validatorType);
                _validators[targetType] = validatorInstance;
            }
        }
    }
}

