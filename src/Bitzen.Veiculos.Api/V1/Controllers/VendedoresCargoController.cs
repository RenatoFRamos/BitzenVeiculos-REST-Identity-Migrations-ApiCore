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
    [Route("api/v{version:apiVersion}/vendedorescargo")]
    public class VendedoresCargoController : MainController
    {
        private readonly IVendedorCargoRepository _vendedorCargoRepository;
        private readonly IVendedorCargoService _vendedorCargoService;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public VendedoresCargoController(IVendedorCargoRepository vendedorCargoRepository,
                                       IVendedorCargoService vendedorCargoService,
                                       IMapper mapper,
                                       INotificador notificador,
                                       IUser user) : base(notificador, user)
        {
            _vendedorCargoRepository = vendedorCargoRepository;
            _vendedorCargoService = vendedorCargoService;
            _mapper = mapper;
            _user = user;
        }

        [ClaimsAuthorize("Admin", "Admin")]
        [HttpPost]
        public async Task<ActionResult<VendedorCargoViewModel>> Adicionar(VendedorCargoViewModel vendedorCargoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            VendedorCargo vendedorCargo = await _vendedorCargoService.Adicionar(_mapper.Map<VendedorCargo>(vendedorCargoViewModel));

            vendedorCargoViewModel = _mapper.Map<VendedorCargoViewModel>(vendedorCargo);

            return CustomResponse(vendedorCargoViewModel);
        }

        [ClaimsAuthorize("Admin", "Admin")]
        [HttpPut]
        public async Task<ActionResult<VendedorCargoViewModel>> Atualizar(VendedorCargoViewModel vendedorCargoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _vendedorCargoService.Atualizar(_mapper.Map<VendedorCargo>(vendedorCargoViewModel));

            return CustomResponse(vendedorCargoViewModel);
        }

        [ClaimsAuthorize("Admin", "Admin")]
        [HttpGet]
        public async Task<IEnumerable<VendedorCargoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<VendedorCargoViewModel>>(await _vendedorCargoRepository.ObterTodos());
        }

        [ClaimsAuthorize("Admin", "Admin")]
        [HttpGet("ObterPorId/{id:guid}")]
        public async Task<ActionResult<VendedorCargoViewModel>> ObterPorId(Guid id)
        {
            var vendedorCargoViewModel = _mapper.Map<VendedorCargoViewModel>(await _vendedorCargoRepository.ObterPorId(id));

            if (vendedorCargoViewModel == null) return NotFound();

            return vendedorCargoViewModel;
        }
    }
}