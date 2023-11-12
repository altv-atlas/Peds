using AltV.Net.Elements.Entities;

namespace AltV.Icarus.Peds.Delegates;

public delegate void PedDamageDelegate( IEntity attacker, uint weapon, ushort healthDamage, ushort armourDamage );