using AltV.Atlas.Peds.Delegates;
using AltV.Atlas.Peds.Shared.Interfaces;

namespace AltV.Atlas.Peds.Interfaces;

/// <summary>
/// Interface that exposes server-side ped events
/// </summary>
public interface IAtlasServerPed : IAtlasPed
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
}