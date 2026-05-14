using System.Reflection;
using Studio36.ControllerComponent;
using Studio36.ModelComponent.Interfaces;
using Studio36.ReportComponent.Interfaces;
using Studio36.ViewComponent.Interfaces;

namespace Studio36.Tests;

public static class T31_ControllerDependeDeInterfaces
{
    public static void Run()
    {
        FieldInfo? modelField = typeof(Controller).GetField("model", BindingFlags.NonPublic | BindingFlags.Instance);
        FieldInfo? viewField = typeof(Controller).GetField("view", BindingFlags.NonPublic | BindingFlags.Instance);
        FieldInfo? reportGeneratorField = typeof(Controller).GetField("reportGenerator", BindingFlags.NonPublic | BindingFlags.Instance);

        if (modelField == null)
        {
            throw new InvalidOperationException("The Controller should have a private model field.");
        }

        if (viewField == null)
        {
            throw new InvalidOperationException("The Controller should have a private view field.");
        }

        if (reportGeneratorField == null)
        {
            throw new InvalidOperationException("The Controller should have a private report generator field.");
        }

        TestHelper.AssertTrue(modelField.FieldType == typeof(IModel), "The Controller model field should depend on IModel.");
        TestHelper.AssertTrue(viewField.FieldType == typeof(IView), "The Controller view field should depend on IView.");
        TestHelper.AssertTrue(reportGeneratorField.FieldType == typeof(IReportGenerator), "The Controller report generator field should depend on IReportGenerator.");
    }
}
