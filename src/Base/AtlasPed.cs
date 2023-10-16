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

public class AtlasPed : AsyncPed, IAtlasPed
{
    private readonly ILogger<AtlasPed> _logger;
    private readonly Dictionary<EPedTask, IPedTaskData> _netOwnerBuffer = new( );
    private EPedTask _currentTask = EPedTask.Idle;
    
    public event PedDeadDelegate? OnDeath;
    public event PedDamageDelegate? OnDamage;
    public event PedHealDelegate? OnHeal;
    public event PedNetOwnerChangeDelegate? OnNetOwnerChange;
    public event PedTaskChangeDelegate? OnTaskChange;

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

    private void EmitPedTask( EPedTask task, IPedTaskData data )
    {
        NetworkOwner.Emit( "Icarus.SetPedTask", this, ( int ) task, data );
    }

    private void AddToNetOwnerBuffer( EPedTask task, IPedTaskData data )
    {
        _netOwnerBuffer.Add( task, data );
    }

    public void SetToWander( Vector3 wanderArea, uint radius, uint minLength, uint timeBetweenWalks )
    {
        var task = new PedTaskDataWander( wanderArea, radius, minLength, timeBetweenWalks );
        SetPedTask( EPedTask.Wander, task );
    }
    
    public void FollowEntity( IEntity entity )
    {
        var task = new PedTaskDataFollowEntity( entity );
        SetPedTask( EPedTask.FollowTargetEntity, task );
    }
    
    public void MoveToTargetPosition( Position targetPosition )
    {
        var task = new PedTaskDataMoveToTargetPosition( targetPosition );
        SetPedTask( EPedTask.MoveToTargetPosition, task );
    }
}