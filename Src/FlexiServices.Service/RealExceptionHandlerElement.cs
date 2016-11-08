using System;
using System.ServiceModel.Configuration;

namespace FlexiServices.Service
{
    public class RealExceptionHandlerElement : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new RealExceptionHandlerServiceBehavior();
        }

        public override Type BehaviorType => typeof(RealExceptionHandlerServiceBehavior);
    }
}
