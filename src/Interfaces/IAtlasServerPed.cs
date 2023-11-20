using AltV.Atlas.Peds.Delegates;
using AltV.Atlas.Peds.Shared.Interfaces;
using AltV.Net.Elements.Entities;

namespace AltV.Atlas.Peds.Interfaces;

/// <summary>
/// Interface that exposes server-side ped events
/// </summary>
public interface IAtlasServerPed : IAtlasPed, IPed
{
    /// <summary>
    /// Triggered when the ped dies
    /// </summary>
    event PedDeadDelegate? OnDeath;
    
    /// <summary>
    /// Triggered when the ped takes damage
    /// </summary>
    event PedDamageDelegate? OnDamage;
    
    /// <summary>
    /// Triggered when the ped has gained health
    /// </summary>
    event PedHealDelegate? OnHeal;
    
    /// <summary>
    /// Triggered when the ped netowner has changed
    /// </summary>
    event PedNetOwnerChangeDelegate? OnNetOwnerChange;
    
    /// <summary>
    /// Triggered when the ped atlas task changed
    /// </summary>
    event PedTaskChangeDelegate? OnTaskChange;
    
    /// <summary>
    /// Give the ped a specific task
    /// </summary>
    /// <param name="pedTask">The ped task</param>>
    void SetPedTask<T>( T pedTask ) where T : class, IPedTask;
}