namespace DateNight.Core.Entities.UserAggregate;

public class Password
{
    public byte[] Hash { get; set; } = Array.Empty<byte>();
    public byte[] Salt { get; set; } = Array.Empty<byte>();
}