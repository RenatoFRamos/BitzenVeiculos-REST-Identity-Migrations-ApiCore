using Bitzen.Veiculos.Business.Interfaces;
using Bitzen.Veiculos.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitzen.Veiculos.Business.Services
{
    public class OportunidadeLogService : BaseService, IOportunidadeLogService
    {
        private readonly IOportunidadeLogRepository _oportunidadeLogRepository;
        private readonly IUser _user;

        public OportunidadeLogService(INotificador notificador,
                              IOportunidadeLogRepository oportunidadeLogRepository,
                              IUser user) : base(notificador)
        {
            _oportunidadeLogRepository = oportunidadeLogRepository;
            _user = user;
        }

        public async Task Adicionar(OportunidadeLog oportunidadeLog)
        { 
            await _oportunidadeLogRepository.Adicionar(oportunidadeLog);
        }

        public async Task Atualizar(OportunidadeLog oportunidadeLog)
        {
            await _oportunidadeLogRepository.Atualizar(oportunidadeLog);
        }

        public async Task Remover(Guid id)
        {
            IEnumerable<OportunidadeLog> oportunidadeLogs = await _oportunidadeLogRepository.Buscar(o => o.Id == id);

            if (oportunidadeLogs.Any())
            {
                OportunidadeLog oportunidadeLog = oportunidadeLogs.FirstOrDefault();
                await _oportunidadeLogRepository.Atualizar(oportunidadeLog);
            }
            else
            {
                Notificar("Id não encontrado para exclusão!");
            }
        }

        public void Dispose()
        {
            _oportunidadeLogRepository.Dispose();
        }
    }
}
