namespace AltV.Atlas.Peds.Enums;

/// <summary>
/// A few built-in tasks for peds
/// </summary>
public enum EPedTask
{
    /// <summary>
    /// Idle, ped stays at location and does nothing. Default value
    /// </summary>
    Idle,
    /// <summary>
    /// Wander around in a given area
    /// </summary>
    Wander,
    /// <summary>
    /// Follow target entity
    /// </summary>
    FollowTargetEntity,
    /// <summary>
    /// Move (walk) to target position
    /// </summary>
    MoveToTargetPosition
}