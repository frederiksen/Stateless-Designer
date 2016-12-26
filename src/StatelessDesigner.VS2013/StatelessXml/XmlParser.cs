using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace StatelessXml
{
  public class XmlParser
  {
    private XElement xElement = null;
    private XNamespace ns = "http://statelessdesigner.codeplex.com/Schema";

    public XmlParser(string xmlContent)
    {
      xElement = XElement.Parse(xmlContent);
    }

    public string ItemName
    {
      get
      {
        return xElement
              .Descendants(ns + "itemname")
              .First()
              .Value;
      }
    }

    public string NameSpace
    {
      get
      {
        return xElement
              .Descendants(ns + "namespace")
              .First()
              .Value;
      }
    }

    public string ClassType
    {
      get
      {
        return xElement
              .Descendants(ns + "class")
              .First()
              .Value;
      }
    }

    public IEnumerable<string> Triggers
    {
      get { return from e in xElement.Descendants(ns + "trigger") select e.Value; }
    }

    public string StartState
    {
      get
      {
        return (from xElem in xElement.Descendants(ns + "state")
                where (xElem.Attribute("start") != null && xElem.Attribute("start").Value == "yes")
                select xElem.Value).FirstOrDefault();
      }
    }

    public IEnumerable<string> States
    {
      get { return from e in xElement.Descendants(ns + "state") select e.Value; }
    }

    public IEnumerable<Transition> Transitions
    {
      get
      {
        return from xElem in xElement.Descendants(ns + "transition")
               select
                 new Transition
                   {
                     Trigger = xElem.Attribute("trigger").Value,
                     From = xElem.Attribute("from").Value,
                     To = xElem.Attribute("to").Value
                   };
      }
    }
  }
}
