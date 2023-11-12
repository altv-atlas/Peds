using System.Numerics;
using AltV.Atlas.Peds.Delegates;
using AltV.Atlas.Peds.Enums;
using AltV.Atlas.Peds.Interfaces;
using AltV.Atlas.Peds.PedTasks;
using AltV.Net;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Microsoft.Extensions.Logging;

namespace AltV.Atlas.Peds.Base;

/// <summary>
/// Base Atlas peds class, can be inherited from to extend functionality
/// </summary>
public class AtlasPed : AsyncPed, IAtlasPed
{
    private readonly ILogger<AtlasPed> _logger;
    private readonly Dictionary<EPedTask, IPedTaskData> _netOwnerBuffer = new( );
    private EPedTask _currentTask = EPedTask.Idle;
    
    /// <summary>
    /// Triggered when the ped dies
    /// </summary>
    public event PedDeadDelegate? OnDeath;
    
    /// <summary>
    /// Triggered when the ped takes damage
    /// </summary>
    public event PedDamageDelegate? OnDamage;
    
    /// <summary>
    /// Triggered when the ped has gained health
    /// </summary>
    public event PedHealDelegate? OnHeal;
    
    /// <summary>
    /// Triggered when the ped netowner has changed
    /// </summary>
    public event PedNetOwnerChangeDelegate? OnNetOwnerChange;
    
    /// <summary>
    /// Triggered when the ped atlas task changed
    /// </summary>
    public event PedTaskChangeDelegate? OnTaskChange;

    /// <summary>
    /// Creates a new atlas ped
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="core">Alt Core</param>
    /// <param name="nativePointer">Alt nativeptr</param>
    /// <param name="id">ID of the ped</param>
    public AtlasPed( ILogger<AtlasPed> logger, ICore core, IntPtr nativePointer, uint id ) : base( core, nativePointer, id )
    {
        _logger = logger;
        Alt.OnNetworkOwnerChange += OnNetworkOwnerChange;
        Alt.OnPedDead += OnPedDead;
        Alt.OnPedDamage += OnPedDamage;
        Alt.OnPedHeal += OnPedHeal;
    }

    private void OnPedHeal( IPed ped, ushort oldHealth, ushort newHealth, ushort oldArmour, ushort newArmour )
    {
        if( !ped.Equals( this ) )
            return;
        
        OnHeal?.Invoke( oldHealth, newHealth, oldArmour, newArmour );
    }

    private void OnPedDamage( IPed ped, IEntity attacker, uint weapon, ushort healthDamage, ushort armourDamage )
    {
        if( !ped.Equals( this ) )
            return;
        
        OnDamage?.Invoke( attacker, weapon, healthDamage, armourDamage );
    }

    private void OnPedDead( IPed ped, IEntity killer, uint weapon )
    {
        if( !ped.Equals( this ) )
            return;
        
        OnDeath?.Invoke( killer, weapon );
    }

    private void OnNetworkOwnerChange( IEntity target, IPlayer? oldNetOwner, IPlayer? newNetOwner )
    {
        if( !target.Equals( this ) || oldNetOwner != null || _netOwnerBuffer.Count == 0 )
            return;
        
        foreach( var (key, value) in _netOwnerBuffer )
        {
            EmitPedTask( key, value );
        }

        _currentTask = _netOwnerBuffer.LastOrDefault( ).Key;
        _netOwnerBuffer.Clear( );
        
        OnNetOwnerChange?.Invoke( oldNetOwner, newNetOwner );
    }

    private void EmitPedTask( EPedTask task, IPedTaskData data )
    {
        NetworkOwner.Emit( "Atlas.SetPedTask", this, ( int ) task, data );
    }

    private void AddToNetOwnerBuffer( EPedTask task, IPedTaskData data )
    {
        _netOwnerBuffer.Add( task, data );
    }

    /// <summary>
    /// Give the ped a specific task
    /// </summary>
    /// <param name="task">The task to give</param>
    /// <param name="data">Optional data for the task</param>
    public void SetPedTask( EPedTask task, IPedTaskData data )
    {
        if( NetworkOwner is null )
        {
            AddToNetOwnerBuffer( task, data );
            return;
        }

        EmitPedTask( task, data );
        OnTaskChange?.Invoke( _currentTask, task );
        _currentTask = task;
    }

    /// <summary>
    /// Set this ped to wander in area (does not work for all peds)
    /// </summary>
    /// <param name="wanderArea">The area to wander around in</param>
    /// <param name="radius">Radius from "wanderArea"</param>
    /// <param name="minLength">Minimum length to walk</param>
    /// <param name="timeBetweenWalks">Wait time between walks</param>
    public void SetToWander( Vector3 wanderArea, uint radius, uint minLength, uint timeBetweenWalks )
    {
        var task = new PedTaskDataWander( wanderArea, radius, minLength, timeBetweenWalks );
        SetPedTask( EPedTask.Wander, task );
    }
    
    /// <summary>
    /// Set this ped to follow another entity
    /// Only players are supported currently
    /// </summary>
    /// <param name="entity">The entity to follow</param>
    public void FollowEntity( IEntity entity )
    {
        var task = new PedTaskDataFollowEntity( entity );
        SetPedTask( EPedTask.FollowTargetEntity, task );
    }
    
    /// <summary>
    /// Set this ped to move (walk) towards the target position
    /// </summary>
    /// <param name="targetPosition">The position to move towards</param>
    public void MoveToTargetPosition( Position targetPosition )
    {
        var task = new PedTaskDataMoveToTargetPosition( targetPosition );
        SetPedTask( EPedTask.MoveToTargetPosition, task );
    }
}