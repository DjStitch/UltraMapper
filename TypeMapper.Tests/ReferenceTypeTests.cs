﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeMapper.Configuration;
using TypeMapper.Mappers;
using TypeMapper.MappingConventions;

namespace TypeMapper.Tests
{
    [TestClass]
    public class ReferenceTypeTests
    {
        private class OuterType
        {
            public InnerType NullInnerType { get; set; }
            public InnerType InnerType { get; set; }

            public OuterType()
            {
                this.InnerType = new InnerType()
                {
                    A = "a",
                    B = "b"
                };
            }
        }

        private class OuterTypeDto
        {
            public InnerTypeDto NullInnerTypeDto { get; set; }
            public InnerTypeDto InnerTypeDto { get; set; }

            public OuterTypeDto()
            {
                this.NullInnerTypeDto = new InnerTypeDto();
                this.InnerTypeDto = new InnerTypeDto();
            }
        }

        private class InnerType
        {
            public string A { get; set; }
            public string B { get; set; }

            public OuterType C { get; set; }
        }

        private class InnerTypeDto
        {
            public string A { get; set; }
            public string B { get; set; }

            public OuterType C { get; set; }
        }

        [TestMethod]
        public void ReferenceTypeTest()
        {
            var temp = new OuterType();
            var temp2 = new OuterTypeDto();

            var typeMapper = new TypeMapper<CustomMappingConvention>( cfg =>
            {
                //cfg.ObjectMappers.Add<BuiltInTypeMapper>()
                //    .Add<ReferenceMapper>()
                //    .Add<CollectionMapper>()
                //    .Add<DictionaryMapper>();
                cfg.GlobalConfiguration.ReferenceMappingStrategy = ReferenceMappingStrategies.USE_TARGET_INSTANCE_IF_NOT_NULL;

                cfg.GlobalConfiguration.MappingConvention.PropertyMatchingRules
                    //.GetOrAdd<TypeMatchingRule>( rule => rule.AllowImplicitConversions = true )
                    .GetOrAdd<ExactNameMatching>( rule => rule.IgnoreCase = true )
                    .GetOrAdd<SuffixMatching>( rule => rule.IgnoreCase = true )
                    .Respect( ( /*rule1,*/ rule2, rule3 ) => /*rule1 & */(rule2 | rule3) );
            } );

            typeMapper.Map( temp, temp2 );
        }
    }
}
