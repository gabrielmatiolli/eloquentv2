using AutoMapper;
using EloquentBackend.DTOs;
using EloquentBackend.Models;

public class SubscriptionPerkProfile : Profile
{
    public SubscriptionPerkProfile()
    {
        // Mapeamentos para requisição (DTO -> Entidade)
        CreateMap<CreateSubscriptionPerkDto, SubscriptionPerk>();

        // NOVO: Mapeamentos para resposta (Entidade -> DTO)
        CreateMap<SubscriptionPerk, SubscriptionPerkDto>();
    }
}
