using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDay.WebApp1
{
  public partial class PublishPost : System.Web.UI.Page
  {
    protected void PublishButton_Click(object sender, EventArgs e) {
      var client = new RepositoryServiceReference.BlogRepositoryClient();

      var post = new RepositoryServiceReference.BlogPost()
      {
        Title = this.TitleBox.Text,
        Content = this.Editor1.Content,
        DateCreated = DateTime.Now,
        Username = User.Identity.Name
      };

      client.PublishPost(post);

      Response.Redirect("~/default.aspx");
    }

    protected void CancelButton_Click(object sender, EventArgs e) {
      Response.Redirect("~/default.aspx");
    }
  }
}