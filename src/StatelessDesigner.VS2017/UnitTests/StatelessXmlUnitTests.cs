using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatelessXml;

namespace UnitTests
{
  [TestClass]
  public class StatelessXmlUnitTests
  {
    [TestMethod]
    public void TestItemName()
    {
      // Given
      const string xmlString = "<statemachine xmlns=\"http://statelessdesigner.codeplex.com/Schema\">" +
                               "<settings>" +
                               "<itemname>test1234</itemname>" +
                               "</settings>" +
                               "</statemachine>";

      // When
      var testObject = new XmlParser(xmlString);

      // Then
      Assert.AreEqual("test1234", testObject.ItemName);
    }

    [TestMethod]
    public void TestNamespace()
    {
      // Given
      const string xmlString = "<statemachine xmlns=\"http://statelessdesigner.codeplex.com/Schema\">" +
                               "<settings>" +
                               "<namespace>anamespace</namespace>" +
                               "</settings>" +
                               "</statemachine>";

      // When
      var testObject = new XmlParser(xmlString);

      // Then
      Assert.AreEqual("anamespace", testObject.NameSpace);
    }

    [TestMethod]
    public void TestClass()
    {
      // Given
      const string xmlString = "<statemachine xmlns=\"http://statelessdesigner.codeplex.com/Schema\">" +
                               "<settings>" +
                               "<class>public</class>" +
                               "</settings>" +
                               "</statemachine>";

      // When
      var testObject = new XmlParser(xmlString);

      // Then
      Assert.AreEqual("public", testObject.ClassType);
    }

    [TestMethod]
    public void TestTriggers()
    {
      // Given
      const string xmlString = "<statemachine xmlns=\"http://statelessdesigner.codeplex.com/Schema\">" +
                               "<triggers>" +
                               "<trigger>trigger1</trigger>" +
                               "<trigger>trigger2</trigger>" +
                               "</triggers>" +
                               "</statemachine>";

      // When
      var testObject = new XmlParser(xmlString);

      // Then
      var triggers = testObject.Triggers.ToList();
      Assert.AreEqual(2, triggers.Count);
      Assert.AreEqual("trigger1", triggers[0]);
      Assert.AreEqual("trigger2", triggers[1]);
    }

    [TestMethod]
    public void TestStates()
    {
      // Given
      const string xmlString = "<statemachine xmlns=\"http://statelessdesigner.codeplex.com/Schema\">" +
                               "<states>" +
                               "<state>state1</state>" +
                               "<state>state2</state>" +
                               "</states>" +
                               "</statemachine>";

      // When
      var testObject = new XmlParser(xmlString);

      // Then
      var states = testObject.States.ToList();
      Assert.AreEqual(2, states.Count);
      Assert.AreEqual("state1", states[0]);
      Assert.AreEqual("state2", states[1]);
    }

    [TestMethod]
    public void TestTransitions()
    {
      // Given
      const string xmlString = "<statemachine xmlns=\"http://statelessdesigner.codeplex.com/Schema\">" +
                               "<transitions>" +
                               "<transition trigger=\"Push\" from=\"Locked1\" to=\"Locked2\" />" +
                               "</transitions>" +
                               "</statemachine>";

      // When
      var testObject = new XmlParser(xmlString);

      // Then
      var transitions = testObject.Transitions.ToList();
      Assert.AreEqual(1, transitions.Count);
      Assert.AreEqual("Push", transitions[0].Trigger);
      Assert.AreEqual("Locked1", transitions[0].From);
      Assert.AreEqual("Locked2", transitions[0].To);
    }

    [TestMethod]
    public void TestFirstStateIsStartState()
    {
      // Given
      const string xmlString = "<statemachine xmlns=\"http://statelessdesigner.codeplex.com/Schema\">" +
                               "<states>" +
                               "<state start=\"yes\">state1</state>" +
                               "<state>state2</state>" +
                               "</states>" +
                               "</statemachine>";

      // When
      var testObject = new XmlParser(xmlString);

      // Then
      Assert.AreEqual("state1", testObject.StartState);
    }

    [TestMethod]
    public void TestSecondStateIsStartState()
    {
      // Given
      const string xmlString = "<statemachine xmlns=\"http://statelessdesigner.codeplex.com/Schema\">" +
                               "<states>" +
                               "<state>state1</state>" +
                               "<state start=\"yes\">state2</state>" +
                               "</states>" +
                               "</statemachine>";

      // When
      var testObject = new XmlParser(xmlString);

      // Then
      Assert.AreEqual("state2", testObject.StartState);
    }
  }
}