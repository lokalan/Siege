/*   Copyright 2009 - 2010 Marcus Bratton

     Licensed under the Apache License, Version 2.0 (the "License");
     you may not use this file except in compliance with the License.
     You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

     Unless required by applicable law or agreed to in writing, software
     distributed under the License is distributed on an "AS IS" BASIS,
     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     See the License for the specific language governing permissions and
     limitations under the License.
*/

using System;
using Siege.ServiceLocation.Bindings.Default;
using Siege.ServiceLocation.Stores;
using Siege.ServiceLocation.UseCases;
using Siege.ServiceLocation.UseCases.Default;

namespace Siege.ServiceLocation.RhinoMocksAdapter
{
    public class AutoMockUseCase : UseCase, IInstanceUseCase, IDefaultUseCase
    {
        private readonly Type from;
        protected object implementation;

        public AutoMockUseCase(Type from, object to)
        {
            this.from = from;
            this.implementation = to;
        }

        object IInstanceUseCase.GetBinding()
        {
            return implementation;
        }

        public override Type GetUseCaseBindingType()
        {
            return typeof (DefaultUseCaseBinding);
        }

        protected override IActivationStrategy GetActivationStrategy()
        {
            return new ImplementationActivationStrategy(implementation);
        }

        public override Type GetBoundType()
        {
            return implementation.GetType();
        }

        public override Type GetBaseBindingType()
        {
            return from;
        }

        public class ImplementationActivationStrategy : IActivationStrategy
        {
            private readonly object implementation;

            public ImplementationActivationStrategy(object implementation)
            {
                this.implementation = implementation;
            }

            public object Resolve(IInstanceResolver locator, IServiceLocatorStore context)
            {
                return implementation;
            }
        }
    }
}