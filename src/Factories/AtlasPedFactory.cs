using AltV.Atlas.Peds.Interfaces;
using AltV.Net.Async;
using AltV.Net.Data;
using Microsoft.Extensions.Logging;

namespace AltV.Atlas.Peds.Factories;

public class AtlasPedFactory
{
    private readonly ILogger<AtlasPedFactory> _logger;

    public AtlasPedFactory( ILogger<AtlasPedFactory> logger )
    {
        _logger = logger;
    }

    public async Task<T> CreatePedAsync<T>( string model, Position position, Rotation rotation ) where T : IAtlasPed
    {
        return (T) await AltAsync.CreatePed( model, position, rotation );
    }
    
    public async Task<IAtlasPed> CreatePedAsync( string model, Position position, Rotation rotation )
    {
        return (IAtlasPed) await AltAsync.CreatePed( model, position, rotation );
    }
}