using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class GroupDTO
{
    /// <summary>
    /// Group Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Capacity of group (more than 0)
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than {1}")]
    public int Capacity { get; set; }
}