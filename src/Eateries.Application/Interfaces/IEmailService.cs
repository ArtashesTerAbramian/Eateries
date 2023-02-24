using Eateries.Application.DTOs.Email;

namespace Eateries.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}