using ArriendosApi.DTOs;
using ArriendosApi.Entities;
using AutoMapper;

namespace ArriendosApi.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<Edificio, EdificioReadDto>();
            CreateMap<EdificioCreateDto, Edificio>();

            CreateMap<Inquilino, InquilinoReadDto>();
            CreateMap<InquilinoCreateDto, Inquilino>();

            CreateMap<Inmueble, InmuebleReadDto>()
    .ForMember(dest => dest.NombreEdificio, opt => opt.MapFrom(src => src.Edificio!.Nombre));
            CreateMap<InmuebleCreateDto, Inmueble>();



            CreateMap<Contrato, ContratoReadDto>().
                ForMember(des => des.NombreInquilino, opt => opt.MapFrom(src => src.Inquilino != null ? src.Inquilino.Nombres : string.Empty)).
                ForMember(des => des.NumeroDepa, opt => opt.MapFrom(src => src.Inmueble != null ? src.Inmueble.NumeroDepa : string.Empty)).
                ForMember(des => des.NombreEdificio, opt => opt.MapFrom(src => (src.Inmueble != null && src.Inmueble.Edificio != null) ? src.Inmueble.Edificio.Nombre : string.Empty)).
                ForMember(des => des.IdentificacionInquilino, opt => opt.MapFrom(src => src.Inquilino != null ? src.Inquilino.Identificacion : string.Empty));
            CreateMap<ContratoCreateDto, Contrato>();



            CreateMap<CobroMensual,CobroMensualReadDto>().ForMember(des=>des.NombreInquilino, opt=>opt.MapFrom(c=>(c.Contrato!=null && c.Contrato.Inquilino!=null) ? c.Contrato.Inquilino.Nombres : string.Empty)).
                ForMember(des=>des.NumeroDepa,opt=>opt.MapFrom(src=>(src.Contrato!=null && src.Contrato.Inmueble!=null)?src.Contrato.Inmueble.NumeroDepa:string.Empty));
            CreateMap<CobroMensualCreateDto,CobroMensual>();




                


        }
    }
}
