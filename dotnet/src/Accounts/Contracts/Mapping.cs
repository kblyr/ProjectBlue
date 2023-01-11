using Mapster;

namespace JIL.Accounts.Contracts;

sealed class Mapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<UserCreatedEvent, UserActivationRequestCreatedEvent>()
            .Map(dest => dest.RequestedBy, src => src.CreatedBy);
    }
}