using LimaArrendamentos.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace LimaArrendamentos.Data
{
    public interface ISendEmailRepository : IGenericRepository<SendEmail>
    {
        public IQueryable GetAllWithUsers();

        SendEmail ToSendEmail(string userid, int realtyId);
    }
}
