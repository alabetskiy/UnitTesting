using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LearningUnitTesting.Mocking
{
    public class VideoRepository : IVideoRepository
    {
        public IEnumerable<Video> GetUnprocessedVideos()
        {
            using (var context = new VideoContext())
            {
                var videos =
                        (from video in context.Videos
                         where !video.IsProcessed
                         select video).ToList();

                return videos;
            }
                
        }
    }

    public interface IVideoRepository
    {
        IEnumerable<Video> GetUnprocessedVideos();
    }
}
