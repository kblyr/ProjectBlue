#nullable disable

using JIL;
using MassTransit;

public sealed class UserSaga : Saga<UserSaga.Instance>
{
    public State Created { get; private set; }

    public Event<UserCreatedEvent> UserCreated { get; private set; }

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