using System.Numerics;
using AltV.Atlas.Peds.Delegates;
using AltV.Atlas.Peds.Enums;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;

namespace AltV.Atlas.Peds.Interfaces;

public interface IAtlasPed : IPed
{
    void SetPedTask( EPedTask task, IPedTaskData data );
    void SetToWander( Vector3 wanderArea, uint radius, uint minLength, uint timeBetweenWalks );
    void FollowEntity( IEntity entity );
    void MoveToTargetPosition( Position targetPosition );

    event PedDeadDelegate? OnDeath;
    event PedDamageDelegate? OnDamage;
    event PedHealDelegate? OnHeal;
    event PedNetOwnerChangeDelegate? OnNetOwnerChange;
    event PedTaskChangeDelegate? OnTaskChange;
}