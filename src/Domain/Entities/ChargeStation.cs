using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ChargeStation
{
    /// <summary>
    /// Charge Station Id
    /// </summary>
    [Key]
    public long Id { get; set; }

    /// <summary>
    /// The name of station
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Id of the group that the charge station belongs to
    /// </summary>
    public long GroupId { get; set; }

    /// <summary>
    /// Group that charge station belongs to
    /// </summary>
    public virtual Group Group { get; set; }

    /// <summary>
    /// Connectors list of the charge station
    /// </summary>
    public Collection<Connector> Connectors { get; set; } = new();
}