using System.Reflection.PortableExecutable;

namespace Web.ViewModels.Pagination
{
    public class PaginationHelper
    {
        public int? Page { get; set; }
        public int? PageCount { get; set; }
        public int? ProductCount { get; set; }
        public int? ProductsOnPage  { get; set; }
        public bool NextIsEnabled { get; set; }
        public bool PreviousIsEnabled { get; set; }

    }
}
