﻿namespace DevFreela.Core.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}