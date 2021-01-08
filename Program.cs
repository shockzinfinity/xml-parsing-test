using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ConsoleApp1
{
  class Program
  {
    static void Main(string[] args)
    {
      //var todo = new Todo { Id = 1, Title = "Buy milk", UserId = 1 };
      //string xml = todo.ToXmlString();
      //File.WriteAllText("todo.xml", xml);

      //string xml = File.ReadAllText("todo.xml");
      //var todo = XmlHelper.DeserializeFromString<Todo>(xml);
      string xml = File.ReadAllText("cancelList.xml");
      var list = XmlHelper.DeserializeFromString<CancelResponseList>(xml);

      string xml2 = list.ToXmlString(omitXmlDeclaration: false, encoding: Encoding.UTF8);
      Console.WriteLine(xml2);

      Console.WriteLine("Hello World!");
      Console.ReadLine();
    }
  }

  [XmlRoot("CancelResponseList")]
  public class CancelResponseList : List<CancelResponse>
  {
  }

  public class CancelResponse
  {
    public string RequestId { get; set; }
    public string ResultCode { get; set; }
    public string ResultMessage { get; set; }
  }


  public class Todo
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public int UserId { get; set; }
  }
}
