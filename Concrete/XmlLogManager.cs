using System;
using System.Collections.Generic;
using autolog.Abstract;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace autolog.Concrete;
public class XmlLogManager : ILog
{
public string Log(string message)
{
return $"logged from XmlLogManager message:{message}";
}
}
