using AltV.Atlas.Peds.Shared.Interfaces;

namespace AltV.Atlas.Peds.Delegates;

/// <summary>
/// Ped task change delegate
/// </summary>
public delegate void PedTaskChangeDelegate( IPedTask? oldTask, IPedTask? newTask );