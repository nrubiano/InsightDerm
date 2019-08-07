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
    public class ConsultationsModule : BaseModule
    {
	    private readonly ConsultationService _consultationService;

        public ConsultationsModule(IOptions<ApiSettings> apiSettings, IMapper mapper, ConsultationService consultationService) : base(apiSettings, mapper)
        {
            _consultationService = consultationService;

            Get(GetPath(), (args, ctk) => Get(args, ctk));

            Get($@"{GetPath()}/{{Id}}", (args, ctk) => GetSingle(args, ctk));

            Post(GetPath(), (args, ctk) => Post(args, ctk));

            Put($@"{GetPath()}/{{Id}}", (args, ctk) => Put(args, ctk));

            Delete($@"{GetPath()}/{{Id}}", (args, ctk) => Delete(args, ctk));

        }

	    protected virtual async Task<dynamic> Get(dynamic args, CancellationToken ct)
	    {
		    string filter = Request.Query["$filter"];
		    
		    var entities = _consultationService.GetAll(filter, "");

		    return Response.AsJson(entities);
	    }

        protected virtual async Task<dynamic> GetSingle(dynamic args, CancellationToken ct)
        {
            var id = (Guid)args.Id;

            var entities = _consultationService.GetSingle(x => x.Id == id);

            return Response.AsJson(entities);
        }

        protected virtual async Task<dynamic> Post(dynamic args, CancellationToken ct)
		{
			var model = BindBody<ConsultationDto>();

			model.CreationDate = DateTime.Now;
			model.RequestedById = Guid.Parse("7ed9364b-418b-481b-b8a7-c7f5bb3b5f7b");
			
			model = _consultationService.Create(model);
  
			return Response.AsJson(model);
		}
	
		protected virtual async Task<dynamic> Put(dynamic args, CancellationToken ct)
		{
			var id = (Guid)args.Id;

			var model = _consultationService.GetSingle(x => x.Id == id);

			if (model == null)
				return HttpStatusCode.NotFound;

			model = UpdateFromBody(model);

			_consultationService.Update(model);

			return Response.AsJson(model);
		}

		protected virtual async Task<dynamic> Delete(dynamic args, CancellationToken ct)
		{
			var id = (Guid)args.Id;

			var model = _consultationService.GetSingle(x => x.Id == id);

			if (model == null)
				return HttpStatusCode.NotFound;
			
			_consultationService.Remove(model);

			return Response.AsJson(model);
		}
    }
}
