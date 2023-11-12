using AltV.Icarus.Peds.Base;
using AltV.Icarus.Peds.Interfaces;
using AltV.Net.Async;
using AltV.Net.Data;
using Microsoft.Extensions.Logging;

namespace AltV.Icarus.Peds.Factories;

public class IcarusPedFactory
{
    private readonly ILogger<IcarusPedFactory> _logger;

    public IcarusPedFactory( ILogger<IcarusPedFactory> logger )
    {
        _logger = logger;
    }

    public async Task<T> CreatePedAsync<T>( string model, Position position, Rotation rotation ) where T : IIcarusPed
    {
        return (T) await AltAsync.CreatePed( model, position, rotation );
    }
    
    public async Task<IIcarusPed> CreatePedAsync( string model, Position position, Rotation rotation )
    {
        return (IIcarusPed) await AltAsync.CreatePed( model, position, rotation );
    }
}