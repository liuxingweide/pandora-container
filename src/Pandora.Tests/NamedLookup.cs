/*
 * Copyright 2009 Daniel H�lbling - http://www.tigraine.at
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *  http://www.apache.org/licenses/LICENSE-2.0
 *  
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Pandora.Tests
{
    using Testclasses;
    using Xunit;

    public class NamedLookup
    {
        [Fact]
        public void CanRetrieveServiceByName()
        {
            var store = new ComponentStore();
            //This registration is to give the container something to choose from, in case named lookup won't work
            store.Add<IService, ClassWithDependencyOnItsOwnService>("memory.repository");
            store.Add<IService, ClassWithNoDependencies>("db.repository");
            var container = new PandoraContainer(store);

            var service = container.Resolve<IService>("db.repository");
            Assert.IsType<ClassWithNoDependencies>(service);
        }

        [Fact]
        public void RetrievalByNameThrowsExceptionWhenNameNotRegistered()
        {
            var store = new ComponentStore();
            var container = new PandoraContainer(store);
            //To make it lookup something
            container.Register(p => 
                p.Service<ClassWithNoDependencies>()
                    .Implementor<ClassWithNoDependencies>());

            Assert.Throws<ServiceNotFoundException>(() => container.Resolve<ClassWithNoDependencies>("test"));
        }
    }
}