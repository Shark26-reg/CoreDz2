using Application.Base;

namespace Application.Categories.Errors;

public static class CategoryErrors
{
    public static NotFound NotFound(Guid id) => new("Category", "Category with id " + id + " not found");
    public static Conflict AlreadyExist(string name) => new("Category", "Category with name " + name + " already exist");
    public static Conflict HasProducts(string name) => new("Category", "Category with name " + name + " has products");
}
