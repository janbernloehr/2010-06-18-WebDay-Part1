using System;
using System.Configuration;
using System.Linq;

namespace WebDay.Repository
{
    public class RepositoryService : Contracts.IBlogRepository
    {
        protected StorageDataContext CreateContext() {
            string connectionString = ConfigurationManager.ConnectionStrings["AppDatabase"].ConnectionString;

            return new StorageDataContext(connectionString);
        }

        #region IBlogRepository Members

        public void PublishPost(Contracts.BlogPost post) {
            // Create Linq2Sql DataContext
            using (var dbx = CreateContext()) {

                // Build entity for blog post
                Entities.Post postEntity = new Entities.Post()
                {
                    DateCreated = DateTime.Now,
                    Title = post.Title,
                    Content = post.Content,
                    Username = post.Username
                };

                if (post.Tags != null) {
                    // Iterate through tags
                    foreach (var tag in post.Tags) {
                        // Create new Tag in databse if needed. Otherwise just get the Id.
                        int? tagId = null;

                        dbx.EnsureTag(tag, ref tagId);

                        // Add PostTagMappings to the BlogPost entity.
                        postEntity.PostTagMappings.Add(new Entities.PostTagMapping()
                        {
                            TagId = tagId.Value
                        });
                    }
                }

                dbx.Posts.InsertOnSubmit(postEntity);

                // Save changes.
                dbx.SubmitChanges();
            }
        }

        public Contracts.BlogPost[] GetRecentPosts(int maxCount) {
            // Create Linq2Sql DataContext
            using (var dbx = CreateContext()) {
                // Build query to retreive BlogPosts from database
                // sorted by datecreated desc.
                var query = from p in dbx.Posts
                            orderby p.DateCreated descending
                            select new Contracts.BlogPost()
                            {
                                Id = p.Id,
                                Title = p.Title,
                                Content = p.Content,
                                DateCreated = p.DateCreated,
                                Username = p.Username,
                                Tags = p.PostTagMappings.Select(mapping => mapping.Tag.Title).ToArray()
                            };

                // Add a TOP (maxCount) condition and execute the query
                // into an array.
                return query.Take(maxCount).ToArray();
            }
        }

        public Contracts.TagCloudItem[] GetTagCloud() {
            // Create Linq2Sql DataContext
            using (var dbx = CreateContext()) {
                var query = from t in dbx.Tags
                            orderby t.Title
                            select new Contracts.TagCloudItem()
                            {
                                Title = t.Title,
                                Weight = t.PostTagMappings.Count
                            };

                // Execute query into array.
                var items = query.ToArray();

                // Calculate Max Weight
                var maxWeight = Math.Max(items.Max(item => item.Weight), 1);

                // Normalize Weight
                foreach (var item in items) {
                    item.Weight = (int)(item.Weight / maxWeight);
                }

                return items;
            }
        }

        public void PublishComment(int postId, Contracts.BlogComment comment) {
            // Create Linq2Sql DataContext
            using (var dbx = CreateContext()) {

                // Build entity for blog post
                Entities.Comment postComment = new Entities.Comment()
                {
                    DateCreated = DateTime.Now,
                    Username = comment.UserName,
                    Comment1 = comment.Comment,
                    PostId = postId
                };

                dbx.Comments.InsertOnSubmit(postComment);

                // Save changes.
                dbx.SubmitChanges();
            }
        }

        public Contracts.BlogPost GetBlogPostById(int postId) {
            // Create Linq2Sql DataContext
            using (var dbx = CreateContext()) {
                // Build query to retreive BlogPost from database
                var query = from p in dbx.Posts
                            orderby p.DateCreated descending
                            select new Contracts.BlogPost()
                            {
                                Id = p.Id,
                                Title = p.Title,
                                Content = p.Content,
                                DateCreated = p.DateCreated,
                                Username = p.Username,
                                Tags = p.PostTagMappings.Select(mapping => mapping.Tag.Title).ToArray(),
                                Comments = p.Comments.Select(c =>
                                    new Contracts.BlogComment()
                                    {
                                        Id = c.Id,
                                        Comment = c.Comment1,
                                        DateCreated = c.DateCreated,
                                        UserName = c.Username
                                    }).ToArray()
                            };

                // Execute query.
                return query.FirstOrDefault();
            }
        }

        #endregion
    }
}
