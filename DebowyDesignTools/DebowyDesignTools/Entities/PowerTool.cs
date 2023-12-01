namespace DebowyDesignTools.Entities;

public class PowerTool : Tool
{
    public string Battery { get; set; }
    
    public override string ToString()
        => base.ToString() + $" ,Battery: {Battery}";
}