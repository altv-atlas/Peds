using AltV.Atlas.Peds.Interfaces;
using AltV.Atlas.Peds.Shared.Interfaces;
using AltV.Atlas.Peds.Shared.PedTasks;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Shared.Elements.Entities;

namespace AltV.Atlas.Peds.PedTasks;

public class PedTaskMoveToTargetPosition( Position targetPosition ) : PedTaskMoveToTargetPositionBase, IPedTask, IWritable
{
    public Guid Id { get; set; } = Guid.Parse( "721ADE18-64A0-4B12-9FF6-09793DDC8C72" );

    private Position TargetPosition { get; set; } = targetPosition;

    public void OnWrite( IMValueWriter writer )
    {
        writer.BeginObject( );

        writer.Name( "pos" );
        writer.BeginObject( );
        writer.Name( "x" );
        writer.Value( TargetPosition.X );
        writer.Name( "y" );
        writer.Value( TargetPosition.Y );
        writer.Name( "z" );
        writer.Value( TargetPosition.Z );
        writer.EndObject( );
        
        writer.EndObject( );
    }
    public void Execute( ISharedPed ped )
    {
        throw new NotImplementedException( );
    }
}