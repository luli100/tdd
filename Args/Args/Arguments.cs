using NUnit.Framework;
using System.Reflection;

namespace Args
{
    public class Arguments
    {
        public static T Parse<T>(params String[] args)
        {
            PropertyInfo[] props = typeof(T).GetProperties();
            List<Object> objects = new List<Object>();
            foreach (var item in props)
            {
                objects.Add(parseOption(args.ToList(), item));
            }
            return (T?)Activator.CreateInstance(typeof(T), args: objects.ToArray());
        }

        private static Object parseOption(List<String> args, PropertyInfo prop)
        {
            object[] attrs = prop.GetCustomAttributes(true);
            foreach (object attr in attrs)
            {
                var option = attr as OptionAttribute;
                if (option != null)
                {
                    if (prop.PropertyType == typeof(Boolean))
                    {
                        foreach (var item in args)
                        {
                            if (item.Contains(option.Value))
                            {
                                return true;
                            }
                        }
                    }
                    else if (prop.PropertyType == typeof(UInt16))
                    {
                        for (int i = 0; i < args.Count; i++)
                        {
                            if (args[i].Contains(option.Value) && i < (args.Count - 1))
                            {
                                if (UInt16.TryParse(args[i + 1], out var value))
                                {
                                    return value;
                                }
                            }
                        }
                    }
                    else if (prop.PropertyType == typeof(String))
                    {
                        for (int i = 0; i < args.Count; i++)
                        {
                            if (args[i].Contains(option.Value) && i < (args.Count - 1))
                            {
                                return args[i + 1];
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}