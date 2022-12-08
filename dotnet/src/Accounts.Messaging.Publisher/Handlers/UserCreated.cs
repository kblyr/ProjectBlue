namespace JIL.Accounts.Handlers;

sealed class UserCreatedHandler : NotificationBusPublishHandler<UserCreatedEvent>
{
    public UserCreatedHandler(IBusAdapter bus) : base(bus)
    {
    }
}
