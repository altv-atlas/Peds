using AltV.Icarus.Peds.Base;
using AltV.Icarus.Peds.Factories;
using AltV.Icarus.Peds.Interfaces;
using AltV.Net;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Elements.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace AltV.Icarus.Peds;

public static class PedModule
{
    public static IServiceCollection RegisterPedModule( this IServiceCollection serviceCollection )
    {
        serviceCollection.AddTransient<IIcarusPed, IcarusPed>( );
        serviceCollection.AddTransient<IcarusPedFactory>( );
        serviceCollection.AddTransient<IPed, AsyncPed>( );

        serviceCollection.AddTransient<IEntityFactory<IPed>, AltPedFactory>( );
        
        return serviceCollection;
    }
}