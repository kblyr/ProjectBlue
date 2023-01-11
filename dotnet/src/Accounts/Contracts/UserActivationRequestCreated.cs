namespace JIL.Accounts.Contracts;

public sealed record UserActivationRequestCreatedEvent : MediatR.INotification
{
    public int Id { get; init; }
    public required string FullName { get; init; }
    public required UserObj RequestedBy { get; init; }
    
    public sealed record UserObj
    {
        public int Id { get; init; }
        public required string FullName { get; init; }
    }
}