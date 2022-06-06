using AutoMapper;
using Pokemon.Data.Entities;

namespace Pokemon.Data.Mapping;

public class PokemonProfile : Profile
{
    public PokemonProfile()
    {
        CreateMap<Models.Pokemon, Entities.Pokemon>()
            .ForMember(dest => dest.BaseStat,
                options => options.MapFrom(src => new BaseStat()
                {
                    PokemonId = src.Id,
                    Attack = src.Attack,
                    Defense = src.Defense,
                    Speed = src.Speed,
                    HitPoints = src.HitPoints,
                    SpAttack = src.SpecialAttack,
                    SpDefense = src.SpecialDefense
                }))
            .ForMember(dest => dest.Name,
                options => options.MapFrom(src => new PokemonName()
                {
                    English = src.EnglishName, 
                    Chinese = src.ChineseName, 
                    Japanese = src.JapaneseName,
                    PokemonId = src.Id
                }));

        CreateMap<Entities.Pokemon, Models.Pokemon>()
            .ForMember(dest => dest.Attack, options => options.MapFrom(src => src.BaseStat.Attack))
            .ForMember(dest => dest.Defense, options => options.MapFrom(src => src.BaseStat.Defense))
            .ForMember(dest => dest.Speed, options => options.MapFrom(src => src.BaseStat.Speed))
            .ForMember(dest => dest.HitPoints, options => options.MapFrom(src => src.BaseStat.HitPoints))
            .ForMember(dest => dest.SpecialAttack, options => options.MapFrom(src => src.BaseStat.SpAttack))
            .ForMember(dest => dest.SpecialDefense, options => options.MapFrom(src => src.BaseStat.SpDefense))
            .ForMember(dest => dest.EnglishName, options => options.MapFrom(src => src.Name.English))
            .ForMember(dest => dest.JapaneseName, options => options.MapFrom(src => src.Name.Japanese))
            .ForMember(dest => dest.ChineseName, options => options.MapFrom(src => src.Name.Chinese));
    }
}