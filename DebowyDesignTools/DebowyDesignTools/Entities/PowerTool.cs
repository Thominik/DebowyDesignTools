namespace DebowyDesignTools.Entities;

public class PowerTool : Tool
{
    public string Battery { get; set; }

    public override string ToString()
    {
        var batteryInfo = String.IsNullOrEmpty(Battery) ? "" : $" ,Battery: {Battery}";
        return base.ToString() + batteryInfo;
    }
}