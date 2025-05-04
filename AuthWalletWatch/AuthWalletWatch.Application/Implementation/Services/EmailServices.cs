using AuthWalletWatch.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthWalletWatch.Application.Implementation.Services;

public class EmailServices : IEmailSender<ApplicationUser>
{
    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        // Реализуйте отправку письма с подтверждением
        // Например, с использованием SendGrid, MailKit или SMTP
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        // Реализуйте отправку письма для сброса пароля
        // Например, с использованием SendGrid, MailKit или SMTP
        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        // Реализуйте отправку кода для сброса пароля
        // Например, с использованием SendGrid, MailKit или SMTP
        return Task.CompletedTask;
    }
}
