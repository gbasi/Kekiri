﻿using System;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Kekiri.TestRunner.Xunit.Infrastructure
{
    public class ScenarioDiscoverer : FactDiscoverer
    {
        private readonly IMessageSink _diagnosticMessageSink;

        public ScenarioDiscoverer(IMessageSink diagnosticMessageSink) : base(diagnosticMessageSink)
        {
            _diagnosticMessageSink = diagnosticMessageSink;
        }

        protected override IXunitTestCase CreateTestCase(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod,
            IAttributeInfo factAttribute)
        {

            if(!typeof(Scenarios).GetTypeInfo().IsAssignableFrom(testMethod.TestClass.Class.ToRuntimeType()))
                throw new NotSupportedException("The Scenario attribute can only be placed on a class inheriting from Kekiri.TestRunner.Xunit.Scenarios");

            return new ScenarioTestCase(_diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod);
        }
    }
}