using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeViewerApp_CodeAlong.Stores;
using YoutubeViewerApp_CodeAlong.ViewModels;

namespace YoutubeViewerApp_CodeAlong.Commands
{
    public class DeleteYoutubeViewerCommand : AsyncCommandBase
    {
        private readonly YoutubeViewesListingItemViewModel _youtubeViewersListingItemViewModel;
        private readonly YoutubeViewersStore _youtubeViewersStore;

        public DeleteYoutubeViewerCommand(YoutubeViewesListingItemViewModel youtubeViewersListingItemViewModel, YoutubeViewersStore youtubeViewersStore)
        {
            _youtubeViewersListingItemViewModel = youtubeViewersListingItemViewModel;
            _youtubeViewersStore = youtubeViewersStore;
        }


        public override async Task ExecuteAsync(object? parameter)
        {
            _youtubeViewersListingItemViewModel.IsDeleting = true;
            _youtubeViewersListingItemViewModel.ErrorMessage = null;

            var youtubeViewer = _youtubeViewersListingItemViewModel.YoutubeViewer;
            try
            {
                await _youtubeViewersStore.Delete(youtubeViewer.Id);
            }
            catch (Exception)
            {
                _youtubeViewersListingItemViewModel.ErrorMessage = "Can't delete. Sorry.";
            }
            finally
            {
                _youtubeViewersListingItemViewModel.IsDeleting = false;
            }
        }
    
    }
}
