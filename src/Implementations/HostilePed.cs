using AltV.Atlas.Peds.Base;
using AltV.Atlas.Peds.Enums;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using Microsoft.Extensions.Logging;

namespace AltV.Atlas.Peds.Implementations;

public class HostilePed : AtlasPed
{
    public HostilePed( ILogger<HostilePed> logger, ICore core, IntPtr nativePointer, uint id ) : base( logger, core, nativePointer, id )
    {

    }
}