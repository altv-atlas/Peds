using AltV.Net.Elements.Entities;

namespace AltV.Atlas.Peds.Delegates;

/// <summary>
/// Ped netowner change delegate
/// </summary>
public delegate void PedNetOwnerChangeDelegate( IPlayer? oldNetOwner, IPlayer? newNetOwner );