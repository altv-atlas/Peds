using AltV.Atlas.Peds.Interfaces;
using AltV.Atlas.Peds.Shared.Interfaces;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Enums;
using Microsoft.Extensions.Logging;

namespace AltV.Atlas.Peds.Factories;

/// <summary>
/// Factory to create new atlas peds
/// </summary>
public class AtlasPedFactory
{
    private readonly ILogger<AtlasPedFactory> _logger;

    /// <summary>
    /// Creates a new instance of this factory
    /// </summary>
    /// <param name="logger">A logger instance</param>
    public AtlasPedFactory( ILogger<AtlasPedFactory> logger )
    {
        _logger = logger;
    }

    /// <summary>
    /// Create a new ped of type T
    /// </summary>
    /// <param name="model">The model of the ped</param>
    /// <param name="position">The position to spawn the ped at</param>
    /// <param name="rotation">The rotation to spawn the ped at</param>
    /// <typeparam name="T">Type of the ped, by default can be IAtlasPed</typeparam>
    /// <returns>A new ped of type T</returns>
    public async Task<T> CreatePedAsync<T>( string model, Position position, Rotation rotation ) where T : IAtlasPed
    {
        return (T) await AltAsync.CreatePed( model, position, rotation );
    }

    /// <summary>
    /// Create a new ped of type T
    /// </summary>
    /// <param name="model">The model of the ped</param>
    /// <param name="position">The position to spawn the ped at</param>
    /// <param name="rotation">The rotation to spawn the ped at</param>
    /// <typeparam name="T">Type of the ped, by default can be IAtlasPed</typeparam>
    /// <returns>A new ped of type T</returns>
    public async Task<T> CreatePedAsync<T>( PedModel model, Position position, Rotation rotation ) where T : IAtlasPed
    {
        return (T) await AltAsync.CreatePed( model, position, rotation );
    }
    
    /// <summary>
    /// Create a new ped of type IAtlasPed
    /// </summary>
    /// <param name="model">The model of the ped</param>
    /// <param name="position">The position to spawn the ped at</param>
    /// <param name="rotation">The rotation to spawn the ped at</param>
    /// <returns>A new instance of IAtlasPed</returns>
    public async Task<IAtlasPed> CreatePedAsync( string model, Position position, Rotation rotation )
    {
        return (IAtlasPed) await AltAsync.CreatePed( model, position, rotation );
    }
    
    /// <summary>
    /// Create a new ped of type IAtlasPed
    /// </summary>
    /// <param name="model">The model of the ped</param>
    /// <param name="position">The position to spawn the ped at</param>
    /// <param name="rotation">The rotation to spawn the ped at</param>
    /// <returns>A new instance of IAtlasPed</returns>
    public async Task<IAtlasPed> CreatePedAsync( PedModel model, Position position, Rotation rotation )
    {
        return (IAtlasPed) await AltAsync.CreatePed( model, position, rotation );
    }
}