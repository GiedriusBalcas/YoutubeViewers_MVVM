using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YoutubeViewerApp_CodeAlong.Stores;
using YoutubeViewerApp_CodeAlong.ViewModels;

namespace YoutubeViewerApp_CodeAlong.Commands
{
    public class OpenAddYoutubeViewerCommand : CommandBase
    {
        private readonly YoutubeViewersStore _youtubeViewersStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddYoutubeViewerCommand(YoutubeViewersStore youtubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            this._youtubeViewersStore = youtubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object? parameter)
        {
            AddYoutubeViewerViewModel addYoutubeViewerViewModel = new AddYoutubeViewerViewModel(_youtubeViewersStore,_modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addYoutubeViewerViewModel;
        }
    }
}
