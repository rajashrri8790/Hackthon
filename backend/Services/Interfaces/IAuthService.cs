using DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IAuthService
    {
        string Register(RegisterDto dto);
        string Login(LoginDto dto);
    }
}
