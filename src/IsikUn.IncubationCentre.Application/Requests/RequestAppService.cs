using System;
using IsikUn.IncubationCentre.Permissions;
using IsikUn.IncubationCentre.Requests.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Requests
{
    public class RequestAppService : CrudAppService<Request, RequestDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateRequestDto, UpdateRequestDto>,
        IRequestAppService
    {
        protected override string GetPolicyName { get; set; } = IncubationCentrePermissions.Request.Default;
        protected override string GetListPolicyName { get; set; } = IncubationCentrePermissions.Request.Default;
        protected override string CreatePolicyName { get; set; } = IncubationCentrePermissions.Request.Create;
        protected override string UpdatePolicyName { get; set; } = IncubationCentrePermissions.Request.Update;
        protected override string DeletePolicyName { get; set; } = IncubationCentrePermissions.Request.Delete;

        private readonly IRequestRepository _repository;

        public RequestAppService(IRequestRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
