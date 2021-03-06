namespace Domain.DTOs;

public class ChargeStationDTO
{
    /// <summary>
    /// Charge station Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// The name of station
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Id of the group that the charge station belongs to
    /// </summary>
    public long GroupId { get; set; }

    public List<ConnectorDTO> Connectors { get; set; }
}