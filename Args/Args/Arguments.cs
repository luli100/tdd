using NUnit.Framework;
using System.Reflection;

namespace Args
{
    public class Arguments
    {
        public static T Parse<T>(String param)
        {
            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    var option = attr as OptionAttribute;
                    if (option != null)
                    {
                        if (param.Contains(option.Value))
                        {
                            return (T?)Activator.CreateInstance(typeof(T), args: true);
                        }
                    }
                }
            }

            return (T?)Activator.CreateInstance(typeof(T), args: false);
        }
    }
}