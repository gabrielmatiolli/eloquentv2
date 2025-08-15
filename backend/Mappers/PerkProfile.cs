using AutoMapper;
using EloquentBackend.DTOs;
using EloquentBackend.Models;

public class PerkProfile : Profile
{
    public PerkProfile()
    {
        // Mapeamentos para requisição (DTO -> Entidade)
        CreateMap<CreatePerkDto, Perk>();
        CreateMap<CreateSubscriptionPerkDto, SubscriptionPerk>();

        // Mapeamentos para resposta (Entidade -> DTO)
        CreateMap<Perk, PerkDto>(); 
        CreateMap<SubscriptionPerk, SubscriptionPerkDto>(); // Esta linha vai funcionar agora
    }
}