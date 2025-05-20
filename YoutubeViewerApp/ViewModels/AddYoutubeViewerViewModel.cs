using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YoutubeViewerApp_CodeAlong.Commands;
using YoutubeViewerApp_CodeAlong.Stores;

namespace YoutubeViewerApp_CodeAlong.ViewModels
{
    public class AddYoutubeViewerViewModel : ViewModelBase
    {

        public YoutubeViewerDetailtsFormViewModel YoutubeViewerDetailtsFormViewModel { get; }

        public AddYoutubeViewerViewModel(YoutubeViewersStore youtubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);

            ICommand submitCommand = new AddYoutubeViewerCommand(this, youtubeViewersStore,modalNavigationStore);
            YoutubeViewerDetailtsFormViewModel = new YoutubeViewerDetailtsFormViewModel(submitCommand, cancelCommand);
        }
    }
}
