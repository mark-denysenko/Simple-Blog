using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebUI.Models.ChatModels;

namespace WebUI.Hubs
{
    public class ChatHub : Hub
    {
        private  static List<ChatUser> _users = new List<ChatUser>();

        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }

        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (!_users.Any(x => x.ConnectionId == id))
            {
                _users.Add(new ChatUser { ConnectionId = id, Nickname = userName });

                Clients.Caller.OnConnected(id, userName, _users);

                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var user = _users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (user != null)
            {
                _users.Remove(user);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, user.Nickname);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}