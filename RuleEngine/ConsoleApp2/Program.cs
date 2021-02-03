using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace ConsoleApp2
{
    public class Program
    {

        static Expression BuildExpr<T>(Rule r, ParameterExpression param)
        {
            var left = MemberExpression.Property(param, r.MemberName);
            var tProp = typeof(T).GetProperty(r.MemberName).PropertyType;
            ExpressionType tBinary;
            // is the operator a known .NET operator?
            if (ExpressionType.TryParse(r.Operator, out tBinary))
            {
                var right = Expression.Constant(Convert.ChangeType(r.TargetValue, tProp));
                // use a binary operation, e.g. 'Equal' -> 'u.Age == 15'
                return Expression.MakeBinary(tBinary, left, right);
            }
            else
            {
                var method = tProp.GetMethod(r.Operator);
                var tParam = method.GetParameters()[0].ParameterType;
                var right = Expression.Constant(Convert.ChangeType(r.TargetValue, tParam));
                // use a method call, e.g. 'Contains' -> 'u.Tags.Contains(some_tag)'
                return Expression.Call(left, method, right);
            }
        }

        public static void Main()
        {
            Rule rule = new Rule();
            var rules = rule.GetAllRules();
            var payment1 = new Payment
            {
                TypeOfProduct = "Book",
            };
            var payment2 = new Payment
            {
                TypeOfProduct = "Physical product",
            };
            var payment3 = new Payment
            {
                TypeOfProduct = "Membership",
            };
            var payment4 = new Payment
            {
                TypeOfProduct = "Upgrade",
            };
            var payment5 = new Payment
            {
                TypeOfProduct = "Activation",
            };
            var payment6 = new Payment
            {
                TypeOfProduct = "Vedio",
            };

            Console.WriteLine("Please enter type of product");
            string type = Console.ReadLine();

            var rul = rules.Where(r => r.TargetValue == type).FirstOrDefault();
            if (rul != null)
            {
                Func<Payment, bool> compiledRule = CompileRule<Payment>(rul);
                if (compiledRule(payment1))
                {
                    Console.WriteLine("create a duplicate packing slips for the royalty department");
                }
                else if (compiledRule(payment2))
                {
                    Console.WriteLine("generate a packing slip for shipping");
                }
                else
                {
                    try
                    {
                        Console.WriteLine("Rule failed");
                    }
                    catch
                    {
                        Console.WriteLine("Rule failed");
                    }
                }
            }
            else
            {
                Console.WriteLine("Rule not found");
            }
        }

        public static Func<T, bool> CompileRule<T>(Rule r)
        {
            var paramUser = Expression.Parameter(typeof(Payment));
            Expression expr = BuildExpr<T>(r, paramUser);
            // build a lambda function User->bool and compile it
            return Expression.Lambda<Func<T, bool>>(expr, paramUser).Compile();
        }


    }
    public class Rule
    {
        List<Rule> rules = new List<Rule>();

        public Rule()
        {
            rules = new List<Rule>() {
                new Rule("TypeOfProduct", "Equal", "Vedio"),
                new Rule("TypeOfProduct", "Equal", "Physical product"),
                new Rule("TypeOfProduct", "Equal", "Membership"),
                new Rule("TypeOfProduct", "Equal", "Upgrade"),
                new Rule("TypeOfProduct", "Equal", "Activation"),
                new Rule("TypeOfProduct", "Equal", "Book")
            };
        }
        public string MemberName
        {
            get;
            set;
        }

        public string Operator
        {
            get;
            set;
        }

        public string TargetValue
        {
            get;
            set;
        }

        public Rule(string MemberName, string Operator, string TargetValue)
        {
            
            this.MemberName = MemberName;
            this.Operator = Operator;
            this.TargetValue = TargetValue;
        }

        public List<Rule> GetAllRules()
        {
            return rules;
        }

        public void AddRule(Rule rule)
        {
            rules.Add(rule);
        }
    }

    public class Payment
    {
        public string TypeOfProduct
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
