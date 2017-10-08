using System;
using AutoMapper;
using Microsoft.Extensions.Options;
using Nancy;
using Nancy.ModelBinding;

namespace InsightDerm.Host.Api
{
	public class BaseModule : NancyModule
	{
        readonly IMapper _mapper;

        readonly ApiSettings _apiSettings;

        public BaseModule(IOptions<ApiSettings> apiSettings, IMapper mapper)
		{
			_apiSettings = apiSettings.Value;
            _mapper = mapper;
		}

		/// <summary>
		/// Build a nice path for us
		/// </summary>
		/// <returns>The path.</returns>
		public string GetPath() 
		{
			var currentClassName = GetType().Name.Replace("Module", string.Empty).ToLowerInvariant();
			return $"{_apiSettings.BasePath}/{_apiSettings.Version}/{currentClassName}";
		}

        /// <summary>
        /// Binds the body to the following type
        /// </summary>
        /// <returns>The request body.</returns>
        /// <typeparam name="T">The target type to bind the request</typeparam>
        public T BindBody<T>()
        {
            return this.Bind<T>();
        }

        /// <summary>
        /// Update the the specified target with the request body
        /// </summary>
        /// <returns>The target update with the request body values</returns>
        /// <param name="target">The target update</param>
        /// <typeparam name="T">Type of the target to use</typeparam>
        public T UpdateFromBody<T>(T target)
        {
            var source = BindBody<T>();

            return _mapper.Map(source, target);
        }
	}
}
