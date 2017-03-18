using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public interface IMailServices  //we will define the interface that each of the mailservices will need. We can IMPLEMENT a service now based on this interface
    {
        void Sendmail(string To, string From, string Subject, string Body);
      
    }
}
