using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeViewers.EntityFramework.DTOs;

namespace YoutubeViewers.EntityFramework
{
    public class YoutubeViewersDbContext : DbContext
    {
        public YoutubeViewersDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<YoutubeViewerDto> YoutubeViewers { get; set; }
    }
}
