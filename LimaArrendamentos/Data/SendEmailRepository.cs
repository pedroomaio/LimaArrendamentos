using LimaArrendamentos.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace LimaArrendamentos.Data
{
    public class SendEmailRepository : GenericRepository<SendEmail>, ISendEmailRepository
    {
        private readonly DataContext _context;

        public SendEmailRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable GetAllWithUsers()
        {
            return _context.SendEmails.Include(p => p.User);
}

        public SendEmail ToSendEmail(string userid, int realtyId)
{
            return new SendEmail
            {
                UserId = userid,
                RealtyId = realtyId
            };
        }
    }
}
