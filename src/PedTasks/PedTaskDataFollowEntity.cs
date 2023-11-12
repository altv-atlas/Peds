using AltV.Atlas.Peds.Interfaces;
using AltV.Net;
using AltV.Net.Elements.Entities;

namespace AltV.Atlas.Peds.PedTasks;

internal class PedTaskDataFollowEntity : IPedTaskData
{
    private IEntity? Target { get; set; }

    internal PedTaskDataFollowEntity( IEntity? target )
    {
        Target = target;
    }

    public void OnWrite( IMValueWriter writer )
    {
        writer.BeginObject( );

        writer.Name( "targetId" );
        if( Target != null )
            writer.Value( Target.Id );
        else 
            writer.Value( -1 );
        
        writer.EndObject( );
    }
}