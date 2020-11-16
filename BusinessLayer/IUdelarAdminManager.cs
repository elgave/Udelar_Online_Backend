using System.Collections.Generic;
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
