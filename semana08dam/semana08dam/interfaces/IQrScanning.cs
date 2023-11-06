using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace semana08dam.interfaces
{
    internal interface IQrScanning
    {
        Task<string> ScanAsync();
    }
}
