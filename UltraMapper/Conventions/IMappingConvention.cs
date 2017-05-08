﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraMapper.Internals;

namespace UltraMapper.Conventions
{
    public interface IMappingConvention
    {
        MatchingRules MatchingRules { get; set; }
        IMatchingRuleEvaluator MatchingRuleEvaluator { get; set; }

        IMemberProvider SourceMemberProvider { get; set; }
        IMemberProvider TargetMemberProvider { get; set; }

        IEnumerable<MemberPair> MapByConvention( Type source, Type target );        
    }
}