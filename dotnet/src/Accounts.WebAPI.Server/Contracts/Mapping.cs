using JIL.Accounts.Converters;
using Mapster;

namespace JIL.Accounts.Contracts;

sealed class Mapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateUserCommand.Response, CreateUser.Response>()
            .Map(dest => dest.Id, src => MapConverters.UserId.Convert(src.Id));
    }
}
