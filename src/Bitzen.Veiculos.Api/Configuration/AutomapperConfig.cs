using AutoMapper;
using Bitzen.Veiculos.Api.ViewModels;
using Bitzen.Veiculos.Business.Models;

namespace Bitzen.Veiculos.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Veiculo, VeiculoViewModel>().ReverseMap();
            CreateMap<Oportunidade, OportunidadeViewModel>().ReverseMap();
            CreateMap<OportunidadeLog, OportunidadeLogViewModel>().ReverseMap();
            CreateMap<Cargo, CargoViewModel>().ReverseMap();
            CreateMap<VendedorCargo, VendedorCargoViewModel>().ReverseMap();
        }
    }
}