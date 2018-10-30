using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IHasherPassword
    {
        string GetHash(string value);
        string GetHash(byte[] value);
    }
}
