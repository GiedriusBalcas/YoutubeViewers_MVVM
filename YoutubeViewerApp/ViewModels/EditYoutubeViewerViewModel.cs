using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YoutubeViewerApp_CodeAlong.Commands;
using YoutubeViewers.Domain.Models;
using YoutubeViewerApp_CodeAlong.Stores;

namespace YoutubeViewerApp_CodeAlong.ViewModels
{
    public class EditYoutubeViewerViewModel : ViewModelBase
    {
        public Guid YoutubeViewerId { get; }

        public YoutubeViewerDetailtsFormViewModel YoutubeViewerDetailtsFormViewModel { get; }

        public EditYoutubeViewerViewModel(YoutubeViewer youtubeViewer, YoutubeViewersStore youtubeViewersStore,  ModalNavigationStore modalNavigationStore)
        {
            YoutubeViewerId = youtubeViewer.Id;

            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);

            ICommand editCommand = new EditYoutubeViewerCommand(this, youtubeViewersStore, modalNavigationStore);
            YoutubeViewerDetailtsFormViewModel = new YoutubeViewerDetailtsFormViewModel(editCommand, cancelCommand)
            {
                Username = youtubeViewer.Username,
                IsSubscribed = youtubeViewer.IsSubscribed,
                IsMember = youtubeViewer.IsMember
            };
        }

    }
}
