using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Bitzen.Veiculos.Api.Controllers;
using Bitzen.Veiculos.Api.Extensions;
using Bitzen.Veiculos.Api.ViewModels;
using Bitzen.Veiculos.Business.Interfaces;
using Bitzen.Veiculos.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bitzen.Veiculos.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/veiculos")]
    public class VeiculosController : MainController
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IVeiculoService _veiculoService;
        private readonly IMapper _mapper;

        public VeiculosController(INotificador notificador,
                                  IVeiculoRepository veiculoRepository,
                                  IVeiculoService veiculoService,
                                  IMapper mapper,
                                  IUser user) : base(notificador, user)
        {
            _veiculoRepository = veiculoRepository;
            _veiculoService = veiculoService;
            _mapper = mapper;
        }

        [ClaimsAuthorize("Veiculo", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<VeiculoViewModel>> Adicionar(VeiculoViewModel veiculoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            Veiculo veiculo = await _veiculoService.Adicionar(_mapper.Map<Veiculo>(veiculoViewModel));

            veiculoViewModel = _mapper.Map<VeiculoViewModel>(veiculo);

            return CustomResponse(veiculoViewModel);
        }

        [ClaimsAuthorize("Veiculo", "Atualizar")]
        [HttpPut]
        public async Task<IActionResult> Atualizar(VeiculoViewModel veiculoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _veiculoService.Atualizar(_mapper.Map<Veiculo>(veiculoViewModel));

            return CustomResponse(veiculoViewModel);
        }

        [ClaimsAuthorize("Veiculo", "Remover")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<VeiculoViewModel>> Remover(Guid id)
        {
            await _veiculoService.Remover(id);

            return CustomResponse(true);
        }

        [ClaimsAuthorize("Veiculo", "Obter")]
        [HttpGet]
        public async Task<IEnumerable<VeiculoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<VeiculoViewModel>>(await _veiculoRepository.Buscar(v => v.Excluido == false &&
                                                                                              v.Status == EStatusVeiculo.Disponivel));
        }

        [ClaimsAuthorize("Veiculo", "Obter")]
        [HttpGet("ObterPorId/{id:guid}")]
        public async Task<ActionResult<VeiculoViewModel>> ObterPorId(Guid id)
        {
            var veiculoViewModel = _mapper.Map<VeiculoViewModel>( await _veiculoRepository.ObterPorId(id));

            if (veiculoViewModel == null) return NotFound();

            return veiculoViewModel;
        }

        [ClaimsAuthorize("Veiculo", "Obter")]
        [HttpGet("ObterPorAno/{ano}")]
        public async Task<IEnumerable<VeiculoViewModel>> ObterPorAno(string ano)
        {
            return _mapper.Map<IEnumerable<VeiculoViewModel>>(await _veiculoRepository.ObterVeiculosPorAno(ano));
        }

        [ClaimsAuthorize("Veiculo", "Obter")]
        [HttpGet("ObterPorCombustivel/{combustivel}")]
        public async Task<IEnumerable<VeiculoViewModel>> ObterPorCombustivel(string combustivel)
        {
            return _mapper.Map<IEnumerable<VeiculoViewModel>>(await _veiculoRepository.ObterVeiculosPorCombustivel(combustivel));
        }

        [ClaimsAuthorize("Veiculo", "Obter")]
        [HttpGet("ObterPorModelo/{modelo}")]
        public async Task<IEnumerable<VeiculoViewModel>> ObterPorModelo(string modelo)
        {
            return _mapper.Map<IEnumerable<VeiculoViewModel>>(await _veiculoRepository.ObterVeiculosPorModelo(modelo));
        }

        [ClaimsAuthorize("Veiculo", "Obter")]
        [HttpGet("ObterPorNome/{nome}")]
        public async Task<IEnumerable<VeiculoViewModel>> ObterPorNome(string nome)
        {
            return _mapper.Map<IEnumerable<VeiculoViewModel>>(await _veiculoRepository.ObterVeiculosPorNome(nome));
        }

        [ClaimsAuthorize("Veiculo", "Obter")]
        [HttpGet("ObterPorPlaca/{placa}")]
        public async Task<IEnumerable<VeiculoViewModel>> ObterPorPlaca(string placa)
        {
            return _mapper.Map<IEnumerable<VeiculoViewModel>>(await _veiculoRepository.ObterVeiculosPorPlaca(placa));
        }

        [ClaimsAuthorize("Veiculo", "Obter")]
        [HttpGet("ObterPorFaixadeValores/{valorInicial}/{valorFinal}")]
        public async Task<IEnumerable<VeiculoViewModel>> ObterPorFaixadeValores(decimal valorInicial, decimal valorFinal)
        {
            return _mapper.Map<IEnumerable<VeiculoViewModel>>(await _veiculoRepository.ObterVeiculosPorValores(valorInicial, valorFinal));
        }

    }
}