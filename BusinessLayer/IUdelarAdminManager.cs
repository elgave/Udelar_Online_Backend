using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace BusinessLayer
{
    public interface IUdelarAdminManager
    {
        ApiResponse<List<IdPassword>> GetAll();
        ApiResponse<List<IdPassword>> AddKey(IdPassword usuario);
        ApiResponse<List<IdPassword>> DeleteKey(string id);
        ApiResponse<bool> Login(IdPassword usuario);
    }
}
