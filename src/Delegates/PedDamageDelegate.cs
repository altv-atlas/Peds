using AltV.Net.Elements.Entities;

namespace AltV.Atlas.Peds.Delegates;

/// <summary>
/// PedDamage delegate
/// </summary>
public delegate void PedDamageDelegate( IEntity attacker, uint weapon, ushort healthDamage, ushort armourDamage );