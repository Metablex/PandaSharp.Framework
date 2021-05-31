using System;
using System.Threading;
using System.Threading.Tasks;

namespace PandaSharp.Framework.Services.Contract
{
    public interface ICommandBase
    {
        Task ExecuteAsync(CancellationToken cancellationToken);

        Task ExecuteAsync();

        Uri GetCommandUri();
    }
}