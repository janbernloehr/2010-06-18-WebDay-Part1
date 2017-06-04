using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WebDay.Contracts
{
    [ServiceContract]
    public interface IBlogRepository
    {
        [OperationContract]
        void PublishPost(BlogPost post);

        [OperationContract]
        void PublishComment(int postId, BlogComment comment);

        [OperationContract]
        BlogPost GetBlogPostById(int postId);

        [OperationContract]
        BlogPost[] GetRecentPosts(int maxCount);

        [OperationContract]
        TagCloudItem[] GetTagCloud();
    }
}
