namespace TestCase.Utils.EnumType;

public class CompareString
{
  public static bool Compare<TEnum>(string Value){
    bool result = Enum.TryParse(typeof(TEnum), Value, true, out _) && string.Equals(Value, typeof(TEnum).ToString(), StringComparison.OrdinalIgnoreCase);
    if (result == false) return false;
    return true;
  }
}
