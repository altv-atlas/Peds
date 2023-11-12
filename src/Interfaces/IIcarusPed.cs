using System.Numerics;
using AltV.Icarus.Peds.Delegates;
using AltV.Icarus.Peds.Enums;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;

namespace AltV.Icarus.Peds.Interfaces;

public interface IIcarusPed : IPed
{
    void SetPedTask( EPedTask task, params object[ ] parameters );
    void SetToWander( Vector3 wanderArea, uint radius, uint minLength, uint timeBetweenWalks );
    void FollowEntity( IEntity entity );
    void MoveToTargetPosition( Position targetPosition );

    event PedDeadDelegate? OnDeath;
    event PedDamageDelegate? OnDamage;
    event PedHealDelegate? OnHeal;
    event PedNetOwnerChangeDelegate? OnNetOwnerChange;
}