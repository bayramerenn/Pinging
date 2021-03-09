using Pinging.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pinging.Entity.Abstract
{
    public interface IIp
    {
        List<Ip> GetAll();

        Ip GetIp(int id);

        List<AdslType> GetAdslAll();
        void Delete(int id);
        void Create(Ip entity);
        void Update(Ip entity);

    }
}
