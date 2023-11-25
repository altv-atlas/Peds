using System.Numerics;
using AltV.Atlas.Peds.Interfaces;
using AltV.Atlas.Peds.Shared.Interfaces;
using AltV.Atlas.Peds.Shared.PedTasks;
using AltV.Net;
using AltV.Net.Shared.Elements.Entities;

namespace AltV.Atlas.Peds.PedTasks;

/// <summary>
/// Makes a ped wander in the given area
/// </summary>
/// <param name="position">The area to wander in</param>
/// <param name="radius">The radius from position to wander around in</param>
/// <param name="minLength">Minimum length to walk</param>
/// <param name="timeBetweenWalks">Waiting time between walks</param>
public class PedTaskWander( Vector3 position, uint radius, uint minLength, uint timeBetweenWalks )
    : PedTaskWanderBase( position, radius, minLength, timeBetweenWalks )
{
}