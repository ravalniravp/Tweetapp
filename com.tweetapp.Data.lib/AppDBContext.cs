using com.tweetapp.Domain.lib.Entities;
using Microsoft.EntityFrameworkCore;


namespace com.tweetapp.DAL.lib
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            :base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TweetReply> TweetReplies { get; set; }
        public DbSet<ReplyTag> ReplyTags { get; set; }
        public DbSet<Likes> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
