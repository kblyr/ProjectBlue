using JIL.Converters;
using Mapster;

namespace JIL.Contracts;

sealed class Mapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<LoggedUserResponse, LoggedUser.Response>()
            .Map(dest => dest.Id, src => MapConverters.UserId.Convert(src.Id));
    }
}
