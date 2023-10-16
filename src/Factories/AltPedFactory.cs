using AltV.Atlas.Peds.Base;
using AltV.Net;
using AltV.Net.Elements.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace AltV.Atlas.Peds.Factories;

public class AltPedFactory : IEntityFactory<IPed>
{
    private readonly IServiceProvider _serviceProvider;

    public AltPedFactory( IServiceProvider serviceProvider )
    {
        _serviceProvider = serviceProvider;
    }
    
    public IPed Create( ICore core, IntPtr entityPointer, uint id )
    {
        return ActivatorUtilities.CreateInstance<AtlasPed>( _serviceProvider, core, entityPointer, id );
    }
}