using AltV.Icarus.Peds.Base;
using AltV.Icarus.Peds.Enums;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using Microsoft.Extensions.Logging;

namespace AltV.Icarus.Peds.Implementations;

public class HostilePed : IcarusPed
{
    public HostilePed( ILogger<HostilePed> logger, ICore core, IntPtr nativePointer, uint id ) : base( logger, core, nativePointer, id )
    {

    }
}