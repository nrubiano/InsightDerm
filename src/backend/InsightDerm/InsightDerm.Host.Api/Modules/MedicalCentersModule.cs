using System.Threading;
using System.Threading.Tasks;
using InsightDerm.Core.Service;
using Microsoft.Extensions.Options;
using Nancy;

namespace InsightDerm.Host.Api.Modules
{
    public class MedicalCentersModule : BaseModule
    {
        readonly MedicalCenterService _medicalCenterService;

        public MedicalCentersModule(IOptions<ApiSettings> apiSettings, MedicalCenterService medicalCenterService) : base(apiSettings)
        {
            _medicalCenterService = medicalCenterService;

            Get(GetPath(), (args, ctk) => Get(args, ctk));
        }

        protected virtual async Task<dynamic> Get(dynamic args, CancellationToken ct)
        {
            var entities = _medicalCenterService.GetAll(x => x.Name != string.Empty);

            return Response.AsJson(entities);
        }
    }
}
