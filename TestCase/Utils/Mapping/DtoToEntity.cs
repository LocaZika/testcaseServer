namespace TestCase.Utils.Mapping;

public class DtoToEntity
{
  // private readonly TDto _Dto;
  // private readonly TEntity _Entity;
  // public DtoToEntity(TDto Dto, TEntity Entity){
  //   _Dto = Dto;
  //   _Entity = Entity;
  // }
  public static void Assign<TDto, TEntity>(TDto Dto, TEntity Entity){
    if (Dto == null){
      throw new ArgumentNullException(nameof(Dto));
    }
    if (Entity == null){
      throw new ArgumentNullException(nameof(Entity));
    }
    var DtoType = Dto.GetType();
    var EntityType = Entity.GetType();

    foreach (var propertyInfo in DtoType.GetProperties())
    {
      var EntityPropertyInfo = EntityType.GetProperty(propertyInfo.Name);

      if (EntityPropertyInfo != null && EntityPropertyInfo.CanWrite)
      {
        try
        {
          EntityPropertyInfo.SetValue(Entity, propertyInfo.GetValue(Dto));
        }
        catch (Exception ex)
        {
          // Handle mapping exceptions, e.g., type mismatches or unsupported operations
          Console.WriteLine($"Error mapping property {propertyInfo.Name}: {ex.Message}");
        }
      }
    }
  }
}
