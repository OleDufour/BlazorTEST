using System;
using System.Collections.Generic;
using System.Text;

namespace TEST.Shared
{
    public interface IResponse<TModel>
    {
        TModel Data { get; set; }
    }

    public class Response<TModel> : IResponse<TModel> where TModel : class
    {
        public Response()
        {
        }

        public string Message { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public string ExceptionDetail { get; set; }
        /// <summary>
        /// Data that we need in the result for subsequent queries such as collaborator details.
        /// </summary>
        public Dictionary<string, string> FilterValues { get; set; }
        public TModel Data { get; set; }
    }

    /// <summary>
    /// Utilisée pour inclure les filtres dans le résultat.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TModelFilter"></typeparam>
    public class Response<TModel, TModelFilter> : IResponse<TModel> where TModel : class, new() where TModelFilter : class
    {
        public Response()
        {
            Data = new TModel();
        }

        public string Message { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public string ExceptionDetail { get; set; }
        /// <summary>
        /// Data that we need in the result for subsequent queries such as collaborator details.
        /// </summary>
        public TModelFilter FilterData { get; set; }
        public TModel Data { get; set; }
    }

    public class ResponseSingle<TModel> : IResponse<TModel> where TModel : struct
    {
        public ResponseSingle()
        {
            Data = new TModel();
        }

        public string Message { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public string ExceptionDetail { get; set; }
        public TModel Data { get; set; }
    }
}
