using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public class DebugMailService : IMailServices
    {
        public void Sendmail(string To, string From, string Subject, string Body)
        {
            Debug.Write($"Sending Mail To: {To}, From: {From}, Subject: {Subject}, Body: {Body}");
        }
    }
}
