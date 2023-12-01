namespace DebowyDesignTools.Entities;

public class Tool : EntityBase
{
    public string? Name { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }

    public override string ToString()
        => $"Id: {Id},Name: {Name}, Brand: {Brand}, Model: {Model}";
}