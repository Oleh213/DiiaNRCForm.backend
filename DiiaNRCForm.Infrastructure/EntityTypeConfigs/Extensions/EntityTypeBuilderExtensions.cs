using System.Linq.Expressions;
using System.Reflection;
using DiiaNRCForm.Abstractions.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiiaNRCForm.Infrastructure.EntityTypeConfigs.Extensions;

internal static class EntityTypeBuilderExtensions
{
    public static void OwnsOneWithName<T, TProperty>(this EntityTypeBuilder<T> builder,
        Expression<Func<T, TProperty?>> navigationProperty) where T : Entity where TProperty : class
    {
        var (type, name) = AnalyzeProperty(navigationProperty);
        if (type == null || string.IsNullOrEmpty(name))
        {
            builder.OwnsOne(navigationProperty);
        }
        else
        {
            builder.OwnsOne(type, name);
        }
    }

    private static (Type? type, string? name) AnalyzeProperty<T, TProperty>(
        Expression<Func<T, TProperty>> navigationProperty) where T : Entity
    {
        return navigationProperty.Body is MemberExpression memberExpression
            ? (GetMemberType(memberExpression.Member), memberExpression.Member.Name)
            : (null, null);
    }

    private static Type? GetMemberType(MemberInfo memberInfo)
    {
        return memberInfo switch
        {
            PropertyInfo property => property.PropertyType,
            FieldInfo field => field.FieldType,
            _ => null
        };
    }
}
