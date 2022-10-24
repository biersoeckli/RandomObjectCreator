using System;
using System.Reflection;
using System.Text;

namespace ObjectCreator
{
    public static class RandomObjectCreator
    {
        public static TEntityType Create<TEntityType>()
        {
            // todo checks for TEntityType
            TEntityType instance = (TEntityType)Activator.CreateInstance(typeof(TEntityType));
            if (instance != null) { PopulateObject(instance); }
            return instance ?? default;
        }

        private static void PopulateObject<TEntityType>(TEntityType instance)
        {
            PropertyInfo[] properties = instance.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                PopulateProperty(instance, property);
            }
        }

        private static void PopulateProperty<TEntityType>(TEntityType instance, PropertyInfo property)
        {
            var typeActionDic = new Dictionary<Type, Action>();

            typeActionDic.Add(typeof(string), () => property.SetValue(instance, RandomValueCreator.GetRandomString()));
            typeActionDic.Add(typeof(bool), () => property.SetValue(instance, RandomValueCreator.GetRandomBool()));
            typeActionDic.Add(typeof(byte), () => property.SetValue(instance, RandomValueCreator.GetRandomByte()));
            typeActionDic.Add(typeof(short), () => property.SetValue(instance, RandomValueCreator.GetRandomShort()));
            typeActionDic.Add(typeof(int), () => property.SetValue(instance, RandomValueCreator.GetRandomInt()));
            typeActionDic.Add(typeof(float), () => property.SetValue(instance, RandomValueCreator.GetRandomFloat()));
            typeActionDic.Add(typeof(double), () => property.SetValue(instance, RandomValueCreator.GetRandomDouble()));
            typeActionDic.Add(typeof(decimal), () => property.SetValue(instance, RandomValueCreator.GetRandomDecimal()));
            typeActionDic.Add(typeof(DateTime), () => property.SetValue(instance, RandomValueCreator.GetRandomDateTime()));
            typeActionDic.Add(typeof(DateOnly), () => property.SetValue(instance, RandomValueCreator.GetRandomDateOnly()));
            typeActionDic.Add(typeof(TimeOnly), () => property.SetValue(instance, RandomValueCreator.GetRandomTimeOnly()));

            if (typeActionDic.Any(typeAction => typeAction.Key == property.PropertyType))
            {
                typeActionDic[property.PropertyType]();
                return;
            }

            if (IsGenericList(property.PropertyType))
            {
                PopulateGenericList(instance, property);
                return;
            }

            // todo add dictionary support 

            if (!property.PropertyType.IsPrimitive && !IsGenericList(property.PropertyType))
            {
                var properyInstance = Activator.CreateInstance(property.PropertyType);
                property.SetValue(instance, properyInstance);
                PopulateObject(properyInstance);
            }
        }

        private static void PopulateGenericList<TEntityType>(TEntityType instance, PropertyInfo property)
        {
            // Create Instance of one element of the list
            var listElementType = property.PropertyType.GetGenericArguments().Single();
            var listElementInstance = Activator.CreateInstance(listElementType);

            // Create a type object representing the generic Dictionary 
            // type, by omitting the type arguments
            Type generic = typeof(List<>);

            // Create an array of types to substitute for the type
            // parameters of Dictionary. The key is of type string, and
            // the type to be contained in the Dictionary is Test.
            Type[] typeArgs = { listElementType };

            // Create a Type object representing the constructed generic type.
            Type constructed = generic.MakeGenericType(typeArgs);
            var constructedListInstance = Activator.CreateInstance(constructed);
            constructedListInstance.GetType().GetMethod("Add").Invoke(constructedListInstance, new[] { listElementInstance });

            property.SetValue(instance, constructedListInstance);
            PopulateObject(listElementInstance);
        }

        // todo move to util class
        public static bool IsGenericList(Type oType)
        {
            if (oType is null)
            {
                return false;
            }
            if (oType.IsGenericType)
            {
                return new List<Type> { typeof(List<>), typeof(ICollection<>), typeof(IEnumerable<>) }.Any(type => oType.GetGenericTypeDefinition() == type);
            }
            return false;
        }
    }
}