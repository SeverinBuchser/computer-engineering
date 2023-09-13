using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BooleanAlgebra
{
    public static class Parser {
        public static readonly string GodPattern = @"(?:(?<not1>!)?(?:(?<var1>\w+)|(?:\(((?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!)))\))))(?:(?<operator>[&\|])(?:(?<not2>!)?(?:(?<var2>\w+)|(?:\(((?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!)))\)))))?";
        public static readonly string VariablePattern = @"\w+";
        public static readonly string WhiteSpacePattern = @"\s+";
        
        public static Evaluator Parse(string formula)
        {
            formula = Regex.Replace(formula, WhiteSpacePattern, "");
            Dictionary<string, Evaluator> variables = new Dictionary<string, Evaluator>();
            foreach (Match m in Regex.Matches(formula, VariablePattern))
                variables[m.Value] = new Variable(m.Value);
            
            return RecursiveParse(formula, variables);
        }
        
        private static Evaluator RecursiveParse(string formula, Dictionary<string, Evaluator> variables)
        {
            if (formula == "") 
                throw new Exception("Formula could not be parsed!");
            Match m = Regex.Match(formula, GodPattern);
            if (m.Value.Length != formula.Length)
                throw new Exception("Formula could not be parsed!");
    
            var a = m.Groups["var1"].Success ? variables[m.Groups["var1"].Value] : RecursiveParse(m.Groups[1].Value, variables);
            a = m.Groups["not1"].Success ? !a : a;
            
            if (!m.Groups["operator"].Success) // no second part
                return a;
    
            var b = m.Groups["var2"].Success ? variables[m.Groups["var2"].Value] : RecursiveParse(m.Groups[2].Value, variables);
            b = m.Groups["not2"].Success ? !b : b;
            
            if (m.Groups["operator"].Value == "&") return a & b;
            return a | b;
        }
    }
}
