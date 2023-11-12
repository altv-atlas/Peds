using System.Numerics;
using AltV.Atlas.Peds.Interfaces;
using AltV.Net;

namespace AltV.Atlas.Peds.PedTasks;

internal class PedTaskDataWander : IPedTaskData
{
    private Vector3 Position { get; set; }
    private uint Radius { get; set; }
    private uint MinLength { get; set; }
    private uint TimeBetweenWalks { get; set; }

    internal PedTaskDataWander( Vector3 position, uint radius, uint minLength, uint timeBetweenWalks )
    {
        Position = position;
        Radius = radius;
        MinLength = minLength;
        TimeBetweenWalks = timeBetweenWalks;
    }
    
    public void OnWrite( IMValueWriter writer )
    {
        writer.BeginObject(  );
        
        writer.Name( "pos" );
        writer.BeginObject( );
        writer.Name( "x" );
        writer.Value( Position.X );
        writer.Name( "y" );
        writer.Value( Position.Y );
        writer.Name( "z" );
        writer.Value( Position.Z );
        writer.EndObject( );
        
        writer.Name( "radius" );
        writer.Value( Radius );
        writer.Name( "minLength" );
        writer.Value( MinLength );
        writer.Name( "timeBetweenWalks" );
        writer.Value( TimeBetweenWalks );
        
        writer.EndObject( );
    }
}