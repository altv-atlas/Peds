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
    private static IServiceProvider? _serviceProvider;
    
    public static IServiceCollection RegisterPedModule( this IServiceCollection serviceCollection )
    {
        serviceCollection.AddTransient<IIcarusPed, IcarusPed>( );
        serviceCollection.AddTransient<IcarusPedFactory>( );
        serviceCollection.AddTransient<IPed, AsyncPed>( );

        serviceCollection.AddTransient<IEntityFactory<IPed>, AltPedFactory>( );
        
        return serviceCollection;
    }

    
    public static IServiceProvider InitializePedModule( this IServiceProvider serviceProvider )
    {
        _serviceProvider = serviceProvider;
        return serviceProvider;
    }

    public static IEntityFactory<IPed> GetFactory( )
    {
        if( _serviceProvider == null )
            throw new NullReferenceException( "Could not load PedFactory. Did you forget PedModule.InitializePedModule()?" );
        
        return _serviceProvider.GetService<IEntityFactory<IPed>>( )!;
    }
}