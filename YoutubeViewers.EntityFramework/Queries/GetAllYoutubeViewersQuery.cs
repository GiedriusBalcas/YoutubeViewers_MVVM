using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;
using YoutubeViewers.Domain.Queries;
using YoutubeViewers.EntityFramework.DTOs;

namespace YoutubeViewers.EntityFramework.Queries
{
    public class GetAllYoutubeViewersQuery : IGetAllYoutubeViewersQuery
    {
        private readonly YoutubeViewersDbContextFactory _dbContextFactory;

        public GetAllYoutubeViewersQuery(YoutubeViewersDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<YoutubeViewer>> Execute()
        {

            using (YoutubeViewersDbContext context = _dbContextFactory.Create())
            {
                IEnumerable<YoutubeViewerDto> youtubeViewerDtos = await context.YoutubeViewers.ToListAsync();

                return youtubeViewerDtos.Select(y => new YoutubeViewer(y.Id, y.Username, y.IsSubscribed, y.IsMember));
            }
        }
    }
}
