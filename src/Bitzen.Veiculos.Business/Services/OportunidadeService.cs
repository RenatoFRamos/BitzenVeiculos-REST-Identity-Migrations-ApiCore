using Bitzen.Veiculos.Business.Interfaces;
using Bitzen.Veiculos.Business.Models;
using Bitzen.Veiculos.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitzen.Veiculos.Business.Services
{
    public class OportunidadeService : BaseService, IOportunidadeService
    {
        private readonly IOportunidadeRepository _oportunidadeRepository;
        private readonly IOportunidadeLogService _oportunidadeLogService;
        private readonly ICargoService _cargoService;
        private readonly IVendedorCargoService _vendedorCargoService;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IUser _user;

        decimal comissaoLimiteSuperior = 18;
        decimal comissaoLimiteInferior = 5;

        public OportunidadeService(INotificador notificador,
            IOportunidadeRepository oportunidadeRepository,
            IOportunidadeLogService oportunidadeLogService,
            ICargoService cargoService,
            IVendedorCargoService vendedorCargoService,
            IVeiculoRepository veiculoRepository,
            IUser user) : base(notificador)
        {
            _oportunidadeRepository = oportunidadeRepository;
            _oportunidadeLogService = oportunidadeLogService;
            _cargoService = cargoService;
            _vendedorCargoService = vendedorCargoService;
            _veiculoRepository = veiculoRepository;
            _user = user;
        }

        public async Task<Oportunidade> Adicionar(Oportunidade oportunidade)
        {
            oportunidade = InserirDadosIniciais(oportunidade);

            if (!ExecutarValidacao(new OportunidadeValidation(), oportunidade))
                return oportunidade;

            if (!RegrasAdicionar(oportunidade))
                return oportunidade;

            oportunidade.Comissao = await ObterComissao();

            await _oportunidadeRepository.Adicionar(oportunidade);

            AdicionaOportunidadeLog(MapearOportunidadeParaOportunidadeLog(oportunidade));

            return oportunidade;
        }

        public async Task<bool> Atualizar(Oportunidade oportunidade)
        {
            if (!ExecutarValidacao(new OportunidadeValidation(), oportunidade))
                return false;

            if (! await RegrasAtualizar(oportunidade))
                return false;

            oportunidade.Comissao = await ObterComissao();

            OportunidadeLog oportunidadeLog = MapearOportunidadeParaOportunidadeLog(oportunidade);

            await _oportunidadeRepository.Atualizar(oportunidade);

            AdicionaOportunidadeLog(oportunidadeLog);

            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            IEnumerable<Oportunidade> oportunidades = await _oportunidadeRepository.Buscar(o => o.Id == id &&
                                                                                           o.Excluido == false);
            Oportunidade oportunidade = new Oportunidade();
            OportunidadeLog oportunidadeLog = new OportunidadeLog();

            if (oportunidades.Any())
            {
                oportunidade = oportunidades.FirstOrDefault();
            }
            else
            {
                Notificar("Id não encontrado para exclusão!");
                return false;
            }

            if (VerificaSeNaoEhMesmoVendedorAutenticado(oportunidade))
            {
                Notificar("Usuário somente tem acesso às oportunidades criadas por ele mesmo!");
                return false;
            }

            if (VerificaSeStatusPossibilitaExclusao(oportunidade))
            {
                await _oportunidadeRepository.Remover(id);
            }
            else
            {
                oportunidade.Excluido = true;
                oportunidade.DataExclusao = DateTime.Now;
                await _oportunidadeRepository.Atualizar(oportunidade);
            }

            oportunidadeLog = MapearOportunidadeParaOportunidadeLog(oportunidade);
            AdicionaOportunidadeLog(oportunidadeLog);

            return true;
        }

        public async Task<IEnumerable<Oportunidade>> ObterTodos()
        {
            return await _oportunidadeRepository.Buscar(o => o.VendedorId == _user.GetUserId() &&
                                                             o.Excluido == false);
        }

        public async Task<Oportunidade> ObterPorId(Guid id)
        {
            IEnumerable<Oportunidade> oportunidades =
               await _oportunidadeRepository.Buscar(o => o.VendedorId == _user.GetUserId() &&
                                                         o.Id == id &&
                                                         o.Excluido == false);

            return oportunidades.FirstOrDefault();
        }

        private async Task<decimal> ObterComissao()
        {
            VendedorCargo vendedorCargo = await _vendedorCargoService.ObterPorVendedorId(_user.GetUserId());

            if (VendedorComCargoAssociado(vendedorCargo))
            {
                if (ComissaoLimiteOK(vendedorCargo))
                {
                    return vendedorCargo.Comissao;
                }
                else
                {
                    Cargo cargo = await _cargoService.ObterPorId(vendedorCargo.CargoId);
                    return cargo.Comissao;
                }
            }
            else
            {
                Notificar("Não existe cargo associado a este vendedor");
            }
            return 0;
        }

        private bool VendedorComCargoAssociado(VendedorCargo vendedorCargo)
        {
            if (vendedorCargo.VendedorId == _user.GetUserId())
                return true;

            return false;
        }

        private bool ComissaoLimiteOK(VendedorCargo vendedorCargo)
        {
            if (vendedorCargo.Comissao >= comissaoLimiteInferior && vendedorCargo.Comissao <= comissaoLimiteSuperior)
                return true;

            return false;
        }

        private bool RegrasAdicionar(Oportunidade oportunidade)
        {
            if (VerificaSeNaoEhMesmoVendedorAutenticado(oportunidade))
            {
                Notificar("Usuário somente tem acesso às oportunidades criadas por ele mesmo!");
                return false;
            }

            if (VerificaSeExisteVeiculoCadastradoDisponivel(oportunidade))
            {
                Notificar("Já existe um veículo com esta placa, com status Disponível.");
                return false;
            }

            return true;
        }

        private async Task<bool> RegrasAtualizar(Oportunidade oportunidade)
        {
            IEnumerable<Oportunidade> oportunidades = await _oportunidadeRepository.Buscar(o => o.Id == oportunidade.Id);

            Oportunidade oportunidadeAtual = oportunidades.FirstOrDefault();

            oportunidade = ManterDadosEssenciais(oportunidade, oportunidadeAtual);

            if (VerificaConsistenciaDaOportunidadeRecebida(oportunidade, oportunidadeAtual))
            {
                Notificar("Inconsistência entre os dados recebidos e o banco de dados");
                return false;
            }

            if (VerificaSeMarcadoComoExcluido(oportunidadeAtual))
            {
                Notificar("Esta Oportunidade já foi excluida!");
                return false;
            }

            if (VerificaSeNaoEhMesmoVendedorAutenticado(oportunidade))
            {
                Notificar("Usuário somente tem acesso às oportunidades criadas por ele mesmo!");
                return false;
            }

            if (VerificaSeExisteVeiculoCadastradoDisponivel(oportunidade))
            {
                Notificar("Não existe um veículo com esta placa, com status Disponível, para atualização.");
                return false;
            }

            if (VerificaSeStatusPossibilitaAtualizacao(oportunidade))
            {
                Notificar("Status altual da Oportunidade não possibilita alteração.");
                return false;
            }

            if (VerificaDataDeValidade(oportunidade))
            {
                Notificar("Oportunidade Expirada.");
                return false;
            }

            oportunidadeAtual = null;

            return true;
        }

        private bool VerificaSeNaoEhMesmoVendedorAutenticado(Oportunidade oportunidade)
        {
            if (oportunidade.VendedorId != _user.GetUserId())
                return true;

            return false;
        }

        private Oportunidade ManterDadosEssenciais(Oportunidade oportunidade, Oportunidade oportunidadeAtual)
        {
            oportunidade.DataExpiracao = oportunidadeAtual.DataExpiracao;
            oportunidade.Comissao = oportunidadeAtual.Comissao;
            oportunidade.DataCadastro = oportunidadeAtual.DataCadastro;

            return oportunidade;
        }

        private Oportunidade InserirDadosIniciais(Oportunidade oportunidade)
        {
            oportunidade.VendedorId = _user.GetUserId();
            oportunidade.Status = EStatusOportunidade.Criada ;
            oportunidade.Excluido = false;

            if (oportunidade.Id.Equals(Guid.Empty))
                oportunidade.Id = Guid.NewGuid();

            return oportunidade;
        }

        private bool VerificaConsistenciaDaOportunidadeRecebida(Oportunidade oportunidade, Oportunidade oportunidadeAtual)
        {
            if (oportunidade.Id != oportunidadeAtual.Id)
                return true;

            if (oportunidade.VeiculoId != oportunidadeAtual.VeiculoId)
                return true;

            return false;
        }

        private bool VerificaSeExisteVeiculoCadastradoDisponivel(Oportunidade oportunidade)
        {
            if (_veiculoRepository.Buscar(v =>
                (v.Id == oportunidade.VeiculoId &&
                (v.Status == EStatusVeiculo.Disponivel))).Result.Any())
                return false;

            return true;
        }

        private bool VerificaSeStatusPossibilitaAtualizacao(Oportunidade oportunidade)
        {
            if (oportunidade.Status == EStatusOportunidade.Criada)
                return true;

            return false;
        }

        private bool VerificaSeStatusPossibilitaExclusao(Oportunidade oportunidade)
        {
            if (oportunidade.Status != EStatusOportunidade.Aceita)
                return true;

            return false;
        }

        private bool VerificaDataDeValidade(Oportunidade oportunidade)
        {
            if (oportunidade.DataExpiracao <= DateTime.Now)
                return true;

            return false;
        }

        private bool VerificaSeMarcadoComoExcluido(Oportunidade oportunidade)
        {
            if (oportunidade.Excluido)
                return true;

            return false;
        }
        private void AdicionaOportunidadeLog(OportunidadeLog oportunidadeLog)
        {
            _oportunidadeLogService.Adicionar(oportunidadeLog);
        }

        private OportunidadeLog MapearOportunidadeParaOportunidadeLog(Oportunidade oportunidade)
        {
            OportunidadeLog oportunidadeLog = new OportunidadeLog()
            {
                Id = Guid.NewGuid(),
                OportunidadeId = oportunidade.Id,
                VeiculoId = oportunidade.VeiculoId,
                VendedorId = oportunidade.VendedorId,
                Valor = oportunidade.Valor,
                Comissao = oportunidade.Comissao,
                Status = oportunidade.Status,
                Excluido = oportunidade.Excluido,
                DataEvento = DateTime.Now
            };
            oportunidade = null;
            return oportunidadeLog;
        }

        public void Dispose()
        {
            _oportunidadeRepository.Dispose();
            _veiculoRepository.Dispose();
        }
    }
}
