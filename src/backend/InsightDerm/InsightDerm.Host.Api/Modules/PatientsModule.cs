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
    public class PatientsModule : BaseModule
    {
        readonly PatientService _patientService;

        public PatientsModule(IOptions<ApiSettings> apiSettings, IMapper mapper, PatientService patientService) : base(apiSettings, mapper)
        {
            _patientService = patientService;

            Get(GetPath(), (args, ctk) => Get(args, ctk));

            Post(GetPath(), (args, ctk) => Post(args, ctk));

            Put($@"{GetPath()}/{{Id}}", (args, ctk) => Put(args, ctk));

            Delete($@"{GetPath()}/{{Id}}", (args, ctk) => Delete(args, ctk));

        }

	    protected virtual async Task<dynamic> Get(dynamic args, CancellationToken ct)
	    {
		    string filter = Request.Query["$filter"];
		    
		    var entities = _patientService.GetAll(filter, "");

		    return Response.AsJson(entities);
	    }
	    
		protected virtual async Task<dynamic> Post(dynamic args, CancellationToken ct)
		{
            var model = BindBody<PatientDto>();

            var exist = _patientService.Exist(x => string.Equals(x.Name, model.Name, StringComparison.OrdinalIgnoreCase));
	
            if (exist)
            {
				return Negotiate
						.WithModel($"The patient with the name {model.Name} already exist")
							.WithStatusCode(HttpStatusCode.BadRequest);
            }
                
            _patientService.Create(model);

            return Response.AsJson(model);
		}

		protected virtual async Task<dynamic> Put(dynamic args, CancellationToken ct)
		{
            var id = (Guid)args.Id;

            var model = _patientService.GetSingle(x => x.Id == id);

            if (model == null)
                return HttpStatusCode.NotFound;

            model = UpdateFromBody(model);

            _patientService.Update(model);

			return Response.AsJson(model);
		}

		protected virtual async Task<dynamic> Delete(dynamic args, CancellationToken ct)
		{
			var id = (Guid)args.Id;

            var model = _patientService.GetSingle(x => x.Id == id);

			if (model == null)
				return HttpStatusCode.NotFound;
            
			_patientService.Remove(model);

			return Response.AsJson(model);
		}
    }
}
