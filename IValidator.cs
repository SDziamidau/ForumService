using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;

namespace ForumRepository
{
    public interface IValidator
    {
        bool ValidatePost(IPost post);
        bool ValidateComment(IComment comment);
    }
}
