using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;

namespace YoutubeViewerApp_CodeAlong.Stores
{
    public class SelectedYoutubeViewerStore
    {

		private readonly YoutubeViewersStore _viewersStore;
		private YoutubeViewer _selecteddYoutubeViewer;


        public YoutubeViewer SelecteddYoutubeViewer
        {
			get { return _selecteddYoutubeViewer; }
			set 
			{
				_selecteddYoutubeViewer = value;
				SelectedYoutubeViewerChanged?.Invoke();
			}
		}

		public event Action SelectedYoutubeViewerChanged;

        public SelectedYoutubeViewerStore(YoutubeViewersStore viewersStore)
        {
            _viewersStore = viewersStore;
            _viewersStore.YoutubeViewerUpdated += ViewersStore_YoutubeViewerUpdated;
            _viewersStore.YoutubeViewerAdded += ViewersStore_YoutubeViewerAdded;
        }

        private void ViewersStore_YoutubeViewerAdded(YoutubeViewer youtubeViewer)
        {
            SelecteddYoutubeViewer = youtubeViewer;
        }

        private void ViewersStore_YoutubeViewerUpdated(YoutubeViewer youtubeViewer)
        {
            if(youtubeViewer.Id == SelecteddYoutubeViewer?.Id)
            {
                SelecteddYoutubeViewer = youtubeViewer;
            }
        }
    }
}
