using System.Reflection;

namespace autolog;

public static class AssemblyExtensions
{
    public static Assembly LoadFromDisk(string path)
    {
        try
        {
            return AppDomain.CurrentDomain.Load(path);
        }
        catch (FileNotFoundException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
        catch (BadImageFormatException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }
}