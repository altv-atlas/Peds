using AltV.Atlas.Peds.Base;
using AltV.Atlas.Peds.Factories;
using AltV.Atlas.Peds.Interfaces;
using AltV.Net;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Elements.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace AltV.Atlas.Peds;

public static class PedModule
{
    public static IServiceCollection RegisterPedModule( this IServiceCollection serviceCollection )
    {
        serviceCollection.AddTransient<IAtlasPed, AtlasPed>( );
        serviceCollection.AddTransient<AtlasPedFactory>( );
        serviceCollection.AddTransient<IPed, AsyncPed>( );

        serviceCollection.AddTransient<IEntityFactory<IPed>, AltPedFactory>( );
        
        return serviceCollection;
    }
}