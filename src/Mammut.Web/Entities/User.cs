namespace Mammut.Web.Entities;

public abstract class CommonEntity
{
    public int Id { get; init; }
    public required DateTime CreatedUtc { get; init; }
    public required DateTime UpdatedUtc { get; init; }
}

public class User: CommonEntity
{
    public required string Login { get; set; }
    public required byte[] PasswordDigest { get; set; }
}