using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IBettingApi
    {
        Task GetBetsAll(CancellationToken cancellationToken);
    }
}