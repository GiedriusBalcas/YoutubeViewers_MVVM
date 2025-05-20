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
    public class YoutubeViewesListingItemViewModel : ViewModelBase
    {
        public YoutubeViewer YoutubeViewer { get; private set; }

        public string Username => YoutubeViewer.Username;

        private bool _isDeleting;

        public bool IsDeleting
        {
            get { return _isDeleting; }
            set 
            { 
                _isDeleting = value; 
                OnPropertyChanged(nameof(IsDeleting));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        private string? _errorMessage;

        public string? ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));

            }
        }

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }



        public YoutubeViewesListingItemViewModel(YoutubeViewer youtubeViewer, YoutubeViewersStore youtubeViewersStore, ModalNavigationStore modalNavigationStore)
        {
            YoutubeViewer = youtubeViewer;

            EditCommand = new OpenEditYoutubeViewerCommand(this, youtubeViewersStore, modalNavigationStore);
            DeleteCommand = new DeleteYoutubeViewerCommand(this, youtubeViewersStore);
        }

        public void Update(YoutubeViewer youtubeViewer)
        {
            YoutubeViewer = youtubeViewer;

            OnPropertyChanged(nameof(Username));
        }
    }
}
