using autolog.Abstract;

namespace autolog;

public class LogFactory
{

    public ILog GetInstance(string className)
    {
        string assembly = $"autolog.Concrete.{className}";
        return (ILog )System.Reflection.Assembly.GetAssembly(typeof(ILog)).CreateInstance(assembly);
    }

}