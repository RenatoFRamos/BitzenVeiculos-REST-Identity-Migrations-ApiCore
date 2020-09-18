using AutoMapper;
using Bitzen.Veiculos.Api.Controllers;
using Bitzen.Veiculos.Api.Extensions;
using Bitzen.Veiculos.Api.ViewModels;
using Bitzen.Veiculos.Business.Interfaces;
using Bitzen.Veiculos.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bitzen.Veiculos.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/cargos")]
    public class CargosController : MainController
    {
        private readonly ICargoRepository _cargoRepository;
        private readonly ICargoService _cargoService;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public CargosController(ICargoRepository cargoRepository,
                                    ICargoService cargoService,
                                    IMapper mapper,
                                    INotificador notificador,
                                    IUser user) : base(notificador, user)
        {
            _cargoRepository = cargoRepository;
            _cargoService = cargoService;
            _mapper = mapper;
            _user = user;
        }

        [ClaimsAuthorize("Admin", "Admin")]
        [HttpPost]
        public async Task<ActionResult<CargoViewModel>> Adicionar(CargoViewModel cargoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            Cargo cargo = await _cargoService.Adicionar(_mapper.Map<Cargo>(cargoViewModel));

            cargoViewModel = _mapper.Map<CargoViewModel>(cargo);

            return CustomResponse(cargoViewModel);
        }

        [ClaimsAuthorize("Admin", "Admin")]
        [HttpPut]
        public async Task<ActionResult<CargoViewModel>> Atualizar(CargoViewModel cargoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _cargoService.Atualizar(_mapper.Map<Cargo>(cargoViewModel));

            return CustomResponse(cargoViewModel);
        }

        [ClaimsAuthorize("Admin", "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CargoViewModel>> Remover(Guid id)
        {
            await _cargoService.Remover(id);

            return CustomResponse(true);
        }

        [ClaimsAuthorize("Admin", "Admin")]
        [HttpGet]
        public async Task<IEnumerable<CargoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<CargoViewModel>>(await _cargoRepository.ObterTodos());
        }

        [ClaimsAuthorize("Admin", "Admin")]
        [HttpGet("ObterPorId/{id:guid}")]
        public async Task<ActionResult<CargoViewModel>> ObterPorId(Guid id)
        {
            var cargoViewModel = _mapper.Map<CargoViewModel>(await _cargoRepository.ObterPorId(id));

            if (cargoViewModel == null) return NotFound();

            return cargoViewModel;
        }
    }
}