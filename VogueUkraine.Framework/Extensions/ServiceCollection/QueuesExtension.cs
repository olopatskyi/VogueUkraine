using System;
using System.Drawing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VogueUkraine.Framework.Services.QueueService.Processor;
using VogueUkraine.Framework.Services.QueueService.Service;
using VogueUkraine.Framework.Services.QueueService.Storage.Entities;
using VogueUkraine.Framework.Services.QueueService.Storage.Interfaces;

namespace VogueUkraine.Framework.Extensions.ServiceCollection;

/// <summary>
/// Class representing configuration options for a queue.
/// </summary>
public class QueueOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether the queue is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = true;
}

public static class QueuesExtension
{

    /// <summary>
    /// Registers a queue service with a repository in the provided service collection.
    /// </summary>
    /// <typeparam name="TTask">Type representing a queue element entity.</typeparam>
    /// <typeparam name="TRepository">Type representing a queue repository.</typeparam>
    /// <typeparam name="TQueueService">Type representing a queue service.</typeparam>
    /// <param name="config">Configuration object providing queue options.</param>
    /// <param name="services">Service collection in which to register the queue service.</param>
    /// <param name="queueOptionsPath">Path to the section of the configuration containing the queue options.</param>
    public static void AddQueueService<TTask, TRepository, TQueueService>
    (this IServiceCollection services, IConfiguration config, string queueOptionsPath)
        where TTask : QueueElementEntity<string>
        where TRepository : class, IQueueRepository<TTask>
        where TQueueService : class, IQueueService<TTask>
    {
        var options = config.GetSection(queueOptionsPath).Get<QueueOptions>();
        // in case if the configuration section is missing - we should enable queue worker by default. 
        var isEnabled = options?.IsEnabled ?? true;
        
        services.AddSingleton<IQueueRepository<TTask>, TRepository>();
        services.AddSingleton<IQueueService<TTask>, TQueueService>();
        if (isEnabled) services.AddSingleton<IHostedService, QueueWorker<TTask>>();
        
        PrintStatusToConsole(queueOptionsPath, isEnabled);
    }
    
    /// <summary>
    /// Registers a queue service with a processor in the provided service collection.
    /// </summary>
    /// <typeparam name="TTask">Type representing a queue element entity.</typeparam>
    /// <typeparam name="TProcessor">Type representing a queue element processor.</typeparam>
    /// <typeparam name="TQueueService">Type representing a queue service.</typeparam>
    /// <param name="services">Service collection in which to register the queue service.</param>
    /// <param name="config">Configuration object providing queue options.</param>
    /// <param name="queueOptionsPath">Path to the section of the configuration containing the queue options.</param>
    /// <param name="additionalConfig">Additional service configuration to be performed after registering the queue service.</param>
    public static void AddQueueService<TTask, TProcessor, TQueueService>
    (this IServiceCollection services, IConfiguration config, string queueOptionsPath,
        Action<IServiceCollection> additionalConfig)
        where TTask : QueueElementEntity<string>
        where TProcessor : QueueElementProcessor<TTask>
        where TQueueService : class, IQueueService<TTask>
    {
        var options = config.GetSection(queueOptionsPath).Get<QueueOptions>();
        // in case if the configuration section is missing - we should enable queue worker by default. 
        var isEnabled = options?.IsEnabled ?? true;
        
        services.AddSingleton<QueueElementProcessor<TTask>, TProcessor>();
        services.AddSingleton<IQueueService<TTask>, TQueueService>();
        if (isEnabled) services.AddSingleton<IHostedService, QueueWorker<TTask>>();

        additionalConfig?.Invoke(services);

        PrintStatusToConsole(queueOptionsPath, isEnabled);
    }

    private static void PrintStatusToConsole(string queueOptionsPath, bool isEnabled)
    {
        Console.ForegroundColor = isEnabled ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine($"{queueOptionsPath} is {(isEnabled ? "enabled" : "disabled")}");
        Console.ResetColor();
    }
}