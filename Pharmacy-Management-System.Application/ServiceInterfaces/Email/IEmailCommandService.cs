using Pharmacy_Management_System.Application.Features.Email.Command.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Application.ServiceInterfaces.Email
{
    public interface IEmailCommandService
    {
        Task<Result> SendOtpEmailAsync(string email, string otp);
        Task<Result> OtpVerificationAsync(VerifyOtpRequestDto request);
    }
}
