using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;
using YoutubeViewerApp_CodeAlong.Stores;
using YoutubeViewerApp_CodeAlong.ViewModels;

namespace YoutubeViewerApp_CodeAlong.Commands
{
    public class AddYoutubeViewerCommand : AsyncCommandBase
    {
        private readonly AddYoutubeViewerViewModel _addYoutubeViewerViewModel;
        private readonly YoutubeViewersStore _youtubeViewersStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public AddYoutubeViewerCommand(ViewModels.AddYoutubeViewerViewModel addYoutubeViewerViewModel, YoutubeViewersStore youtubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            _addYoutubeViewerViewModel = addYoutubeViewerViewModel;
            _youtubeViewersStore = youtubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
        }


        public override async Task ExecuteAsync(object parameter)
        {
            var formViewModel = _addYoutubeViewerViewModel.YoutubeViewerDetailtsFormViewModel;
            formViewModel.ErrorMessage = null;

            formViewModel.IsSubmitting = true;

            YoutubeViewer youtubeViewer = new YoutubeViewer(
                Guid.NewGuid(),
                formViewModel.Username, 
                formViewModel.IsSubscribed, 
                formViewModel.IsMember);
            //Add user to the database
            try
            {

                await _youtubeViewersStore.Add(youtubeViewer);

                _modalNavigationStore.Close();

            }
            catch (Exception)
            {
                formViewModel.ErrorMessage = "Failed to add a viewer. Restart everything.";
            }
            finally
            {
                formViewModel.IsSubmitting = false;
            }
        }
    }
}
