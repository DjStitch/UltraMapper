﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TypeMapper.Mappers;

namespace TypeMapper.Internals
{
    public class PropertyMapping : PropertyMapping<object, object>
    {
        public PropertyMapping( SourceProperty sourceProperty,
            TargetProperty targetProperty = null,
            Func<object, object> converter = null ) : base( sourceProperty )
        {

        }
    }

    public class PropertyMapping<TSource, TTarget>
    {
        public SourceProperty<TSource> SourceProperty { get; private set; }
        public TargetProperty<TTarget> TargetProperty { get; set; }

        public IObjectMapper Mapper { get; set; }

        //Generalize to Func<object,object> to avoid carrying too many generic T types around
        //and using Delegate and DynamicInvoke.
        public Func<object, object> ValueConverter { get; set; }

        public PropertyMapping( SourceProperty<TSource> sourceProperty,
            TargetProperty<TTarget> targetProperty = null,
            Func<object, object> converter = null )
        {
            this.SourceProperty = sourceProperty;
            this.TargetProperty = targetProperty;
            this.ValueConverter = converter;
        }

        public override string ToString()
        {
            return $"[Name : {SourceProperty.PropertyInfo.Name}, Type : {SourceProperty.PropertyInfo.PropertyType.FullName}] -> " +
                   $"[Name : { TargetProperty.PropertyInfo.Name}, Type : { TargetProperty.PropertyInfo.PropertyType.FullName}]";
        }
    }
}
