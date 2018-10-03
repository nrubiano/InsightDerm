using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InsightDerm.Core.Dto;
using InsightDerm.Core.Service;
using Microsoft.Extensions.Options;
using Nancy;

namespace InsightDerm.Host.Api.Controllers
{
    public class DoctorsModule : BaseModule
	{
		private readonly DoctorService _doctorService;

		public DoctorsModule(IOptions<ApiSettings> apiSettings, IMapper mapper, DoctorService doctorService) : base(apiSettings, mapper)
		{
			_doctorService = doctorService;

			Get(GetPath(), (args, ctk) => Get(args, ctk));
			
			Post(GetPath(), (args, ctk) => Post(args, ctk));
			
			Put($@"{GetPath()}/{{Id}}", (args, ctk) => Put(args, ctk));
			
			Delete($@"{GetPath()}/{{Id}}", (args, ctk) => Delete(args, ctk));
		}

		protected virtual async Task<dynamic> Get(dynamic args, CancellationToken ct)
		{
			var entities = _doctorService.GetAll(x => x.Name != "");

			return Response.AsJson(entities);
		}
		
		protected virtual async Task<dynamic> Post(dynamic args, CancellationToken ct)
		{
			var model = BindBody<DoctorDto>();
	 
			var exist = _doctorService.Exist(x => string.Equals(x.Identification
				, model.Identification
				, StringComparison.OrdinalIgnoreCase));
	
			if (exist)
			{
				return Negotiate
					.WithModel($"The doctor with the name {model.Name} already exist")
					.WithStatusCode(HttpStatusCode.BadRequest);
			}
				
			_doctorService.Create(model);
  
			return Response.AsJson(model);
		}
		
		protected virtual async Task<dynamic> Put(dynamic args, CancellationToken ct)
		{
			var id = (Guid)args.Id;
	
			var model = _doctorService.GetSingle(x => x.Id == id);
	
			if (model == null)
				return HttpStatusCode.NotFound;
	
			model = UpdateFromBody(model);
	
			_doctorService.Update(model);
	
			return Response.AsJson(model);
		}
	
		protected virtual async Task<dynamic> Delete(dynamic args, CancellationToken ct)
		{
			var id = (Guid)args.Id;
	
			var model = _doctorService.GetSingle(x => x.Id == id);
	
			if (model == null)
				return HttpStatusCode.NotFound;
				
			_doctorService.Remove(model);
	
			return Response.AsJson(model);
		}
	}
}
