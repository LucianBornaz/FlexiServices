using System;
using System.ServiceModel.Configuration;

namespace FlexiServices.Client
{
    public class RealExceptionBehaviorExtensionElement : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new RealExceptionEndpointBehavior();
        }

        public override Type BehaviorType => typeof(RealExceptionEndpointBehavior);
    }
}
