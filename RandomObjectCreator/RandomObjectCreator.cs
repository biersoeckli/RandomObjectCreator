using System;
using System.Reflection;
using System.Text;

namespace ObjectCreator
{
    public static class RandomObjectCreator
    {

        public static TEntityType Create<TEntityType>(IList<string> ignoredPropertyNames = null, IList<Type> ignoredPropertyTypes = null)
        {
            // todo checks for TEntityType
            TEntityType instance = (TEntityType)Activator.CreateInstance(typeof(TEntityType));
            if (instance != null) { PopulateObject(instance, ignoredPropertyNames, ignoredPropertyTypes); }
            return instance ?? default;
        }

        private static void PopulateObject<TEntityType>(TEntityType instance, IList<string>? ignoredPropertyNames = null, IList<Type>? ignoredPropertyTypes = null)
        {
            var instanceProperties = instance?.GetType()?.GetProperties() ?? Array.Empty<PropertyInfo>();
            foreach (PropertyInfo property in instanceProperties)
            {
                if (property.MemberType != MemberTypes.Property || !property.CanWrite)
                {
                    continue;
                }
                var propertyNameIsIgnored = ignoredPropertyNames?.Any(x => x.ToLowerInvariant() == property.Name.ToLowerInvariant()) ?? false;
                var propertyTypeIsIgnored = ignoredPropertyTypes?.Any(x => x == property.PropertyType) ?? false;
                if (propertyNameIsIgnored || propertyTypeIsIgnored)
                {
                    continue;
                }
                PopulateProperty(instance, property, ignoredPropertyNames, ignoredPropertyTypes);
            }
        }

        private static void PopulateProperty<TEntityType>(TEntityType instance, PropertyInfo property,
            IList<string>? ignoredPropertyNames = null, IList<Type>? ignoredPropertyTypes = null)
        {
            var typeActionDic = new Dictionary<Type, Action>
            {
                { typeof(string), () => property.SetValue(instance, RandomValueCreator.GetRandomString()) },
                { typeof(bool), () => property.SetValue(instance, RandomValueCreator.GetRandomBool()) },
                { typeof(byte), () => property.SetValue(instance, RandomValueCreator.GetRandomByte()) },
                { typeof(short), () => property.SetValue(instance, RandomValueCreator.GetRandomShort()) },
                { typeof(int), () => property.SetValue(instance, RandomValueCreator.GetRandomInt()) },
                { typeof(float), () => property.SetValue(instance, RandomValueCreator.GetRandomFloat()) },
                { typeof(double), () => property.SetValue(instance, RandomValueCreator.GetRandomDouble()) },
                { typeof(decimal), () => property.SetValue(instance, RandomValueCreator.GetRandomDecimal()) },
                { typeof(DateTime), () => property.SetValue(instance, RandomValueCreator.GetRandomDateTime()) },
                { typeof(DateOnly), () => property.SetValue(instance, RandomValueCreator.GetRandomDateOnly()) },
                { typeof(TimeOnly), () => property.SetValue(instance, RandomValueCreator.GetRandomTimeOnly()) }
            };

            if (typeActionDic.Any(typeAction => typeAction.Key == property.PropertyType))
            {
                typeActionDic[property.PropertyType]();
                return;
            }

            if (ListUtils.IsGenericList(property.PropertyType))
            {
                PopulateGenericList(instance, property);
                return;
            }

            // todo add dictionary support 

            if (!property.PropertyType.IsPrimitive && !ListUtils.IsGenericList(property.PropertyType))
            {
                var properyInstance = Activator.CreateInstance(property.PropertyType);
                property.SetValue(instance, properyInstance);
                PopulateObject(properyInstance, ignoredPropertyNames, ignoredPropertyTypes);
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
    }
}