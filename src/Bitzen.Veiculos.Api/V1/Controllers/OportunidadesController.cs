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
    [Route("api/v{version:apiVersion}/oportunidades")]
    public class OportunidadesController : MainController
    {
        private readonly IOportunidadeRepository _oportunidadeRepository;
        private readonly IOportunidadeService _oportunidadeService;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public OportunidadesController(IOportunidadeRepository oportunidadeRepository,
                                      IMapper mapper,
                                      IOportunidadeService oportunidadeService,
                                      INotificador notificador,
                                      IUser user) : base(notificador, user)
        {
            _oportunidadeRepository = oportunidadeRepository;
            _oportunidadeService = oportunidadeService;
            _mapper = mapper;
            _user = user;
        }

        [ClaimsAuthorize("Oportunidade", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<OportunidadeViewModel>> Adicionar(OportunidadeViewModel oportunidadeViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            Oportunidade oportunidade = await _oportunidadeService.Adicionar(_mapper.Map<Oportunidade>(oportunidadeViewModel));

            oportunidadeViewModel = _mapper.Map<OportunidadeViewModel>(oportunidade);

            return CustomResponse(oportunidadeViewModel);
        }

        [ClaimsAuthorize("Oportunidade", "Atualizar")]
        [HttpPut]
        public async Task<ActionResult<OportunidadeViewModel>> Atualizar(OportunidadeViewModel oportunidadeViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _oportunidadeService.Atualizar(_mapper.Map<Oportunidade>(oportunidadeViewModel));

            return CustomResponse(oportunidadeViewModel);
        }

        [ClaimsAuthorize("Oportunidade", "Remover")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<OportunidadeViewModel>> Remover(Guid id)
        {
            await _oportunidadeService.Remover(id);

            return CustomResponse(true);
        }

        [ClaimsAuthorize("Oportunidade", "Obter")]
        [HttpGet]
        public async Task<IEnumerable<OportunidadeViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<OportunidadeViewModel>>(await _oportunidadeRepository.Buscar(o => o.Excluido == false));
        }

        [ClaimsAuthorize("Oportunidade", "Obter")]
        [HttpGet("ObterPorId/{id:guid}")]
        public async Task<ActionResult<OportunidadeViewModel>> ObterPorId(Guid id)
        {
            var oportunidadeViewModel = _mapper.Map<OportunidadeViewModel>(await _oportunidadeService.ObterPorId(id));

            if (oportunidadeViewModel == null)
                return NotFound();

            return oportunidadeViewModel;
        }
    }
}