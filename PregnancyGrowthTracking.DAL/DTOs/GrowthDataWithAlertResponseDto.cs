public class GrowthDataWithAlertResponseDto
{
    public int GrowthDataId { get; set; }
    public int Age { get; set; }
    public GrowthMeasurementWithAlert HC { get; set; }
    public GrowthMeasurementWithAlert AC { get; set; }
    public GrowthMeasurementWithAlert FL { get; set; }
    public GrowthMeasurementWithAlert EFW { get; set; }
    public string Date { get; set; }
}

public class GrowthMeasurementWithAlert
{
    public double Value { get; set; }
    public bool IsAlert { get; set; }
    public double? MinRange { get; set; }
    public double? MaxRange { get; set; }
} 