using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pinging.Entity.Abstract;
using Pinging.Entity.Concrete;
using Pinging.Models;
using Pinging.PingServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pinging.Controllers
{
    public class HomeController : Controller
    {
        private readonly PingService _pingService;
        private readonly IIp _ip;

        public HomeController(PingService pingService, IIp ip)
        {
            _pingService = pingService;
            _ip = ip;
        }

        public IActionResult Index()
        {
            return View(new List<PingGroupViewModel>());
        }

        public IActionResult Reflesh()
        {
          
            var pingModel = _pingService.GetPingModel(_ip.GetAll());
          

            var groupPing = pingModel.GroupBy(g => new { g.Adress, g.HostName, g.Modem }).
                Select(s =>
                new PingGroupViewModel
                {
                    Address = s.Key.Adress,
                    Name = s.Key.HostName,
                  //  Dns = s.Key.DnsName,
                    Modem = s.Key.Modem,
                    RTT1 = s.Where(x => x.RoundTT == "RTT1").Sum(sum => sum.RoundTripTime),
                    RTT2 = s.Where(x => x.RoundTT == "RTT2").Sum(sum => sum.RoundTripTime),
                    RTT3 = s.Where(x => x.RoundTT == "RTT3").Sum(sum => sum.RoundTripTime),
                    RTT4 = s.Where(x => x.RoundTT == "RTT4").Sum(sum => sum.RoundTripTime),
                    RTT5 = s.Where(x => x.RoundTT == "RTT5").Sum(sum => sum.RoundTripTime),
                    Status = s.Select(x => x.Status).ToList()
                }).ToList();
      
            return View("Index", groupPing);
        }

        public IActionResult Create()
        {
            var model = new IpViewModel
            {
                AdslType = new SelectList(_ip.GetAdslAll(), "AdslTypeId", "AdslTypeDesc")
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Ip ip)
        {
            ModelState.Remove("AdslTypes");
            if (ModelState.IsValid)
            {
                _ip.Create(ip);

                TempData["Message"] = "Ip adres başarıyla eklendi";

                return RedirectToAction("Create");
            }
            else
            {
                ModelState.AddModelError("", "Hata bilgiler girdiniz");
            }
            return View();
        }

        public IActionResult List()
        {
            var list = _ip.GetAll();
            return View(list);
        }

        public IActionResult Delete(int id)
        {
            _ip.Delete(id);
            return RedirectToAction("List");
        }

        public IActionResult Update(int id)
        {
            var model = _ip.GetIp(id);

            IpViewModel _ipViewModel = new IpViewModel
            {
                Id = model.Id,
                IpAddress = model.IpAddress,
                IpHostName = model.IpHostName,
                AdslTypeId = model.AdslTypeId,
                AdslType = new SelectList(_ip.GetAdslAll(), "AdslTypeId", "AdslTypeDesc"),
                Port = model.Port
            };

            // ViewBag.AdslType = new SelectList(_ip.GetAdslAll(), "AdslTypeId", "AdslTypeDesc");
            return View(_ipViewModel);
        }

        [HttpPost]
        public IActionResult Update(IpViewModel ipViewModel)
        {
            ModelState.Remove(nameof(ipViewModel.AdslType));
            if (ModelState.IsValid)
            {
                _ip.Update(new Ip
                {
                    Id = ipViewModel.Id,
                    IpAddress = ipViewModel.IpAddress,
                    IpHostName = ipViewModel.IpHostName,
                    Port = ipViewModel.Port,
                    AdslTypeId = ipViewModel.AdslTypeId
                });

                return RedirectToAction("List");
            }
            ModelState.AddModelError("", "Hata oluştu tekrar deneyiniz");

            return View();
        }

        public IActionResult Tracert([FromBody]DnsHost host)
        {
            List<string> ipList = new List<string>();

            foreach (var item in _pingService.GetTraceRoute(host.HostName))
            {
                ipList.Add(item.ToString());
            }
            var a = ipList[0];

            return Json(ipList);
        }
    }
}

public class DnsHost
{
    public string HostName { get; set; }
}