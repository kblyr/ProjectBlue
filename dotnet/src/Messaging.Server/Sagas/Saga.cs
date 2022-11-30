#nullable disable
using MassTransit;
using MassTransit.Testing;

namespace JIL;

public abstract class Saga<TInstance> : MassTransitStateMachine<TInstance>
    where TInstance : class, ISagaInstance
{

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