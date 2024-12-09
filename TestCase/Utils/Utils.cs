using TestCase.Utils.EnumType;
using TestCase.Utils.Mapping;

namespace TestCase.Utils;

public class DtoHelper
{
  public static void MappingDtoToEntity<TDto, TEntity>(TDto Dto, TEntity Entity){
    DtoToEntity.Assign<TDto, TEntity>(Dto, Entity);
  } 
  public static bool CompareEnumType<TEnum>(string Value){
    return CompareString.Compare<TEnum>(Value);
  }
}
