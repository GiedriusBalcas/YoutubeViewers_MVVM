using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeViewerApp_CodeAlong.Stores;
using YoutubeViewerApp_CodeAlong.ViewModels;

namespace YoutubeViewerApp_CodeAlong.Commands
{
    public class LoadYoutubeViewersCommand : AsyncCommandBase
    {
        private readonly YoutubeViewerViewModel _youtubeViewerViewModel;
        private readonly YoutubeViewersStore _youtubeViewersStore;

        public LoadYoutubeViewersCommand(YoutubeViewerViewModel youtubeViewerViewModel, YoutubeViewersStore youtubeViewersStore)
        {
            this._youtubeViewerViewModel = youtubeViewerViewModel;
            _youtubeViewersStore = youtubeViewersStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _youtubeViewerViewModel.ErrorMessage = null;
            _youtubeViewerViewModel.IsLoading = true;

            try
            {
                await _youtubeViewersStore.Load();
            }
            catch (Exception)
            {
                _youtubeViewerViewModel.ErrorMessage = "Failed to load Youtube viewers. Restart of application is needed.";
            }
            finally
            {
                _youtubeViewerViewModel.IsLoading = false;
            }
        }
    }
}
