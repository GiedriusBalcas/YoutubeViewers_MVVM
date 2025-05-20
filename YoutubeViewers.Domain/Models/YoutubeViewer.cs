using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeViewers.Domain.Models
{
    public class YoutubeViewer
    {
        public Guid Id { get; }
        public string Username { get; }
        public bool IsSubscribed { get;}
        public bool IsMember { get; }

        public YoutubeViewer(Guid id, string username, bool isSubscribed, bool isMember)
        {
            Id = id;
            Username = username;
            IsSubscribed = isSubscribed;
            IsMember = isMember;
        }
    }
}
