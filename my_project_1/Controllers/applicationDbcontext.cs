using System.Collections;

namespace WebApplication1.Controllers
{
    internal class applicationDbcontext
    {
        public applicationDbcontext()
        {
        }

        public IEnumerable Roles { get; internal set; }
    }
}