using Data;
using DTOs;
using Models;
using Services.Interfaces;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthService(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public string Register(RegisterDto dto)
        {
            var exists = _context.Customers.Any(x => x.Email == dto.Email);

            if (exists)
                return "Email already exists";

            var customer = new Customer
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "Customer"
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return "Registration successful";
        }

        public string Login(LoginDto dto)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Email == dto.Email);

            if (customer == null)
                return "Invalid email or password";

            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, customer.PasswordHash);

            if (!isValid)
                return "Invalid email or password";

            return _jwtService.GenerateToken(customer);
        }
    }
}