using System.Numerics;
using System.Text.Json;
using AltV.Icarus.Peds.Delegates;
using AltV.Icarus.Peds.Enums;
using AltV.Icarus.Peds.Interfaces;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Microsoft.Extensions.Logging;

namespace AltV.Icarus.Peds.Base;

public class IcarusPed : AsyncPed, IIcarusPed
{
    private readonly ILogger<IcarusPed> _logger;
    private readonly Dictionary<EPedTask, object[]> _netOwnerBuffer = new( );
    
    public event PedDeadDelegate? OnDeath;
    public event PedDamageDelegate? OnDamage;
    public event PedHealDelegate? OnHeal;
    public event PedNetOwnerChangeDelegate? OnNetOwnerChange;

    public IcarusPed( ILogger<IcarusPed> logger, ICore core, IntPtr nativePointer, uint id ) : base( core, nativePointer, id )
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
            EmitPedData( key, value );
        }
        
        OnNetOwnerChange?.Invoke( oldNetOwner, newNetOwner );
    }

    public void SetPedTask( EPedTask task, params object[] parameters )
    {
        if( NetworkOwner is null )
        {
            AddToNetOwnerBuffer( task, parameters );
            return;
        }

        EmitPedData( task, parameters );
    }

    private void EmitPedData( EPedTask task, object[]? value )
    {
        NetworkOwner.Emit( "Icarus.SetPedData", this, ( int ) task, JsonSerializer.Serialize( value ) );
    }

    private void AddToNetOwnerBuffer( EPedTask task, params object[] value )
    {
        _netOwnerBuffer.Add( task, value );
    }

    public void SetToWander( Vector3 wanderArea, uint radius, uint minLength, uint timeBetweenWalks )
    {
        SetPedTask( EPedTask.Wander, wanderArea, radius, minLength, timeBetweenWalks );
    }
    
    public void FollowEntity( IEntity entity )
    {
        SetPedTask( EPedTask.FollowTargetEntity, entity.Id );
    }
    
    public void MoveToTargetPosition( Position targetPosition )
    {
        SetPedTask( EPedTask.MoveToTargetPosition, targetPosition );
    }
}