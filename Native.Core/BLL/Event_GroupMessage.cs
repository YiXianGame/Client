using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Core.BLL
{
    public class Event_GroupMessage : IGroupMessage
    {
        public void GroupMessage(object sender, CQGroupMessageEventArgs e)
        {
            Transfer_Station.CQ_To_Server(e.Message, e.FromQQ, e.FromGroup);
        }
    }
}
