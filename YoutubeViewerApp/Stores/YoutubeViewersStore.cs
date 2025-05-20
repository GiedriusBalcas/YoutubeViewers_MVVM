using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Commands;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.Domain.Queries;

namespace YoutubeViewerApp_CodeAlong.Stores
{
    public class YoutubeViewersStore
    {
        private readonly IGetAllYoutubeViewersQuery _getAllYoutubeViewersQuery;
        private readonly ICreateYoutubeViewerCommand _createYoutubeViewerCommand;
        private readonly IUpdateYoutubeViewerCommand _updateYoutubeViewerCommand;
        private readonly IDeleteYoutubeViewerCommand _deleteYoutubeViewerCommand;

        private readonly List<YoutubeViewer> _youtubeViewers;
        public IEnumerable<YoutubeViewer> YoutubeViewers => _youtubeViewers;
        public YoutubeViewersStore(
            IGetAllYoutubeViewersQuery getAllYoutubeViewersQuery, 
            ICreateYoutubeViewerCommand createYoutubeViewerCommand, 
            IUpdateYoutubeViewerCommand updateYoutubeViewerCommand,
            IDeleteYoutubeViewerCommand deleteYoutubeViewerCommand)
        {
            _getAllYoutubeViewersQuery = getAllYoutubeViewersQuery;
            _createYoutubeViewerCommand = createYoutubeViewerCommand;
            _updateYoutubeViewerCommand = updateYoutubeViewerCommand;
            _deleteYoutubeViewerCommand = deleteYoutubeViewerCommand;

            _youtubeViewers = new List<YoutubeViewer>();
        }


        public event Action YoutubeViewersLoaded;
        public event Action<YoutubeViewer> YoutubeViewerAdded;
        public event Action<YoutubeViewer> YoutubeViewerUpdated;
        public event Action<Guid> YoutubeViewerDeleted;

        public async Task Load()
        {
            IEnumerable<YoutubeViewer> youtubeViewers = await _getAllYoutubeViewersQuery.Execute();
            await Task.Delay(TimeSpan.FromSeconds(2));
            _youtubeViewers.Clear();
            _youtubeViewers.AddRange(youtubeViewers);
            YoutubeViewersLoaded?.Invoke();
        }
        public async Task Add(YoutubeViewer youtubeViewer)
        {
            await _createYoutubeViewerCommand.Execute(youtubeViewer);
            await Task.Delay(TimeSpan.FromSeconds(2));
            _youtubeViewers.Add(youtubeViewer);


            YoutubeViewerAdded?.Invoke(youtubeViewer);
        }

        public async Task Update(YoutubeViewer youtubeViewer)
        {
            await _updateYoutubeViewerCommand.Execute(youtubeViewer);
            await Task.Delay(TimeSpan.FromSeconds(2));
            var currentIndex = _youtubeViewers.FindIndex(y => y.Id == youtubeViewer.Id);

            if(currentIndex != -1)
            {
                _youtubeViewers[currentIndex] = youtubeViewer;
            }
            else
            {
                _youtubeViewers.Add(youtubeViewer);
            }

            YoutubeViewerUpdated?.Invoke(youtubeViewer);
        }

        public async Task Delete(Guid id)
        {
            await _deleteYoutubeViewerCommand.Execute(id);
            await Task.Delay(TimeSpan.FromSeconds(1));
            _youtubeViewers.RemoveAll(y => y.Id == id);

            YoutubeViewerDeleted?.Invoke(id);
        }

    }
}
