using Application.Base;

namespace Application.Stores.Errors;

public static class StoreErrors
{
    public static NotFound NotFound(Guid id) => new("Store", "Store with id " + id + " not found");
    public static Conflict AlreadyExist(string identitytNumber) => new("Store", "Store with identity number " + identitytNumber + " already exist");  
    public static Conflict NotEmpty(string identitytNumber) => new("Store", "Store with identity number " + identitytNumber + " has stocks");  

}
