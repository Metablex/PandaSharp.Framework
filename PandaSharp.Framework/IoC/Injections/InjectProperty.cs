namespace PandaSharp.Framework.IoC.Injections
{
    public sealed class InjectProperty : InjectionBase
    {
        public string PropertyName { get; }

        public object PropertyValue { get; }

        public InjectProperty(string propertyName, object propertyValue)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }
    }
}