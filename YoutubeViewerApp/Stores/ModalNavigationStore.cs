using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeViewerApp_CodeAlong.ViewModels;

namespace YoutubeViewerApp_CodeAlong.Stores
{
    public class ModalNavigationStore
    {
		private ViewModelBase _currentViewModel;

		public bool IsOpen => CurrentViewModel != null;

		public ViewModelBase CurrentViewModel
        {
			get { return _currentViewModel; }
			set 
			{ 
				_currentViewModel = value;
				CurrentViewmodelChanged?.Invoke();
			}
		}


        public event Action CurrentViewmodelChanged;

        internal void Close()
        {
			CurrentViewModel = null;
        }
    }
}
