using AltV.Atlas.Peds.Base;
using AltV.Atlas.Peds.Factories;
using AltV.Atlas.Peds.Interfaces;
using AltV.Atlas.Peds.Shared.Factories;
using AltV.Atlas.Peds.Shared.Interfaces;
using AltV.Net;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Elements.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace AltV.Atlas.Peds;

/// <summary>
/// Entrypoint for atlas ped module
/// </summary>
public static class PedModule
{
    /// <summary>
    /// Registers the ped module and it's classes/interfaces
    /// </summary>
    /// <param name="serviceCollection">A service collection</param>
    /// <returns>The service collection</returns>
    public static IServiceCollection RegisterPedModule( this IServiceCollection serviceCollection )
    {
        serviceCollection.AddTransient<IAtlasServerPed, AtlasPed>( );
        serviceCollection.AddTransient<AtlasPedFactory>( );
        serviceCollection.AddTransient<IPed, AsyncPed>( );
        serviceCollection.AddTransient<PedTaskFactory>( );

        serviceCollection.AddTransient<IEntityFactory<IPed>, AltPedFactory>( );
        
        return serviceCollection;
    }
}