using Logicea_Cards.Validation;

namespace Logicea_Cards.Models
{
    public class QueryParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
        public string Name { get; set; }
      
        public string Color { get; set; } // Ensure format validation
      
        public string Status { get; set; }

        public string DateOfCreation { get; set; }
    }
}
