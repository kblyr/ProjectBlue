#nullable disable
using MassTransit;
using MassTransit.Testing;

namespace JIL;

public abstract class Saga<TInstance> : MassTransitStateMachine<TInstance>
    where TInstance : class, ISagaInstance
{
    public Saga()
    {
        InstanceState(instance => instance.CurrentState);
        ConfigureEvents();
        ConfigureEventActivities();
    }

    protected virtual void ConfigureEvents() { }

    protected virtual void ConfigureEventActivities() { }
}

public interface ISagaInstance : SagaStateMachineInstance, ISagaVersion
{
    string CurrentState { get; set; }
}

public abstract class SagaInstance<TData> where TData : class, new()
{
    public Guid CorrelationId { get; set; }
    public int Version { get; set; }
    public string CurrentState { get; set; }
    public TData Data { get; private set; } = new();
}