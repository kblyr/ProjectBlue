#nullable disable

using JIL;
using JIL.Accounts.Lookups;
using MassTransit;
using Microsoft.Extensions.Options;

public sealed class UserSaga : Saga<UserSaga.Instance>
{
    readonly UserStatusesLookup _statuses;

    public UserSaga(IOptions<UserStatusesLookup> statuses)
    {
        _statuses = statuses.Value;
    }

    public State Pending { get; private set; }
    public State Active { get; private set; }
    public State Deactivated { get; private set; }
    public State Locked { get; private set; }

    public Event<UserCreatedEvent> UserCreated { get; private set; }

    protected override void ConfigureEvents()
    {
        Event(() => UserCreated,
            corr => corr
                .CorrelateBy((instance, context) => instance.Data.Id == context.Message.Id)
                .SelectId(context => NewId.NextGuid())
        );
    }

    protected override void ConfigureEventActivities()
    {
        Initially(
            Ignore(UserCreated, context => context.Message.StatusId == _statuses.Deactivated),
            Ignore(UserCreated, context => context.Message.StatusId == _statuses.Locked),
            When(UserCreated, context => context.Message.StatusId == _statuses.Pending)
                .Then(OnUserCreated)
                .TransitionTo(Pending),
            When(UserCreated, context => context.Message.StatusId == _statuses.Active)
                .Then(OnUserCreated)
                .TransitionTo(Active)
        );
    }

    void OnUserCreated(BehaviorContext<Instance, UserCreatedEvent> context)
    {
        context.Saga.Data.Id = context.Message.Id;
        context.Saga.Data.FullName = context.Message.FullName;
        context.Saga.Data.Username = context.Message.Username;
        context.Saga.Data.StatusId = context.Message.StatusId;
    }

    public sealed class Instance : SagaInstance<Instance.DataObj>, ISagaInstance
    {
        public sealed class DataObj
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string Username { get; set; }
            public short StatusId { get; set; }
        }
    }
}