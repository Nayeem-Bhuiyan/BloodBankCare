using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.Bloodbank
{
    public class BloodRequestReceivedInfo:Base
    {
        //blood request info list e accept button e bloodrequestInfoId included thakbe

        public int? BloodRequestInfoId { get; set; }  //accept button theke asbe not dropdown
        public virtual BloodRequestInfo BloodRequestInfo { get; set; }

        public int? isAccept { get; set; }  //0=pending(user) ,1=accept(donor)
        public string acceptedBy { get; set; }  //donorUserId: accept button e click korle ei user Id Porbe
        public int? isDonated { get; set; } //0=Ongoing ,1=NoDonation ,2=Donated //eta korbe donor blood donate korar por (Acceptedby e data porlei kebol isDonated Button show korbe abong accepted by button read only hoye jabe  )


        //BloodRequestInfoId wise ei table er requestAcceptList ta dekhte parbe requestUser


    }
}
