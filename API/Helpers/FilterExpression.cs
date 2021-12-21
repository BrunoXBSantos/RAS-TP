using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Helpers;

public static partial class FilterExpression
{

    public static Expression<Func<T, bool>> BuildPredicate<T>(string propertyName, string comparison, string value)
    {
        // Not developed yet.
        throw new NotImplementedException();
    }

    private static Expression MakeComparison(Expression left, string comparison, string value)
    {
        // Not developed yet.
        throw new NotImplementedException();
    }

    private static Expression MakeString(Expression source)
    {
        // Not developed yet.
        throw new NotImplementedException();
    }

    private static Expression MakeBinary(ExpressionType type, Expression left, string value)
    {
        // Not developed yet.
        throw new NotImplementedException();
    }
}
