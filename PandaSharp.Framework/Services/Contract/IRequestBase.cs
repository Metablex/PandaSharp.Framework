using System;
using System.Threading;
using System.Threading.Tasks;

namespace PandaSharp.Framework.Services.Contract
{
    public interface IRequestBase<T>
    {
        Task<T> ExecuteAsync(CancellationToken cancellationToken);

        Task<T> ExecuteAsync();

        Uri GetRequestUri();
    }
}
