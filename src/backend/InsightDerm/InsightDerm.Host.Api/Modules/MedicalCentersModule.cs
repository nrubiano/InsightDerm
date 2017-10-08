using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InsightDerm.Core.Dto;
using InsightDerm.Core.Service;
using Microsoft.Extensions.Options;
using Nancy;


namespace InsightDerm.Host.Api.Modules
{
    public class MedicalCentersModule : BaseModule
    {
        readonly MedicalCenterService _medicalCenterService;

        public MedicalCentersModule(IOptions<ApiSettings> apiSettings, IMapper mapper, MedicalCenterService medicalCenterService) : base(apiSettings, mapper)
        {
            _medicalCenterService = medicalCenterService;

            Get(GetPath(), (args, ctk) => Get(args, ctk));

            Post(GetPath(), (args, ctk) => Post(args, ctk));

            Put($@"{GetPath()}/{{Id}}", (args, ctk) => Put(args, ctk));

            Delete($@"{GetPath()}/{{Id}}", (args, ctk) => Delete(args, ctk));

        }

        protected virtual async Task<dynamic> Get(dynamic args, CancellationToken ct)
        {
            var entities = _medicalCenterService.GetAll(x => x.Name != string.Empty);

            return Response.AsJson(entities);
        }

		protected virtual async Task<dynamic> Post(dynamic args, CancellationToken ct)
		{
            var model = BindBody<MedicalCenterDto>();

            var exist = _medicalCenterService.Exist(x => string.Equals(x.Name, model.Name, StringComparison.OrdinalIgnoreCase));

            if (exist)
            {
				return Negotiate
						.WithModel($"The medical center with the name {model.Name} already exist")
							.WithStatusCode(HttpStatusCode.BadRequest);
            }
                
            _medicalCenterService.Create(model);

            return Response.AsJson(model);
		}

		protected virtual async Task<dynamic> Put(dynamic args, CancellationToken ct)
		{
            var id = (Guid)args.Id;

            var model = _medicalCenterService.GetSingle(x => x.Id == id);

            if (model == null)
                return HttpStatusCode.NotFound;

            model = UpdateFromBody(model);

            _medicalCenterService.Update(model);

			return Response.AsJson(model);
		}

		protected virtual async Task<dynamic> Delete(dynamic args, CancellationToken ct)
		{
			var id = (Guid)args.Id;

            var model = _medicalCenterService.GetSingle(x => x.Id == id);

			if (model == null)
				return HttpStatusCode.NotFound;
            
			_medicalCenterService.Remove(model);

			return Response.AsJson(model);
		}
    }
}
