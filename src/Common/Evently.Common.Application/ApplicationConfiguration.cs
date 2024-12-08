using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Common.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
        Assembly[] moduleAssemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);
           
            // Регистрация поведения обработки исключений
            // [!] Решение сквозной задачи: обработка исключений
            config.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));

            // Регистрация поведения логирования запросов
            // [!] Решение сквозной задачи: логирование
            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));

            // Регистрация поведения валидации запросов
            // [!] Решение сквозной задачи: валидация
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));

            // [!] Решение сквозной задачи: кэширование
            config.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(moduleAssemblies,
            includeInternalTypes: true);

        return services;
    }
}
