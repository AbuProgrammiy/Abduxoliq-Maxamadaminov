using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailSenderApp.Domen.Entities.Models;

namespace BookStore.Application.Services.EmailSenderService
{
    public interface ISendEmailService
    {
        public Task SendEmailAsync(Email emailModel);
    }
}
