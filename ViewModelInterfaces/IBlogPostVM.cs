
using System.Collections.Generic;
namespace ViewModelInterfaces
{
    public interface IBlogPostVM
    {

        int BlogPostID { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string ContentText { get; set; }
        string ContentHtml { get; set; }
        //DateTime DateCreated { get; set; }
        //DateTime DateModified { get; set; }
        bool IsPublished { get; set; }
        int CategoryID { get; set; }
        int AuthorID { get; set; }
        string ImageURL { get; set; }

    }
}
