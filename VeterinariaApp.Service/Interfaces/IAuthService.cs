
using VeterinariaApp.Domain.DTOs;
using VeterinariaApp.Domain.Models;

namespace VeterinariaApp.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto dto);
        Task<Usuario> RegisterAsync(RegistroDto dto);
    }
}
