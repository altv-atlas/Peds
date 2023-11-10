using System.Numerics;
using AltV.Atlas.Peds.Delegates;
using AltV.Atlas.Peds.Enums;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;

namespace AltV.Atlas.Peds.Interfaces;

/// <summary>
/// Base blueprint for atlas peds
/// </summary>
public interface IAtlasPed : IPed
{
    /// <summary>
    /// Give the ped a specific task
    /// </summary>
    /// <param name="task">The task to give</param>
    /// <param name="data">Optional data for the task</param>>
    void SetPedTask( EPedTask task, IPedTaskData data );
    
    /// <summary>
    /// Set this ped to wander in area (does not work for all peds)
    /// </summary>
    /// <param name="wanderArea">The area to wander around in</param>
    /// <param name="radius">Radius from "wanderArea"</param>
    /// <param name="minLength">Minimum length to walk</param>
    /// <param name="timeBetweenWalks">Wait time between walks</param>
    void SetToWander( Vector3 wanderArea, uint radius, uint minLength, uint timeBetweenWalks );
    
    /// <summary>
    /// Set this ped to follow another entity
    /// Only players are supported currently
    /// </summary>
    /// <param name="entity">The entity to follow</param>
    void FollowEntity( IEntity entity );
    
    /// <summary>
    /// Set this ped to move (walk) towards the target position
    /// </summary>
    /// <param name="targetPosition">The position to move towards</param>
    void MoveToTargetPosition( Position targetPosition );

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