using AutoMapper;
using TimeloggerCore.Data.Entities;
using TimeloggerCore.Data.IRepository;
using TimeloggerCore.Services.IService;
using System;
using System.Collections.Generic;
using System.Text;
using TimeloggerCore.Common.Models;
using static TimeloggerCore.Common.Utility.Enums;
using System.Threading.Tasks;

namespace TimeloggerCore.Services
{
    public class ClientWorkerService : BaseService<ClientWorkerModel, ClientWorker, int>, IClientWorkerService
    {
        private readonly IClientWorkerRepository _clientWorkerRepository;

        public ClientWorkerService(
            IMapper mapper, 
            IClientWorkerRepository clientWorkerRepository, 
            IUnitOfWork unitOfWork
            ) : base(mapper, clientWorkerRepository, unitOfWork)
        {
            this._clientWorkerRepository = clientWorkerRepository;
        }
        public async Task<BaseModel> GetProjectInvitation(string workerId, WorkerType workerType)
        {
            var result = await _clientWorkerRepository.GetProjectInvitation(workerId, workerType);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }

        public async Task<BaseModel> GetUserProjecInviationtList(string Id, WorkerType invitationType)
        {
            var result = await _clientWorkerRepository.GetUserProjecInviationtList(Id, invitationType);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
        public async Task<BaseModel> AlreadyInvitationExit(int ProjectId, int ProjectInvitationId, string userId, WorkerType workerType)
        {
            var result = await _clientWorkerRepository.AlreadyInvitationExit(ProjectId,ProjectInvitationId,userId,workerType);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<ClientWorker, ClientWorkerModel>(result)
            };
        }
        public async Task<BaseModel> GetAllProjectWorker(string ProjectId, string clientId)
        {
            var result = await _clientWorkerRepository.GetAllProjectWorker(ProjectId, clientId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
        public async Task<BaseModel> GetAllAgencyProjectWorker(ClientInviteModel clientInviteViewModel)
        {
            var result = await _clientWorkerRepository.GetAllAgencyProjectWorker(clientInviteViewModel);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
        public async Task<BaseModel> DeleteAgencyProjectWorker(DeleteClientWorker deleteClientWorker)
        {
            var result = await _clientWorkerRepository.DeleteAgencyProjectWorker(deleteClientWorker);
            string message = "Not Exist.";
            if (result != null)
            {
                result.IsDeleted = true;
                result.Status = MemberStatus.Inactive;
                await Update(mapper.Map<ClientWorker, ClientWorkerModel>(result));
                return new BaseModel
                {
                    Success = true,
                    Data = mapper.Map<ClientWorker, ClientWorkerModel>(result),
                    Message = "Successful"
                };
            }
            return new BaseModel
            {
                Success = false,
                Data = mapper.Map<ClientWorker, ClientWorkerModel>(result),
                Message = message
            };
        }
        public async Task<BaseModel> GetProjectInvitation(string userId)
        {
            var result = await _clientWorkerRepository.GetProjectInvitation(userId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
        public async Task<BaseModel> GetClientInvitation(string userId, WorkerType workerType)
        {
            var result = await _clientWorkerRepository.GetClientInvitation(userId, workerType);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
        public async Task<BaseModel> GetAgencyInvitation(string AgencyId, WorkerType workerType)
        {
            var result = await _clientWorkerRepository.GetAgencyInvitation(AgencyId, workerType);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
        public async Task<BaseModel> GetWorkerInvitation(string AgencyId, int ProjectId, InvitationType invitationType)
        {
            var result = await _clientWorkerRepository.GetWorkerInvitation(AgencyId, ProjectId, invitationType);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
        public async Task<BaseModel> GetWorkerClientById(int Id)
        {
            var result = await _clientWorkerRepository.GetWorkerClientById(Id);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<ClientWorker, ClientWorkerModel>(result)
            };
        }
        public async Task<BaseModel> GetClientAgencyWorker(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var result = await _clientWorkerRepository.GetClientAgencyWorker(clientAgencyWorkerViewModel);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
        public async Task<BaseModel> GetClientAgencyWorkerInvitation(ClientAgencyWorkerModel clientAgencyWorkerModel)
        {
            var result = await _clientWorkerRepository.GetClientAgencyWorkerInvitation(clientAgencyWorkerModel);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
        public async Task<BaseModel> GetClientProjects(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var result = await _clientWorkerRepository.GetClientProjects(clientAgencyWorkerViewModel);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
        public async Task<BaseModel> GetClientAgencyWorkerProject(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var result = await _clientWorkerRepository.GetClientAgencyWorkerProject(clientAgencyWorkerViewModel);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
        public async Task<BaseModel> GetClientIndividualWorker(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var result = await _clientWorkerRepository.GetClientIndividualWorker(clientAgencyWorkerViewModel);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
        public async Task<BaseModel> GetClientWorkerInvitation(ClientAgencyWorkerModel clientAgencyWorkerViewModel)
        {
            var result = await _clientWorkerRepository.GetClientWorkerInvitation(clientAgencyWorkerViewModel);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
        public async Task<BaseModel> GetAllIndividualWorker(string ProjectId, string clientId)
        {
            var result = await _clientWorkerRepository.GetAllIndividualWorker(ProjectId, clientId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
        public async Task<BaseModel> AlreadySendInvitation(string userId, int invationId, int projectId, string AgencyId)
        {
            var result = await _clientWorkerRepository.AlreadySendInvitation(userId, invationId, projectId, AgencyId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<ClientWorker, ClientWorkerModel>(result)
            };
        }
        public async Task<BaseModel> GetClientWorker(int Id)
        {
            var result = await _clientWorkerRepository.GetClientWorker(Id);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<ClientWorker, ClientWorkerModel>(result)
            };
        }
        public async Task<BaseModel> GetAllInvitation(string SenderId)
        {
            var result = await _clientWorkerRepository.GetAllInvitation(SenderId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
        public async Task<BaseModel> GetAllClientWorker(string ClientId)
        {
            var result = await _clientWorkerRepository.GetAllClientWorker(ClientId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
        public async Task<BaseModel> GetClientActiveProjects(string userId)
        {
            var result = await _clientWorkerRepository.GetClientActiveProjects(userId);
            return new BaseModel
            {
                Success = true,
                Data = mapper.Map<List<ClientWorker>, List<ClientWorkerModel>>(result)
            };
        }
    }
}
