using System.Reflection.PortableExecutable;

namespace Web.ViewModels.Pagination
{
    public class PaginationHelper
    {
        public int? StartIndex { get; set; }
        public int? NumberOfProducts { get; set; }
        public int? NumberOfPages { get; set; }
        public int? MaxIndex { get; set; }

    }
}
