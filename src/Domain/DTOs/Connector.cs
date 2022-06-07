using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class ConnectorDTO
{
    /// <summary>
    /// Max current of the connector
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "MaxCurrent must be greater than {1}")]
    public int MaxCurrent { get; set; }

    /// <summary>
    /// Charge Station Id that the connector belongs to
    /// </summary>
    public long ChargeStationId { get; set; }
}