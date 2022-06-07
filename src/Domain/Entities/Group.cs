using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Group
{
    /// <summary>
    /// Unique Identifier
    /// </summary>
    [Key]
    public long Id { get; set; }

    /// <summary>
    /// Group Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Capacity of group (more than 0)
    /// </summary>

    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than {1}")]
    public int Capacity { get; set; }

    /// <summary>
    /// Charge stations in group
    /// </summary>
    [JsonIgnore]
    public Collection<ChargeStation> ChargeStations { get; set; } = new();
}