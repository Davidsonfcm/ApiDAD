using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace AL.Atendimento.SobConsulta.Api
{
    public class SwaggerAddEnumDescriptions : ISchemaFilter, IOperationFilter
    {
        private string DescribeEnum(IList<object> enums, Type type)
        {
            List<string> enumDescriptions = new List<string>();
            foreach (object enumOption in enums)
            {
                if (enumOption is string)
                    enumDescriptions.Add(string.Format("\"{0}\" = {1}", enumOption, GetEnumName((string)enumOption, type)));
                else
                    enumDescriptions.Add(string.Format("{0} = {1}", (int)enumOption, Enum.GetName(enumOption.GetType(), enumOption)));
            }
            return string.Join(", ", enumDescriptions.ToArray());
        }

        public static string GetEnumName(string str, Type enumType)
        {
            foreach (var name in Enum.GetNames(enumType))
            {
                var enumMemberAttribute = ((System.Runtime.Serialization.EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(System.Runtime.Serialization.EnumMemberAttribute), true)).Single();
                if (enumMemberAttribute.Value == str) return name;
            }
            return null;
        }

        public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
        {
            foreach (KeyValuePair<string, Schema> propertyDictionaryItem in schema.properties)
            {
                Schema docProperty = propertyDictionaryItem.Value;
                IList<object> propertyEnums = docProperty.@enum;
                if (propertyEnums != null && propertyEnums.Count > 0)
                {
                    var property = type.GetProperty(propertyDictionaryItem.Key);
                    if (property != null)
                    {
                        docProperty.description += DescribeEnum(propertyEnums, property.PropertyType);
                    }
                    else
                    {
                        var field = type.GetField(propertyDictionaryItem.Key);
                        docProperty.description += DescribeEnum(propertyEnums, field.FieldType);
                    }
                }
            }
        }

        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            foreach (var docParameter in operation.parameters)
            {
                IList<object> propertyEnums = docParameter.@enum;
                if (propertyEnums != null && propertyEnums.Count > 0)
                {
                    var parameter = apiDescription.ParameterDescriptions.First(item => item.Name == docParameter.name);
                    docParameter.description += DescribeEnum(propertyEnums, ((System.Web.Http.Controllers.ReflectedHttpParameterDescriptor)parameter.ParameterDescriptor).ParameterType);
                }
            }
        }
    }
}