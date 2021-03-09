using Microsoft.EntityFrameworkCore;
using Pinging.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pinging.Entity.Concrete
{
    public class EfIp : IIp
    {
        private readonly AppDbContext _context;

        public EfIp(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public void Create(Ip entity)
        {
            _context.Ips.Add(entity);

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var ip = GetIp(id);

            _context.Ips.Remove(ip);

            _context.SaveChanges();
        }

        public List<AdslType> GetAdslAll()
        {
            return _context.AdslTypes.ToList();
        }

        public List<Ip> GetAll()
        {

            return _context.Ips.Include(x => x.AdslTypes).ToList();
        }

        public Ip GetIp(int id)
        { 
            var result = _context.Ips.Include(x => x.AdslTypes).FirstOrDefault(x => x.Id== id);
            return result;
        }

        public void Update(Ip entity)
        {
            var updateIp = _context.Entry(entity);
            updateIp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();

         
        }
    }
}
