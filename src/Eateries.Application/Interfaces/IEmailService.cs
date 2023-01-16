using Eateries.Application.DTOs.Email;
using System.Threading.Tasks;

namespace Eateries.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}