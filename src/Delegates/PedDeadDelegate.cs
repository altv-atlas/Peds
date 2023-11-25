using AltV.Atlas.Peds.Interfaces;
using AltV.Net.Elements.Entities;

namespace AltV.Atlas.Peds.Delegates;

/// <summary>
/// Ped dead delegate
/// </summary>
public delegate void PedDeadDelegate( IAtlasServerPed ped, IEntity killer, uint weapon );