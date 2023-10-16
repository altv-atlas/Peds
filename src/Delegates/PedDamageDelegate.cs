using AltV.Net.Elements.Entities;

namespace AltV.Atlas.Peds.Delegates;

public delegate void PedDamageDelegate( IEntity attacker, uint weapon, ushort healthDamage, ushort armourDamage );