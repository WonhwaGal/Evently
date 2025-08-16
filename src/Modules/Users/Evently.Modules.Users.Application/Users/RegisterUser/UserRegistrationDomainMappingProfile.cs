using AutoMapper;
using Evently.Modules.Users.Application.Users.GetUser;
using Evently.Modules.Users.Domain.Users.Events;
using Evently.Modules.Users.IntegrationEvents;

namespace Evently.Modules.Users.Application.Users.RegisterUser;
public sealed class UserRegistrationDomainMappingProfile : Profile
{
    public UserRegistrationDomainMappingProfile()
    {
        CreateMap<(UserRegisteredDomainEvent, UserResponse), UserRegisteredIntegrationEvent>()
            .ForMember(dest=> dest.IntegrationEventId, opt =>opt.MapFrom(src => src.Item1.DomainEventId))
            .ForMember(dest => dest.OccurredOnUtc, opt => opt.MapFrom(src => src.Item1.OccurredOnUtc))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Item2.Id))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Item2.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Item2.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Item2.LastName));
    }
}
