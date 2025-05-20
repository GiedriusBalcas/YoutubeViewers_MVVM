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
    public class YoutubeViewerViewModel : ViewModelBase
    {
        public YoutubeViewersListingViewModel YoutubeViewersListingViewModel { get;}
        public YoutubeViewersDetailsViewModel YoutubeViewersDetailsViewModel { get;}

        private bool _isLoading;

        public bool IsLoading
        {   
            get { return _isLoading; }
            set 
            { 
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
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



        public ICommand AddYoutubeViewersCommand { get; }
        public ICommand LoadYoutubeViewersCommand { get; }


        public YoutubeViewerViewModel(YoutubeViewersStore youtubeViewersStore, SelectedYoutubeViewerStore _selectedYoutubeViewerStore, ModalNavigationStore modalNavigationStore)
        {
            YoutubeViewersListingViewModel = new YoutubeViewersListingViewModel(youtubeViewersStore, _selectedYoutubeViewerStore, modalNavigationStore);
            YoutubeViewersDetailsViewModel = new YoutubeViewersDetailsViewModel(_selectedYoutubeViewerStore);

            LoadYoutubeViewersCommand = new LoadYoutubeViewersCommand(this, youtubeViewersStore);
            AddYoutubeViewersCommand = new OpenAddYoutubeViewerCommand(youtubeViewersStore, modalNavigationStore);
        }


        public static YoutubeViewerViewModel LoadViewModel(YoutubeViewersStore youtubeViewersStore, SelectedYoutubeViewerStore selectedYoutubeViewerStore, ModalNavigationStore modalNavigationStore)
        {
            var viewModel = new YoutubeViewerViewModel(youtubeViewersStore, selectedYoutubeViewerStore, modalNavigationStore);

            viewModel.LoadYoutubeViewersCommand.Execute(null);

            return viewModel;
        }
    }
}
