namespace PandaSharp.Framework.IoC.Injections
{
    public sealed class InjectProperty
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