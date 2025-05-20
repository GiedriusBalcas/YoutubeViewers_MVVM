using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YoutubeViewerApp_CodeAlong.Commands;
using YoutubeViewers.Domain.Models;
using YoutubeViewerApp_CodeAlong.Stores;

namespace YoutubeViewerApp_CodeAlong.ViewModels
{
    public class YoutubeViewersListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<YoutubeViewesListingItemViewModel> _youtubeViewesListingItemViewModels;
        private readonly YoutubeViewersStore _youtubeViewersStore;
        private readonly SelectedYoutubeViewerStore _selectedYoutubeViewerStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public IEnumerable<YoutubeViewesListingItemViewModel> YoutubeViewesListingItemsViewModels => _youtubeViewesListingItemViewModels;

        //private YoutubeViewesListingItemViewModel _selectedYoutubeViewerListingItemViewModel;

        public YoutubeViewesListingItemViewModel SelectedYoutubeViewerListingItemViewModel
        {
            get { return _youtubeViewesListingItemViewModels.FirstOrDefault(y => y.YoutubeViewer?.Id == _selectedYoutubeViewerStore.SelecteddYoutubeViewer?.Id); }
            set 
            { 
                _selectedYoutubeViewerStore.SelecteddYoutubeViewer = value?.YoutubeViewer;
            }
        }


        public YoutubeViewersListingViewModel(YoutubeViewersStore youtubeViewersStore, SelectedYoutubeViewerStore selectedYoutubeViewerStore, ModalNavigationStore modalNavigationStore)
        {
            _youtubeViewesListingItemViewModels = new ObservableCollection<YoutubeViewesListingItemViewModel>();
            _youtubeViewersStore = youtubeViewersStore;
            _selectedYoutubeViewerStore = selectedYoutubeViewerStore;
            _modalNavigationStore = modalNavigationStore;

            //LoadYoutubeViewersCommand = new LoadYoutubeViewersCommand(youtubeViewersStore);

            _selectedYoutubeViewerStore.SelectedYoutubeViewerChanged += SelectedYoutubeViewerStore_SelectedYoutubeViewerChanged;

            _youtubeViewersStore.YoutubeViewersLoaded += YoutubeViewersStore_YoutubeViewersLoaded;
            _youtubeViewersStore.YoutubeViewerAdded += YoutubeViewersStore_YoutubeViewerAdded;
            _youtubeViewersStore.YoutubeViewerUpdated += YoutubeViewersStore_YoutubeViewerUpdated;
            _youtubeViewersStore.YoutubeViewerDeleted += YoutubeViewersStore_YoutubeViewerDeleted;

            _youtubeViewesListingItemViewModels.CollectionChanged += YoutubeViewesListingItemViewModels_CollectionChanged;
        }

        private void YoutubeViewesListingItemViewModels_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedYoutubeViewerListingItemViewModel));
        }

        private void SelectedYoutubeViewerStore_SelectedYoutubeViewerChanged()
        {
            OnPropertyChanged(nameof(SelectedYoutubeViewerListingItemViewModel));

        }

        private void YoutubeViewersStore_YoutubeViewerDeleted(Guid id)
        {
            var itemViewmodel = _youtubeViewesListingItemViewModels.FirstOrDefault(y => y.YoutubeViewer?.Id == id);

            if(itemViewmodel != null)
            {
                _youtubeViewesListingItemViewModels.Remove(itemViewmodel);
            }
        }

        private void YoutubeViewersStore_YoutubeViewersLoaded()
        {
            _youtubeViewesListingItemViewModels.Clear();

            foreach (YoutubeViewer youtubeViewer in _youtubeViewersStore.YoutubeViewers)
            {
                AddYoutubeViewer(youtubeViewer);
            }
        }


        protected override void Dispose()
        {
            _youtubeViewersStore.YoutubeViewerAdded -= YoutubeViewersStore_YoutubeViewerAdded;
            _youtubeViewersStore.YoutubeViewerUpdated -= YoutubeViewersStore_YoutubeViewerUpdated;
            _youtubeViewersStore.YoutubeViewersLoaded -= YoutubeViewersStore_YoutubeViewersLoaded;
            _youtubeViewersStore.YoutubeViewerDeleted -= YoutubeViewersStore_YoutubeViewerDeleted;
            _selectedYoutubeViewerStore.SelectedYoutubeViewerChanged -= SelectedYoutubeViewerStore_SelectedYoutubeViewerChanged;

            base.Dispose();
        }

        private void YoutubeViewersStore_YoutubeViewerAdded(YoutubeViewer youtubeViewer)
        {
            AddYoutubeViewer(youtubeViewer);
        }

        private void YoutubeViewersStore_YoutubeViewerUpdated(YoutubeViewer youtubeViewer)
        {
            var youtubeViewerViewModel = _youtubeViewesListingItemViewModels.FirstOrDefault(y => y.YoutubeViewer.Id == youtubeViewer.Id);

            if (youtubeViewerViewModel != null)
            {
                youtubeViewerViewModel.Update(youtubeViewer);
            }
        }

        private void AddYoutubeViewer(YoutubeViewer youtubeViewer)
        {
            //ICommand editCommand = new OpenEditYoutubeViewerCommand(youtubeViewer, _modalNavigationStore);
            YoutubeViewesListingItemViewModel itemViewModel = new YoutubeViewesListingItemViewModel(youtubeViewer, _youtubeViewersStore, _modalNavigationStore);
            _youtubeViewesListingItemViewModels.Add(itemViewModel);
        }
    }
}
