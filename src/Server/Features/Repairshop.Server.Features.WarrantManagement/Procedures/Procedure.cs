namespace Repairshop.Server.Features.WarrantManagement.Procedures;

public class Procedure
{
#nullable disable
    private Procedure() { }
#nullable enable

    private Procedure(
        Guid id,
        string name,
        ColorCode color)
    {
        Id = id;
        Color = color;
        Name = name;
    }

    public Guid Id { get; private set; }
    public ColorCode Color { get; private set; }
    public string Name { get; private set; }

    public static Procedure Create(
        string name,
        ColorCode color)
    {
        return new Procedure(Guid.NewGuid(), name, color);
    }

    public void Update(
        string name,
        ColorCode color)
    {
        Name = name;
        Color = color;
    }
}
