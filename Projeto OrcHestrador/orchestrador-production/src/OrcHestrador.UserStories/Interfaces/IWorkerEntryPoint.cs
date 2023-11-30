using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcHestrador.UserStories.Interfaces
{
    public interface IWorkerEntryPoint
    {
        Task exec(CancellationToken stoppingToken);
    }
}
