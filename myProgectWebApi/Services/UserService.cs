using Microsoft.IdentityModel.Tokens;
using myProgectWebApi.DAL.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace myProgectWebApi.Services
{
    public class UserService: IUserService
    {


            private readonly IConfiguration _config;

            public UserService(IConfiguration config)
            {
                _config = config;
            }

            public async Task<AuthResponse> RegisterAsync(UserRegisterDto dto)
            {
               
                await SendEmailAsync(dto.Email, "Registration succeses!",
                    $"Hello, {dto.FullName}! Your registry is succese");

                
                var token = GenerateJwtToken(dto.Email);

                return new AuthResponse
                {
                    Success = true,
                    Message = "Registrion successe",
                    Token = token
                };
            }

            private async Task SendEmailAsync(string toEmail, string subject, string body)
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("nastya.lyashyk08@gmail.com", "140908trn"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("nastya.lyashyk08@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }

            private string GenerateJwtToken(string email)
            {
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }

