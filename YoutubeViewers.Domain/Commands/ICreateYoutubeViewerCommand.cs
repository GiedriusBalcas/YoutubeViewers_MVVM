using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;

namespace YoutubeViewers.Domain.Commands
{
    public interface ICreateYoutubeViewerCommand
    {
        Task Execute(YoutubeViewer youtubeViewer);
    }
}
