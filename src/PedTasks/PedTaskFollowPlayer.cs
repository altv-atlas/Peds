using AltV.Atlas.Peds.Shared.Interfaces;
using AltV.Atlas.Peds.Shared.PedTasks;
using AltV.Net;
using AltV.Net.Shared.Elements.Entities;

namespace AltV.Atlas.Peds.PedTasks;

/// <summary>
/// Task to make the ped follow a player
/// </summary>
/// <param name="targetId">The remote ID of the player</param>
public class PedTaskFollowPlayer( uint targetId ) : PedTaskFollowPlayerBase( targetId ), IPedTask, IWritable
{
    /// <summary>
    /// Convert type to altV serializable entity
    /// </summary>
    /// <param name="writer"></param>
    public void OnWrite( IMValueWriter writer )
    {
        writer.BeginObject( );

        writer.Name( "targetId" );
        writer.Value( TargetId );
        
        writer.EndObject( );
    }
    public void Execute( ISharedPed ped )
    {
        throw new NotImplementedException( );
    }
}