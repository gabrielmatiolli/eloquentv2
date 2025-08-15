using AutoMapper;
using EloquentBackend.DTOs;
using EloquentBackend.Models;

public class SubscriptionProfile : Profile
{
    public SubscriptionProfile()
    {
        // Mapeamentos para requisição (DTO -> Entidade)
        CreateMap<CreateSubscriptionDto, Subscription>();
        CreateMap<CreateSubscriptionPerkDto, SubscriptionPerk>();

        // NOVO: Mapeamentos para resposta (Entidade -> DTO)
        CreateMap<Subscription, SubscriptionDto>();
        CreateMap<SubscriptionPerk, SubscriptionPerkDto>();
    }
}
