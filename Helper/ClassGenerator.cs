namespace autolog.Helper;

public class ClassGenerator
{
    public void GenerateClass(string className)
    {
        using (StreamWriter writer = new StreamWriter($"/Users/bnd/Desktop/btk/projects/first-week/autolog/Concrete/{className}.cs"))
        {
            writer.WriteLine("using System;");
            writer.WriteLine("using System.Collections.Generic;");
            writer.WriteLine("using autolog.Abstract;");
            writer.WriteLine("using System.ComponentModel.DataAnnotations;");
            writer.WriteLine("using System.Linq;");
            writer.WriteLine("using System.Text;");
            writer.WriteLine("using System.Threading.Tasks;");
            writer.WriteLine("namespace autolog.Concrete;");  //namespace adını düzenlememiz lazım kendi namespacemize göre.
            writer.WriteLine($"public class {className} : ILog");
            writer.WriteLine("{");
            writer.WriteLine("public string Log(string message)");
            writer.WriteLine("{");
            writer.WriteLine($"return $\"logged from {className} " + "message:{message}\";");
            writer.WriteLine("}");
            /*            writer.WriteLine("[Key]");
                       writer.WriteLine("private int Id { get; set; }");
                       writer.WriteLine("[Required]");
                       writer.WriteLine("private string Name { get; set; }"); */
            //writer.WriteLine($"    public string MyProperty = \"{myClass.MyProperty}\";");     --> içine prop eklemek istersek bu yapıyı kullanırız.
            writer.WriteLine("}");
        }
    }
}

