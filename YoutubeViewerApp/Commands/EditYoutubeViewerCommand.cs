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
    public class EditYoutubeViewerCommand : AsyncCommandBase
    {
        private readonly EditYoutubeViewerViewModel _editYoutubeViewerViewModel;
        private readonly YoutubeViewersStore _youtubeViewersStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public EditYoutubeViewerCommand(EditYoutubeViewerViewModel editYoutubeViewerViewModel, YoutubeViewersStore youtubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            _editYoutubeViewerViewModel = editYoutubeViewerViewModel;
            _youtubeViewersStore = youtubeViewersStore;
            _modalNavigationStore = modalNavigationStore;
        }


        public override async Task ExecuteAsync(object parameter)
        {
            var formViewModel = _editYoutubeViewerViewModel.YoutubeViewerDetailtsFormViewModel;
            formViewModel.ErrorMessage = null;
            formViewModel.IsSubmitting = true;

            YoutubeViewer youtubeViewer = new YoutubeViewer(
                _editYoutubeViewerViewModel.YoutubeViewerId,
                formViewModel.Username,
                formViewModel.IsSubscribed,
                formViewModel.IsMember);
            //Add user to the database
            try
            {

                await _youtubeViewersStore.Update(youtubeViewer);

                _modalNavigationStore.Close();

            }
            catch (Exception)
            {
                formViewModel.ErrorMessage = "Failed to update the viewer. Restart in needed.";
            }
            finally
            {
                formViewModel.IsSubmitting = false;
            }

            //_youtubeViewersStore.Update();

            //_modalNavigationStore.Close();
        }
    }
}
