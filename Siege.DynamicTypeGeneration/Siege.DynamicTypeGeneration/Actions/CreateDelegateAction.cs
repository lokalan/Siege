﻿/*   Copyright 2009 - 2010 Marcus Bratton

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
using System.Reflection;
using System.Reflection.Emit;

namespace Siege.DynamicTypeGeneration.Actions
{
    internal class CreateDelegateAction : ITypeGenerationAction
    {
        private readonly Func<MethodBuilderBundle> bundle;
        private ConstructorInfo delegateConstructor;
        internal Type DelegateType { get; private set; }

        public CreateDelegateAction(Func<MethodBuilderBundle> bundle, Type returnType)
        {
            this.bundle = bundle;

            if (returnType == typeof(void))
            {
                DelegateType = typeof(Action);
            }
            else
            {
                DelegateType = typeof(Func<>).MakeGenericType(returnType);
            }

            delegateConstructor = DelegateType.GetConstructor(new[] { typeof(object), typeof(IntPtr) });
			
        }

        public void Execute()
        {
            var methodGenerator = bundle().MethodBuilder.GetILGenerator();

            methodGenerator.Emit(OpCodes.Newobj, delegateConstructor);
        }
    }
}
