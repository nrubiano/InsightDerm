using System;
using System.Threading;
using System.Threading.Tasks;
using InsightDerm.Core.Dto;
using Nancy;

namespace InsightDerm.Host.Api.Modules
{
    public partial class ConsultationsModule 
    {
        protected virtual async Task<dynamic> GetImages(dynamic args, CancellationToken ct)
        {
            var id = (Guid)args.Id;
            
            //string filter = Request.Query["$filter"];

            var entities = _diagnosticImageService.GetAll(x => x.ConsultationId == id);

            return Response.AsJson(entities);
        }

        protected virtual async Task<dynamic> GetSingleImage(dynamic args, CancellationToken ct)
        {
            var id = (Guid)args.ImageId;

            var entities = _diagnosticImageService.GetSingle(x => x.Id == id);

            return Response.AsJson(entities);
        }

        protected virtual async Task<dynamic> PostImage(dynamic args, CancellationToken ct)
        {
            var consultationId = (Guid)args.Id;
            
            var model = BindBody<DiagnosticImageDto>();
            model.ConsultationId = consultationId;
            
            model = _diagnosticImageService.Create(model);

            return Response.AsJson(model);
        }

        protected virtual async Task<dynamic> PutImage(dynamic args, CancellationToken ct)
        {
            var id = (Guid)args.ImageId;

            var model = _diagnosticImageService.GetSingle(x => x.Id == id);

            if (model == null)
                return HttpStatusCode.NotFound;

            model = UpdateFromBody(model);

            _diagnosticImageService.Update(model);

            return Response.AsJson(model);
        }

        protected virtual async Task<dynamic> DeleteImage(dynamic args, CancellationToken ct)
        {
            var id = (Guid)args.ImageId;

            var model = _diagnosticImageService.GetSingle(x => x.Id == id);

            if (model == null)
                return HttpStatusCode.NotFound;

            _diagnosticImageService.Remove(model);

            return Response.AsJson(model);
        }
    }
}
