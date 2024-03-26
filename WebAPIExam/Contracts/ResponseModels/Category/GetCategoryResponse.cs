namespace Contracts.RequestModels.Category
{
    public class GetCategoryResponse
    {
        public List<GetCategoryModel> categoryList { get; set; } = [];
    }
    public class GetCategoryModel
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
