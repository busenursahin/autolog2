using System.Reflection;
using System.Reflection.Emit;
using autolog.Abstract;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.IO;
using System.Linq;
using autolog.Constant;

namespace autolog;

public class LogFactory
{
    public ILog GetInstance(string className)
    {
        string classFilePath
        = $"{Constants.GeneratedFolder}/{className}.cs";

        string code = File.ReadAllText(classFilePath);
        // C# kodunu derle
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

        var references = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
        .Select(a => MetadataReference.CreateFromFile(a.Location));

        CSharpCompilation compilation = CSharpCompilation.Create(
            $"DynamicAssembly-{className}.dll",
            new[] { syntaxTree },
            references,
            //new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );

        // Derleme sonucunu kontrol et
        EmitResult result = compilation.Emit($"DynamicAssembly-{className}.dll");
        if (result.Success)
        {
            Console.WriteLine($"DLL oluşturuldu: DynamicAssembly-{className}.dll");

            // Oluşturulan DLL'den assembly yükle


            // Sınıfın bir instance'ını oluştur
            // instance = (ILog) Activator.CreateInstance(myClassType);

        }
        else
        {
            Console.WriteLine("Derleme hatası:");
            foreach (var diagnostic in result.Diagnostics)
            {
                Console.WriteLine(diagnostic);
            }
        }

        Assembly dynamicAssembly = Assembly.LoadFrom($"DynamicAssembly-{className}.dll");
        string namespaceName = "autolog.Concrete";
        string fullClassName = namespaceName != null ? $"{namespaceName}.{className}" : className;
        // Yüklü assembly içindeki türü al
        Type myClassType = dynamicAssembly.GetType(fullClassName);
        //  ILog instance = (ILog)Assembly.GetAssembly(typeof(ILog)).CreateInstance(myClassType.Name);
        // Sınıfın bir instance'ını oluştur
        ILog instance = (ILog)Activator.CreateInstance(myClassType);

        return instance;



    }
}