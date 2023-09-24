using System.Numerics;
using AltV.Icarus.Peds.Interfaces;
using AltV.Net;
using AltV.Net.Data;

namespace AltV.Icarus.Peds.PedTasks;

public class PedTaskDataMoveToTargetPosition : IPedTaskData
{
    private Position TargetPosition { get; set; }

    public PedTaskDataMoveToTargetPosition( Position targetPosition )
    {
        TargetPosition = targetPosition;
    }

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
}