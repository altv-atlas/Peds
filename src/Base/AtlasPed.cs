using System.Numerics;
using System.Text.Json;
using AltV.Atlas.Peds.Delegates;
using AltV.Atlas.Peds.Interfaces;
using AltV.Atlas.Peds.PedTasks;
using AltV.Atlas.Peds.Shared;
using AltV.Atlas.Peds.Shared.Interfaces;
using AltV.Atlas.Peds.Shared.PedTasks;
using AltV.Atlas.Shared;
using AltV.Atlas.Shared.Converters;
using AltV.Net;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Microsoft.Extensions.Logging;

namespace AltV.Atlas.Peds.Base;

/// <summary>
/// Base Atlas peds class, can be inherited from to extend functionality
/// </summary>
public class AtlasPed : AsyncPed, IAtlasServerPed
{
    private readonly ILogger<AtlasPed> _logger;
    private readonly JsonTypeConverter<IPedTask> _pedTaskJsonConverter = new();
    private IPedTask? _currentTask;
    
    /// <summary>
    /// The current ped task
    /// </summary>
    public IPedTask? CurrentTask {
        get => _currentTask;
        private set
        {
            _currentTask = value;
            
            if( _currentTask is null )
                DeleteStreamSyncedMetaData( PedConstants.CurrentTaskMetaKey );
            else
            {
                var json = JsonSerializer.Serialize( _currentTask, JsonOptions.WithConverters( _pedTaskJsonConverter ) );
                _logger.LogTrace( "converted to json: {Json}", json );
                SetStreamSyncedMetaData( PedConstants.CurrentTaskMetaKey, json );
            }
        }
    }
    
    /// <summary>
    /// Triggered when the ped dies
    /// </summary>
    public event PedDeadDelegate? OnDeath;
    
    /// <summary>
    /// Triggered when the ped takes damage
    /// </summary>
    public event PedDamageDelegate? OnDamage;
    
    /// <summary>
    /// Triggered when the ped has gained health
    /// </summary>
    public event PedHealDelegate? OnHeal;
    
    /// <summary>
    /// Triggered when the ped netowner has changed
    /// </summary>
    public event PedNetOwnerChangeDelegate? OnNetOwnerChange;
    
    /// <summary>
    /// Triggered when the ped atlas task changed
    /// </summary>
    public event PedTaskChangeDelegate? OnTaskChange;

    /// <summary>
    /// Creates a new atlas ped
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="core">Alt Core</param>
    /// <param name="nativePointer">Alt nativeptr</param>
    /// <param name="id">ID of the ped</param>
    public AtlasPed( ILogger<AtlasPed> logger, ICore core, IntPtr nativePointer, uint id ) : base( core, nativePointer, id )
    {
        _logger = logger;
        Alt.OnNetworkOwnerChange += OnNetworkOwnerChange;
        Alt.OnPedDead += OnPedDead;
        Alt.OnPedDamage += OnPedDamage;
        Alt.OnPedHeal += OnPedHeal;
    }

    /// <summary>
    /// Destructor
    /// </summary>
    ~AtlasPed( )
    {
        CurrentTask?.OnStop( this );
    }

    private void OnPedHeal( IPed ped, ushort oldHealth, ushort newHealth, ushort oldArmour, ushort newArmour )
    {
        if( !ped.Equals( this ) )
            return;
        
        OnHeal?.Invoke( oldHealth, newHealth, oldArmour, newArmour );
    }

    private void OnPedDamage( IPed ped, IEntity attacker, uint weapon, ushort healthDamage, ushort armourDamage )
    {
        if( !ped.Equals( this ) )
            return;
        
        OnDamage?.Invoke( attacker, weapon, healthDamage, armourDamage );
    }

    private void OnPedDead( IPed ped, IEntity killer, uint weapon )
    {
        if( !ped.Equals( this ) )
            return;
        
        CurrentTask?.OnStop( this );
        OnDeath?.Invoke( this, killer, weapon );
    }

    private void OnNetworkOwnerChange( IEntity target, IPlayer? oldNetOwner, IPlayer? newNetOwner )
    {
        if( !target.Equals( this ) )
            return;
        
        OnNetOwnerChange?.Invoke( oldNetOwner, newNetOwner );
    }
    
    /// <summary>
    /// Give the ped a specific task
    /// </summary>
    /// <param name="pedTask">pedTask for the task</param>
    public void SetPedTask<T>( T pedTask ) where T : class, IPedTask
    {
        _logger.LogTrace( "SetPedTask {PedTask}", typeof(T) );
        OnTaskChange?.Invoke( CurrentTask, pedTask );
        CurrentTask = pedTask;
    }
}