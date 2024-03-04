using BookStore.Application.Abstractions.IServices;
using BookStore.Application.Services.AuthServices;
using BookStore.Application.Services.EmailSenderService;
using BookStore.Domain.Entities.DTOs;       // UserDTO ishlashi uchun
using BookStore.Domain.Entities.Models;
using EmailSenderApp.Domen.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;             // ControllerBase ishlashi uchun

// Ushbu Controllerda logic yozilganiga sabab ushbu controllerda fayllar bilan ishlangan
// va IWebHostEnvironment tipi faqat Controller ichida taniydi!
namespace BookStore.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class aLoginAndRegistrationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ISendEmailService _emailSender;
        private readonly Random random = new Random();
        private readonly IWebHostEnvironment _webEnv;

        public aLoginAndRegistrationController(IUserService userService,IAuthService authService, ISendEmailService emailSender, IWebHostEnvironment webEnv)
        {
            _userService = userService;
            _authService = authService;
            _emailSender = emailSender;
            _webEnv = webEnv;
        }

        [HttpGet]
        public string Login(string userName, string paasword)
        {
            User user=_userService.GetAll().FirstOrDefault(x=>x.UserName==userName&&x.Password==paasword)!;
            if (user== null)
                return "UserName yoki Pasword Xato!";
            UserDTO userDTO = new UserDTO()
            {
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Role = user.Role,
            };

            return _authService.GenerateToken(userDTO);
        }

        [HttpPost]
        public string Register(UserDTO userDTO)
        {
            User user = _userService.GetAll().FirstOrDefault(x=>x.UserName == userDTO.UserName)!;
            if (user != null)
                return "UserName allaqachon band qilingan";

            int randomNumber = random.Next(100000, 999999);
            string path = Path.Combine(_webEnv.WebRootPath, "verificationCode.txt");
            string path2 = Path.Combine(_webEnv.WebRootPath, "Model.json");
            System.IO.File.WriteAllText(path, randomNumber.ToString());
            System.IO.File.WriteAllText(path2, JsonSerializer.Serialize(userDTO,new JsonSerializerOptions { WriteIndented=true}));
            Email emailodel = new Email()
            {
                To = userDTO.Email,
                Subject = "Registratsiya uchun tastiqlash kodi:",
                Body = $"Tasdiqlash kod: {randomNumber}"
            };

            _emailSender.SendEmailAsync(emailodel);
            return "Emailingizga Tasdiqlash kodi yuborildi!\nUni VerifyEmail qismiga kiritng!";
        }

        [HttpGet]
        public string VerifyEmail(int code)
        {
            if (code != int.Parse(System.IO.File.ReadAllText(Path.Combine(_webEnv.WebRootPath, "verificationCode.txt"))))
                return "Notogri parol!\nQayta urinish bepul";

            string data = System.IO.File.ReadAllText(Path.Combine(_webEnv.WebRootPath, "Model.json"));
            UserDTO userDTO=JsonSerializer.Deserialize<UserDTO>(data)!;
            _userService.Create(userDTO);
            return "Muvaffaqiyatli registratsiya qildingiz!\nEndi siz Login orqali Token olishingiz mumkin)";
        }
    }
}
