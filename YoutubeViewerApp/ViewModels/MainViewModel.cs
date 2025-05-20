using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeViewerApp_CodeAlong.Stores;

namespace YoutubeViewerApp_CodeAlong.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        public YoutubeViewerViewModel YoutubeViewerViewModel { get; }
        
        public ViewModelBase CurrentModalViewmodel => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public MainViewModel(ModalNavigationStore modalNavigationStore, YoutubeViewerViewModel youtubeViewerViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            YoutubeViewerViewModel = youtubeViewerViewModel;
            _modalNavigationStore.CurrentViewmodelChanged += ModalNavigationStore_CurrentViewmodelChanged;

        }

        protected override void Dispose()
        {
            _modalNavigationStore.CurrentViewmodelChanged -= ModalNavigationStore_CurrentViewmodelChanged;
            base.Dispose();
        }

        private void ModalNavigationStore_CurrentViewmodelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewmodel));
            OnPropertyChanged(nameof(IsModalOpen));
        }
    }
}
