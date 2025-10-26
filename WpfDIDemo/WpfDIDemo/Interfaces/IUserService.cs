using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDIDemo.Interfaces
{
    public interface IUserService
    {
        Task SaveUserDataAsync();
    }
}
