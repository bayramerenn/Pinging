using Pinging.Entity.Concrete;
using Pinging.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pinging.PingServices
{
    public class PingService
    {
        public List<Task<PingReply>> GetPingReplies()
        {
            string[] addresses = { "www.google.com", "192.168.2.44" };
            List<Task<PingReply>> pingTasks = new List<Task<PingReply>>();
            for (int i = 0; i < 4; i++)
            {
                foreach (var address in addresses)
                {
                    pingTasks.Add(PingAsync(address));
                }
                Thread.Sleep(1000);
            }

            //Wait for all the tasks to complete
            Task.WaitAll(pingTasks.ToArray());

            //Now you can iterate over your list of pingTasks

            return pingTasks;
        }

        public List<PingViewModel> GetPingModel(List<Ip> listIp)
        {
            List<PingViewModel> pingViewModels = new List<PingViewModel>();

            Ping ping = new Ping();

            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);

            //Bir saniyede yanıt gelmez ise timeout hatası verecek
            int timeout = 1000;
            PingOptions pingOptions = new PingOptions(64, true);


            for (int i = 1; i <= 5; i++)
            {
                //var startDate = DateTime.Now;
                foreach (var item in listIp)
                {
                    PingViewModel pingView = new PingViewModel();
                    try
                    {
                        PingReply pingReply = ping.Send(item.IpAddress, timeout, buffer, pingOptions);

                        pingView.Adress = item.IpAddress;
                    //    pingView.DnsName = pingReply.Address.ToString();
                        pingView.HostName = item.IpHostName;
                        pingView.DateTime = DateTime.Now;
                        pingView.Modem = $"https://{item.IpAddress}:{item.Port}/";
                        pingView.RoundTripTime = pingReply.RoundtripTime;
                        pingView.RoundTT = "RTT" + i;
                        pingView.Status = pingReply.Status.ToString();
                        pingView.TimeToLive = pingReply.Options == null ? 0 : pingReply.Options.Ttl;

                        pingViewModels.Add(pingView);
                    }
                    catch
                    {
                        pingView.Adress = item.IpAddress;
                        pingView.HostName = item.IpHostName;
                        pingView.DateTime = DateTime.Now;
                        pingView.Status = "not found";
                        pingViewModels.Add(pingView);
                    }
                }
            //    var endDate = DateTime.Now - startDate;

                Thread.Sleep(1000);
            }

            return pingViewModels;
        }

        private static Task<PingReply> PingAsync(string address)
        {
            var tcs = new TaskCompletionSource<PingReply>();

            Ping ping = new Ping();

            ping.PingCompleted += (obj, sender) =>
            {
                tcs.SetResult(sender.Reply);
            };

            ping.SendAsync(address, new object { });

            var model = tcs.Task.Result;
            return tcs.Task;
        }

        public IEnumerable<IPAddress> GetTraceRoute(string hostName)
        {
            const int timeout = 10000;
            const int maxTTL = 30;
            const int bufferSize = 32;

            byte[] buffer = new byte[bufferSize];
            new Random().NextBytes(buffer);

            using (var pinger = new Ping())
            {
                for (int ttl = 1; ttl <= maxTTL; ttl++)
                {
                    PingOptions pingOptions = new PingOptions(ttl, true);
                    PingReply reply = pinger.Send(hostName, timeout, buffer, pingOptions);

                    if (reply.Status == IPStatus.Success || reply.Status == IPStatus.TtlExpired)
                    {
                        yield return reply.Address;
                    }

                    if (reply.Status != IPStatus.TtlExpired && reply.Status != IPStatus.TimedOut)
                    {
                        break;
                    }
                }
            }
        }
    }

    //public class PingModel
    //{
    //    public PingModel()
    //    {
    //        PingReplies = new List<Task<PingReply>>();
    //    }
    //    public List<Task<PingReply>> PingReplies { get; set; }
    //    public DateTime Date { get; set; }
    //    public string IpAddress { get; set; }
    //    public string IpHostName { get; set; }

    //    public string RoundTT { get; set; }

    //}
}